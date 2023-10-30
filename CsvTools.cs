using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WatsonAI
{
    public class CsvTools
    {
        public CsvTools()
        {}

        /// <summary>
        /// Takes string path to the input CSV file (ignores commas; expecting only a single column with rows of text), creates List of
        /// *String from CVS file and parses into a List of *string for sending to WatsonAssistant class.
        /// </summary>
        /// <param name="inFilePath"></param>
        /// <returns></returns>
        public List<string> ParseInputsList(string inFilePath)
        {
            var inCsvContents = File.ReadAllLines(inFilePath).ToList(); //Creates List<string> object with rows/newlines of text. No comma 
                                                                        //delimeters since only one column.

            if (inCsvContents == null || inCsvContents.Count == 0)
                throw new ArgumentNullException("List<String> inCsvContents");

            var listInputs = new List<string>();
            foreach (var row in inCsvContents)
            {
                //Test
                //Console.WriteLine(row.Trim('"'));

                //Takes newline-delimited string inCsvContents and splits by row/newline char, creating an IEnumerable<string>...
                var iEnumerableInputs = row.Split('\n').Select(rows => rows.Trim('"')); //Select takes function named rows where it trims off 
                                                                                        //double quote chars, returns to var iEnumerableInputs.
                listInputs.AddRange(iEnumerableInputs); //Add IEnumerable List to List<string> to return in proper form.
               
            }
            return listInputs;
        }

        /// <summary>
        /// Takes string outputFilePath and List of String outputCsvContents, and writes all rows of pre-formatted and sorted text from 
        /// outputCsvContents to CSV file.
        /// </summary>
        /// <param name="outFilePath"></param>
        /// <param name="outCsvContents"></param>
        public void WriteToCsv(string outFilePath, List<String> outCsvContents)
        {
            if (outFilePath == null)
                throw new ArgumentNullException("string outFilePath");
            if (outCsvContents == null)
                throw new ArgumentNullException("List<String> outCsvContents");

            var strArray = outCsvContents.ToArray();

            using (var stream = File.OpenWrite(outFilePath))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(string.Join("\n", strArray));
                    Console.Write(string.Join("\n", strArray));
                }
            }
            //File.WriteAllLines(outFilePath, outCsvContents); //WriteAllLines()/WriteLine() append an extra empty line at the end; couldn't use.
        }

        /// <summary>
        /// Takes List of Intents (from MapJObjectsToCSharpTool.IntentsList result), orders list by descending intent:confidence value (intScore), 
        /// foreach: formats and adds to a List of string to be returned for calling WriteToCsv method.
        /// </summary>
        /// <param name="intentsList"></param>
        /// <returns></returns>
        public List<string> PopulateIntentsListCsv(List<Intents> intentsList)
        {
            if (intentsList == null)
                throw new ArgumentNullException("List<Intents> intentsList");

            List<String> outCsvContents = new List<string>();
            var sortedList = intentsList.OrderByDescending(u => u.IntScore);
            string percent = "";
            
            foreach (var iS in sortedList)
            {
                percent = String.Format("{0:P2}", iS.IntScore);
                outCsvContents.Add(iS.Input + "," + iS.Intent + "," + percent);
                //Test
                //Console.WriteLine(iS.Input + "," + iS.Intent + "," + percent);
            }
            //Console.WriteLine(outCsvContents.Count());
            //outCsvContents.RemoveAll(item => item.Length == 0); //Removes empty items from list.
            return outCsvContents;
        }
    }
}
