using System;
using System.IO;

namespace WatsonAI
{
    class MainProgram
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Parameter count = {0}", args.Length);
            string apiKey;
            string inCsvFilePath;
            string outCsvFilePath;
            // Test if input arguments were supplied.
            if (args.Length < 3)
            {
                Console.WriteLine("Please enter your apiKey, inputCsvFilePath, outputCsvFilePath arguments.");
                Console.WriteLine(@"For example: abcdefghijklmnop ""C:\Users\YourName\input.csv"" ""C:\Users\YourName\output.csv""");
            }
            else
            {
                String[] arguments = Environment.GetCommandLineArgs();
                Console.WriteLine("GetCommandLineArgs: {0}", String.Join(", ", arguments));
                
                apiKey = args[0].ToString();
                inCsvFilePath = args[1].ToString();
                outCsvFilePath = args[2].ToString();

                if (!Path.HasExtension(inCsvFilePath) || !Path.HasExtension(outCsvFilePath))
                {
                    Console.WriteLine("{0} and/or {1} missing an extension.", inCsvFilePath, outCsvFilePath);
                }

                else if (!Path.IsPathRooted(inCsvFilePath) || !Path.IsPathRooted(outCsvFilePath))
                {
                    Console.WriteLine("{0} and/or {1} missing root information.", inCsvFilePath, outCsvFilePath);
                }

                else
                {
                    //Console.WriteLine("{0} is the location for temporary files.", Path.GetTempPath());
                    //Console.WriteLine("{0} is a file available for use.", Path.GetTempFileName());

                    //string inFilePath = @"C:\Users\SUS\Demos\utterances.csv"; //The @ symbol makes it read as a string literal,           
                                                                                //ignoring the backslash escape chars.
                   
                    //Create CsvTools obj.
                    var csvTools = new CsvTools();
                    //Call method ParseInputsList with string inCsvFilePath.
                    var inputs = csvTools.ParseInputsList(inCsvFilePath);
                    
                    var watson = new WatsonAssistant();
                    var jObjects = watson.Message(inputs, apiKey);
                    
                    var mapJObjToCObj = new MapJObjectsToCSharpTool();
                    var intentsList = mapJObjToCObj.IntentsList(jObjects);
                    
                    var intentsListCsv = csvTools.PopulateIntentsListCsv(intentsList);

                    csvTools.WriteToCsv(outCsvFilePath, intentsListCsv);
                }
            }
            Console.ReadLine(); //Keeps console window open for tests.
        }
    }
}
