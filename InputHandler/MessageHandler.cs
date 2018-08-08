using SimpleEchoBot.Controllers;
using SimpleEchoBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleEchoBot
{
    public class MessageHandler
    {
        HelpController _help = new HelpController();
        CanvasController _canvas = new CanvasController();
        NoteController _notes = new NoteController();

        /// <summary>
        /// This handle method looks for our main "Keys", those being Canvas, Notes, or Help and directs the user to the specific Controller for further "handling"
        /// </summary>
        /// <param name="text">The input by the user</param>
        /// <returns> a List of Strings which are retrieved from the methods being called which will be sent back to the bot to send to the user </returns>
        public List<string> Handle(string text)
        {
            if (text.Contains("canvas"))
            {
                return _canvas.CanvasTimeDecider(text);
            }
            else if (text.Contains("todo"))
            {
                return _notes.NoteTimeDecider(text);
            }
            else if (text.Contains("help"))
            {
                return _help.Helper(text);
            }
            else
            {
                return _help.HandlerError(text);
            }
        }
        /// <summary>
        /// If our bot can't decipher what the user is requesting for, this method is called
        /// </summary>
        /// <param name="text">Input from the user</param>
        /// <returns>A list of strings that direct the user to some help </returns>
        public List<string> HandlerError(string text)
        {
            List<string> output = new List<string>();

            output.Add($"Something went wrong with your request for {text}");
            output.Add("For help mention our Bot followed by the word  \"Help\"");


            return output;
        }
    }
}