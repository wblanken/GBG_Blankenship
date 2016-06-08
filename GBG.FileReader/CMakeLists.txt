cmake_minimum_required(VERSION 3.5)
project(GBG_FileReader)

set(dir ${CMAKE_CURRENT_SOURCE_DIR})
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY "${dir}/lib")
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY "${dir}/lib")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${dir}/bin")
set (include "${dir}/include")

set(BOOST_ROOT "D:\\Dev\\Libraries\\Boost\\1.61.0\\boost\\bin.v2")
set(BOOST_INCLUDEDIR "D:\\Dev\\Libraries\\boost_1_61_0")

set(Boost_USE_STATIC_LIBS       ON)
set(Boost_USE_MULTITHREADED     ON)

find_package(Boost 1.61.0 COMPONENTS program_options system filesystem REQUIRED)
if (Boost_FOUND)
    include_directories(${Boost_INCLUDE_DIR})
endif ()

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11")

include_directories(${include})

set(SOURCE_FILES
    src/main.cpp include/DataContainer.h include/QualityAllelePair.h)

add_executable(GBG_FileReader ${SOURCE_FILES})

if (Boost_FOUND)
    target_link_libraries(GBG_FileReader ${Boost_LIBRARIES})
endif ()