using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
namespace WatsonAI
{

    public class MapJObjectsToCSharpTool
    {
        /// <summary>
        /// A class constructor for all of the JSON object mapping methods.
        /// </summary>
        public MapJObjectsToCSharpTool()
        { }

        /// <summary>
        /// Check that the JSON JOjbect contains the string property sent; for example for now it's mainly checking for the propety called "intents".
        /// </summary>
        /// <param name="jObj"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private bool CheckPropertyExists(JObject jObj, string property)
        {
            var msgProperty = jObj.Property(property);
            if (msgProperty != null)
            {
                return true;
            }
            else
            {
                throw new ArgumentException("Property not found: ", property);
            }
        }

        /// <summary>
        /// Check that JSON List of JObject isn't null or empty.
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        private bool CheckJObjListAny(List<JObject> objList)
        {
            if (objList != null && objList.Any())
            {
                return true;
            }
            else
            {
                throw new ArgumentNullException("List<JObject> objList");
            }
        }


        /// <summary>
        /// Takes JSON List of JObject and returns List of Intent values using JObject mapping to C# obj (called Intents).
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public List<Intents> IntentsList(List<JObject> objList) //Modify later to also accept a string arg for the property(ies) needed.
        {
            var iList = new List<Intents>();
            if (CheckJObjListAny(objList)) //If JObject isn't null or empty.
            {
                //Iterate over JObject list...
                foreach (var obj in objList)
                {
                    if (CheckPropertyExists(obj, "intents")) //If JOjbect contains the property "intents".
                    {
                        string intent = (string)obj["intents"][0]["intent"];
                        double intScore = (double)obj["intents"][0]["confidence"];
                        //string entity = (string)obj["entities"][0]["entity"];
                        //string entityValue = (string)obj["entities"][0]["value"];
                        //double entityScore = (double)obj["entities"][0]["confidence"];
                        string input = (string)obj["input"]["text"];
                        //string output = (string)obj["output"]["generic"][0]["text"];

                        iList.Add(new Intents(input, intent, intScore));
                        //Test
                        //Console.WriteLine(input + " | " + intent + " | " + intScore);
                    }
                }
                return iList;
                //Test
                //Console.WriteLine(uList.Count.ToString());
            }
            else
            { 
                return null;
            }
        }
    }
}