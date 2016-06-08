//
// Copyright (c) 2016 Will Blankenship, All rights reserved.
//

#ifndef GBG_FILEREADER_DATACONTAINER_H
#define GBG_FILEREADER_DATACONTAINER_H

#include <vector>
#include <algorithm>
#include "QualityAllelePair.h"

namespace gbg_filereader
{
    class DataContainer
    {
    private:
        std::string SNP;

        // Collection of qualities paired with their Allele value ([][1]).
        // Since it's possible for there to be duplicate SNPs it's easier to store duplicate
        // values in a single object.
        std::vector<QualityAllelePair> QualityAllelePairs;

    public :
        DataContainer() {}

        DataContainer(std::string snp, std::string allele, float quality) : SNP(snp)
        {
            addQuality(quality, allele);
        }

        /************************************
         * Gets SNP value.
         ************************************/
        const std::string &getSNP() const
        {
            return this->SNP;
        }

        /************************************
         * Sets SNP value
         * if one has not been set
         ************************************/
        void setSNP(const std::string &snp)
        {
            if(this->SNP.empty())
            {
				this->SNP = snp;
            }
        }


        /************************************
         * Adds a new quality to the vector
         * with it's allele value and 
		 * keeps it sorted
         ************************************/
        void addQuality(float newQuality, std::string allele)
        {
			this->QualityAllelePairs.push_back(QualityAllelePair(newQuality, allele));

            if(QualityAllelePairs.size() > 1)
            {
                std::sort(QualityAllelePairs.begin(), QualityAllelePairs.end());
            }
        }

		/************************************
		* Adds a new quality and allele pair
		************************************/
		void addQuality(QualityAllelePair qualityAllelePair)
		{
			addQuality(qualityAllelePair.Quality, qualityAllelePair.Allele);
		}

        /************************************
         * Returns the top level quality,
         * which will always be the lowest value
         ************************************/
        float getTopQuality() const
        {
            return this->QualityAllelePairs[0].Quality;
        }

		/************************************
		* Returns the top level quality,
		* and it's Allele value
		************************************/
		QualityAllelePair GetQualityAllelePair()
		{
			return this->QualityAllelePairs[0];
		}

		/************************************
		* Returns the vector of Quality Allele pairs
		************************************/
		std::vector<QualityAllelePair> GetQualityAllelePairs() const
		{
			return this->QualityAllelePairs;
		}

        /************************************
         * Compare quality then SNP value
         ************************************/
        bool operator < (const DataContainer &other) const
        {
            if(this->getTopQuality() < other.getTopQuality()) return true;
            if(other.getTopQuality() < this->getTopQuality()) return false;

            // Quality is identical so compare SNP
            return this->SNP < other.getSNP();

			return false;
        }
    };
}

#endif //GBG_FILEREADER_DATACONTAINER_H
