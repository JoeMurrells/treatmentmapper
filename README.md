# Treatment Mapper

## Introduction

A tool for mapping treatments as part of a conversion to Dentally. A treatment map CSV is selected and then treatment descriptions are fuzzy matched against master descriptions and a code assigned based on this match, if the match is below 85% the user will be prompted to confirm or change the code, reference codes can be searched to ensure the correct code is used. Once all codes have been mapped a completed treatment map CSV is output.

## Purpose

Greatly reduces the time spent mapping treatments, as previously this was done by manually updating each row of the CSV with the relevant treatment code. Also greatly improves the accuracy of mappings and reduces the chance of error which in turn improves the quality of the data conversion.

## Usage

Start by running "Treatment Mapper.exe".

Enter a reference for the practice you are mapping treatments for, this can be anything you like, it will just help you identify your completed treatment map. Select the relevant system you are mapping the treatments for. Click the Import Treatment Map CSV button and select your CSV file, once selected the tool will start processing the treatments.

If a treatment is found that cannot automatically be mapped or is below 85% of a match (95% for BUPA) the tool will prompt you to enter the correct code or confirm the suggested code, please ensure you enter the code as accurately as possible as if you enter a new mapping, the tool will remember the code you entered so it can map the treatment in the future.

You can use the search box to search the reference codes to ensure the correct code is used to map the treatment.

Once the process has finished the mapped CSV will be output into a folder with the Practice Reference you set located in the output folder, which will be in the same directory as the executable.

### Settings

The Skipped Mapped Treatments options will instruct the software to skip over treatments that already have a code set in the source CSV, this option is ticked by default.

If Enable Logging is ticked the software will output all comparisons made between the input CSV and the master into a log.txt file in the same directory as the executable, this can be used to see how the software made the decision to map a particular treatment the way it did, for example, if you are finding items being mapped incorrectly.

## Packages

CSVHelper is used for the reading and writing of CSV files.
[CSVHelper](https://joshclose.github.io/CsvHelper/)

FuzzySharp is used for the fuzzy matching functionality.
[FuzzySharp](https://github.com/JakeBayer/FuzzySharp)
