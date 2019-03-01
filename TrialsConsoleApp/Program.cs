using System;
using TrialsConsoleApp.Trials.DataLogTrials;
using TrialsConsoleApp.Trials.DataLogTrials.DataLogFiles;

namespace TrialsConsoleApp
{
    public class Program
    {
        // Er is iemand die een query language heeft gemaakt voor MathLab? Check:
        // https://github.com/brian-lau/MatlabQuery
        // Dit kan handig zijn uiteindelijk om snel bepaalde zaken makkelijk te maken.

        public static void Main(string[] args)
        {
            // Parse the textfile into lines of which each will be split into it's columns.
            var trialDataLogFile = new DataLogFile(@"C:\Development\Teaching\Trials\TrialsConsoleApp\TrialsConsoleApp\Data\TrialData1.csv");

            // Use the parsed datalogfile to create a trials collection
            var trials = new DataLogTrialCollection(trialDataLogFile);

            // Result to console
            foreach (var trial in trials)
            {
                Console.WriteLine($"Trial {trial.Id} was done successful: {trial.IsCorrect}");
            }

            // Wait for a key
            Console.ReadKey();
        }
    }
}
