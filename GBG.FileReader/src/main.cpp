//
// Copyright (c) 2016 Will Blankenship, All rights reserved.
//

#include <boost/system/system_error.hpp>
#include <boost/algorithm/string.hpp>
#include <boost/program_options.hpp>
#include <boost/sort/spreadsort/spreadsort.hpp>
#include <boost/filesystem.hpp>
#include <boost/foreach.hpp>

#include <iostream>
#include "DataContainer.h"

using namespace std;
using namespace gbg_filereader;

/************************************
 * Parses the reference data
 * and returns a sorted vector
 ************************************/
vector<string> parseSubset(string path)
{
    boost::system::error_code ec;
    vector<string> retFilters;
    boost::filesystem::ifstream ifs;
    boost::filesystem::path p { path };
    string line;

    if (!boost::filesystem::exists(p, ec))
    {
        throw boost::filesystem::filesystem_error("Subset file not found", p, ec);
    }

    ifs.open(p);

    while(getline(ifs, line))
    {
        // Strip any leading or trailing whitespace from the filter
        boost::algorithm::trim(line);

        // Make the filter all uppercase
        boost::algorithm::to_upper(line);

        retFilters.push_back(line);
    }

    ifs.close();

    boost::sort::spreadsort::spreadsort(retFilters.begin(), retFilters.end());

    return retFilters;
}

/************************************
 * Parses the sample data,
 * finds the data intersection,
 * and returns an sorted vector
 ************************************/
vector<DataContainer> parseData(const string &path, vector<string> *pFilters)
{
    boost::system::error_code ec;
    map<string, DataContainer> parsedData;
    vector<DataContainer> retData;
    boost::filesystem::ifstream ifs;
    boost::filesystem::path p { path };
    string line;

    if (!boost::filesystem::exists(p, ec))
    {
        throw boost::filesystem::filesystem_error("Data file not found", p, ec);
    }

    ifs.open(p);

    while(getline(ifs, line))
    {
        vector<string> splitVector;

        // Strip any whitespace
        boost::algorithm::trim(line);
        boost::algorithm::split(splitVector, line, boost::algorithm::is_any_of("\t"),
                                boost::algorithm::token_compress_on);

        auto searchString = splitVector[0];
        boost::algorithm::to_upper(searchString);

        // See if the SNP data is part of our filter list
        if(binary_search(pFilters->begin(), pFilters->end(), searchString))
        {
            DataContainer data (splitVector[0], splitVector[1], stof(splitVector[2]));
            auto ret = parsedData.insert(pair<string, DataContainer>(data.getSNP(), data));

            // If the key already existed in the map then we have a duplicate SNP
            if(!ret.second)
            {
                parsedData[data.getSNP()].addQuality(data.GetQualityAllelePair());
            }
        }
    }

    ifs.close();

    // Break the map values out into a vector
    typedef std::map<string, DataContainer> data_map_type;
    BOOST_FOREACH(const data_map_type::value_type& dataPair, parsedData)
    {
        retData.push_back(dataPair.second);
    }

    // Sort the vector
    sort(retData.begin(), retData.end());

    return retData;
}

/************************************
 * Creates the output file from the
 * pruned and sorted sample data
 ************************************/
void outputFile(const string &path, const vector<DataContainer> &sortedData)
{
	boost::filesystem::path p{ path };
	boost::filesystem::ofstream ofs { p, ios_base::trunc | ios_base::binary };

	BOOST_FOREACH(DataContainer sample, sortedData)
	{
		auto snp = sample.getSNP();
		BOOST_FOREACH(QualityAllelePair qualityPair, sample.GetQualityAllelePairs())
		{
			ofs << snp << "\t" << qualityPair.Allele << "\t" << qualityPair.Quality << endl;
		}
	}

	ofs.close();
}

int main(int argc, const char* argv[])
{
    cout << "***************************************" << endl <<
            "Sample Document Parser For Gene By Gene" << endl <<
            "By William Blankenship" << endl <<
            "***************************************" << endl << endl;

    try
    {
        boost::program_options::options_description desc("Allowed options");
        desc.add_options()
                ("help,h", "produce help message")
                ("a,a", boost::program_options::value<string>(), "data input file")
                ("r,r", boost::program_options::value<string>(), "subset input file")
                ("o,o", boost::program_options::value<string>(), "output data file");

        boost::program_options::variables_map vm;
        boost::program_options::store(boost::program_options::command_line_parser(argc, argv)
                                              .options(desc)
                                              .style(boost::program_options::command_line_style::unix_style
                                                     | boost::program_options::command_line_style::allow_short
                                                     | boost::program_options::command_line_style::allow_dash_for_short)
                                              .run(),
                                      vm);

        boost::program_options::notify(vm);

        if (vm.count("help"))
        {
            cout << desc << endl;
            return 0;
        }

        if (vm.count("r") && vm.count("a") && vm.count("o"))
        {
	        auto filters = parseSubset(vm["r"].as<string>());
	        auto data = parseData(vm["a"].as<string>(), &filters);
            outputFile(vm["o"].as<string>(), data);
        }
        else
        {
            cout << "Invalid entry, two input files and an output file are required" << endl;
            return -1;
        }
    }
    catch (boost::filesystem::filesystem_error& e)
    {
        cerr << e.what() << endl <<
                e.path1() << endl <<
                "Error code: " << e.code() << endl;
    }
    catch (exception& e)
    {
        cerr << "error: " << e.what() << endl;
    }
    catch (...)
    {
        cerr << "Unknown exception!" << endl;
    }
}