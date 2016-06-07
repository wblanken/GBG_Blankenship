//
// Copyright (c) 2016 Will Blankenship, All rights reserved.
//

#include<boost/program_options.hpp>
namespace po = boost::program_options;

#include <iostream>
using namespace std;

int main(int argc, char* argv[])
{
    cout << "***************************************" << endl <<
            "Sample Document Parser For Gene By Gene" << endl <<
            "By William Blankenship" << endl <<
            "***************************************" << endl << endl;

    try
    {
        po::options_description desc("Allowed options");
        desc.add_options()
            ("help", "produce help message")
            ("a", po::value<string>(), "tsv input file")
            ("r", po::value<string>(), "text input file")
            ("o", po::value<string>(), "output file")
        ;

        po::variables_map vm;
        po::store(po::parse_command_line(argc, argv, desc), vm);
        po::notify(vm);

        if(vm.count("help"))
        {
            cout << desc << endl;
            return 0;
        }
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