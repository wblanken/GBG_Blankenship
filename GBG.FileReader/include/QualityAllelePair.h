//
// Copyright (c) 2016 Will Blankenship, All rights reserved.
//

#ifndef GBG_FILEREADER_QUALITYALLELEPAIR_H
#define GBG_FILEREADER_QUALITYALLELEPAIR_H

#include <string>

struct QualityAllelePair
{

    QualityAllelePair()
    { }

    QualityAllelePair(float Quality, const std::string &Allele) : Quality(Quality), Allele(Allele)
    { }

    float Quality;
    std::string Allele;

    bool operator < (const QualityAllelePair &other) const
    {
        return this->Quality < other.Quality;
    }
};

#endif //GBG_FILEREADER_QUALITYALLELEPAIR_H
