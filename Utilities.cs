using System;
using System.Text;

namespace WatsonAI
{
    class Utilities
    {

        public Utilities() { }

        /// <summary>
        /// Takes UTF8 string apiKey and converts it to Base64 encoding for RestSharp auth parameter part of POST request.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string Base64Encode(string plainText)
        {
            if (plainText == null)
                throw new ArgumentNullException("string plainText");
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        //apikey:qg5nT_mlTrGwvFvBjW41kFQ70m1o8QCgT4VSqX5P-KaS; //UTF-8 to byte array
        //Convert.FromBase64String()
    }
}
