# README #

This repository is of coding challenges for Gene By Gene.
Both task 1 and task 2 are included in the visual studio solution file. There is also a cmake configuration included for Task 2 (GBG.FileReader/CMakeLists.txt)

I built both applications on Windows using Visual Studio 2015 for task 1 and CLion for Task 2 (with some debugging done in Visual Studio 2015). I also tested the 2nd task in a Ubuntu environment to verify it could build and run correctly.

## Task 1 ##

### GBG.Web ###
* This is a web application built using ASP.NET MVC5 and using Angular.js to create a single page application.
* The back-end is built for Microsoft SQL server 2016 (though fully compatible with older versions of SQL Server)
* Dependencies should either be included or pulled from nuget
* There is a hosted version of the application on AppHarbor here: [http://genebygeneblankenship.apphb.com/](http://genebygeneblankenship.apphb.com/)

## Task 2 ##

### GBG.FileReader ###
* This application was built using gcc 4.9.3 on Windows (mingw) as well as tested with gcc 4.8.5 on Ubuntu
* The application requires Boost 1.61.0 (the newest release at the time of writing). 
    * boost/system
    * boost/algorithm
    * boost/program_options
    * boost/sort
    * boost/filesystem
    * boost/foreach