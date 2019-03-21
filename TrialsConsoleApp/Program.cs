using System;
using System.IO;
using System.Linq;
using System.Reflection;
using TrialsConsoleApp.Trials.DataLogTrials;
using TrialsConsoleApp.Trials.DataLogTrials.DataLogFiles;
using TrialsConsoleApp.Trials.DataLogTrials.TrialEvents;

namespace TrialsConsoleApp
{
    public class Program
    {
        // Er is iemand die een query language heeft gemaakt voor MathLab? Check:
        // https://github.com/brian-lau/MatlabQuery
        // Dit kan handig zijn uiteindelijk om snel bepaalde zaken makkelijk te maken.

        // C:\Development\Teaching\Trials\TrialsConsoleApp\TrialsConsoleApp\Data\TrialData1.csv
        public static string[] LineDataSeparatorsDefault = { "\t", ";" };

        public static void Main(string[] args)
        {
            Console.WriteLine($"Usage: {Path.GetFileName(Assembly.GetEntryAssembly().Location)} \"input file path\" \"output file path\" \"separators\"");
            Console.WriteLine("When input is not given the application will ask in case of filepaths");
            Console.WriteLine($"In case of separators defaults wil be used: {string.Join(" ", LineDataSeparatorsDefault)}");

            // Get filepaths
            var inputFilepath = args.FirstOrDefault() ?? GetConsoleLine("Specify input path.");
            var outputFilepath = args.Skip(1).FirstOrDefault() ?? GetConsoleLine("Specify output path.");
            var separators = args.Length > 2 ? args.Skip(2).ToArray() : LineDataSeparatorsDefault;

            // Parse the textfile into lines of which each will be split into it's columns.
            var trialDataLogFile = new DataLogFile(inputFilepath, separators);

            // Use the parsed datalogfile to create a trials collection
            var trials = new DataLogTrialCollection(trialDataLogFile);

            // Write to file
            WriteResultsToFile(trials, outputFilepath, separators.First());
        }

        private static void WriteResultsToFile(DataLogTrialCollection trials, string outputFilepath, string s)
        {
            var headings = string.Join(s, new[]
            {
                "TrialID",

                "ResponseIsValid",
                "Responded",
                "ResponseTime",
                "ResponseWorldTime",
                "ResponseTrialTime",

                "StartCode",
                "StartWorldTime",
                "StartTrialTime",
                "StartIsNoGo",

                "EndWorldTime",
                "EndTrialTime",
                "LineCount",

                "Explanation"
            });

            var bodyLines = trials.Select(t => TrialResult.DetermineResult(t)).Select(
                r =>
                {
                    var responded = r.ResponseEvent != null;
                    var rt = responded ? r.ResponseTime.ToString() : string.Empty;
                    return string.Join(s, new[]
                    {
                        $"{r.Trial.Id}",

                        $"{r.UserResponseIsValid}",
                        $"{responded}",
                        $"{rt}",
                        $"{r.ResponseEvent?.WorldTime}",
                        $"{r.ResponseEvent?.TrialTime}",

                        $"{r.FirstTestPictureEvent.PictureId}",
                        $"{r.FirstTestPictureEvent.WorldTime}",
                        $"{r.FirstTestPictureEvent.TrialTime}",
                        $"{r.FirstTestPictureEvent.IsNoGo}",

                        $"{r.EndTestPictureEvent.WorldTime}",
                        $"{r.EndTestPictureEvent.TrialTime}",
                        $"{r.Trial.TrialLineCount}",

                        $"{r.Explanation}",
                    });
                });
            File.WriteAllLines(outputFilepath, new[] { headings }.Concat(bodyLines));
        }

        private static string GetConsoleLine(string description)
        {
            Console.WriteLine(description);
            Console.Write(">");
            return Console.ReadLine();
        }
    }
}
