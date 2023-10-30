using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace WatsonAI
{
    //Need to pass the URL for class's constructor that is responsible for making the call, or an instance of RestSharp for it to use.

    public class WatsonAssistant
    {
        /// <summary>
        /// A class constructor for all of the Watson Assistant Message API methods.
        /// </summary>
        public WatsonAssistant() { }

        //One instance of client.
        public static RestClient client = new RestClient();
        static readonly string watsonMsgUrl = 
            "https://gateway.watsonplatform.net/assistant/api/v1/workspaces/042db9d3-4484-497d-a117-475c549cd866/message?version=2018-09-20"; //Watson Assist "message" API
        //string sampleInput = "{\"input\": {\"text\": \"i want some pizza\"}, \"alternate_intents\": true}";

        /// <summary>
        /// Takes List of string inputs and string apiKey, uses Utilities method to convert apiKey to Base64, uses JObject to format 
        /// parameters for RestSharp to form request foreach input, executes POST request, parses each response to JObject, adds JObject 
        /// to list foreach, and then returns list of JObjects.
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public List<JObject> Message(List<string> inputs, string apiKey) //Watson Assistant Message API
        {
            if (inputs == null)
                throw new ArgumentNullException("List<string> inputs");
            if (apiKey == null)
                throw new ArgumentNullException("string apiKey");

            //Take UTF8 text apiKey and convert to Base64.
            var util = new Utilities(); 
            string byteKey = util.Base64Encode(apiKey); 
            //Console.WriteLine(byteKey);
            string auth = "Basic " + byteKey;

            List<JObject> objList = new List<JObject>();
            foreach (string input in inputs)
            {
                //Format payload parameter for RestSharp request.
                JObject payLoad = new JObject(
                    new JProperty("input",
                        new JObject(
                            new JProperty("text", input),
                            new JProperty("alternate_intents", true)
                            )
                        )
                    );

                var client = new RestClient(watsonMsgUrl);
                var request = new RestRequest(Method.POST);
                //Add headers
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("content-length", "67");
                request.AddHeader("accept-encoding", "gzip, deflate");
                request.AddHeader("Host", "gateway.watsonplatform.net");
                request.AddHeader("Postman-Token", "177ef4df-df0d-466f-a278-b0b860700b36,440bad6c-01c2-4b4a-800d-c314abcc7e05");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("User-Agent", "PostmanRuntime/7.15.0");
                request.AddHeader("Authorization", auth);
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("undefined", payLoad.ToString(), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                //Test
                //Console.WriteLine(response.Content.ToString());
                //Console.WriteLine();
                //Convert to JSON.
                JObject obj = JObject.Parse(response.Content.ToString());
                objList.Add(obj);
            }
            return objList;
        }
    }
}
