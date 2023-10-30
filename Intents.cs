using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatsonAI
{
    //Class for holding "intent" value objects from JSON result.
    public class Intents
    {
        public Intents() { }
        public Intents(string input, string intent, double intScore)
        {
            Input = input;
            Intent = intent;
            IntScore = intScore;
        }
        public string Input { get; set; }

        public string Intent { get; set; }

        public double IntScore { get; set; }


        /*public Utterance(string input, string entity, string entValue, double entScore, string intent, double intScore, string output)
        {
            Input = input;
            Entity = entity;
            EntValue = entValue;
            EntScore = entScore;
            Intent = intent;
            IntScore = intScore;
            Output = output;

        }
        public string Input { get; set; }

        public string Entity { get; set; }

        public string EntValue { get; set; }

        public double EntScore { get; set; }

        public string Intent { get; set; }

        public double IntScore { get; set; }

        public string Output { get; set; } */

    }
}
