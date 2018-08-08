using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleEchoBot.Controllers
{
    public class HelpController
    {
        /// <summary>
        /// When the user requests for help with the "Help" key, they are sent to this method
        /// </summary>
        /// <param name="text">The Input from the user</param>
        /// <returns>output</returns>
        public List<string> Helper(string text)
        {
            
            List<string> output = new List<string>();
            output.Add("To use our Bot you must begin your sentence with a mention of the bot ex: @bot202 or @Schedule");
            output.Add("Then you must specify if you want to list canvas or from your class's notes");
            output.Add("Lastly you want to specificy what time span you want your results to be from, Today, Upcoming, or this Week.");
            return output;
            
        }

        /// <summary>
        /// When the bot can not find what the user is asking for, they are sent here
        /// </summary>
        /// <param name="text">The input from the user</param>
        /// <returns>A List of strings that explain there was an error finding what they are looking for and advise the user to read get help from the bot </returns>
        public List<string> HandlerError(string text)
        {
            List<string> output = new List<string>();
            output.Add($"Something went wrong with your request for {text}");
            output.Add("For help mention our Bot followed by the word \"Help\"");
            return output;
        }
    }
}