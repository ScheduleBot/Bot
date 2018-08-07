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
        CanvasController _canvas = new CanvasController();
        TestController _test = new TestController();
        //TodoController _todo = new TodoController();

        public MessageHandler()
        {
        }
        public List<string> Handle(string text)
        {
            if (text.Contains("canvas"))
            {
                return CanvasTimeDecider(text);
            }
            //else if (text.Contains("todo"))
            //{
            //    return 
            //}


            List<string> output = new List<string>();
            output.Add("Couldn't find what you were trying to do");
            return output;

        }


        /// <summary>
        /// Once Handle method determines the user wants a canvas request, it decides what time parameters they would like their request to be in.
        /// There are 3 options for time. Today, This week, and the next 3 days.
        /// </summary>
        /// <param name="text"></param>
        public List<string> CanvasTimeDecider(string text)
        {
            if (text.Contains("today"))
            {
                return _canvas.Today(text).Result;
            }
            else if (text.Contains("week"))
            {
                return _canvas.Week(text).Result;
            }
            //else if (text.Contains("upcoming"))
            //{
            //    _canvas.Next(text);
            //}
            else
            {
                return HandlerError(text);
            }
        }



        /// <summary>
        /// If our bot can't decipher what the user is requesting for, this method is called
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<string> HandlerError(string text)
        {
            List<string> output = new List<string>();
            
            output.Add($"Something went wrong with your request for {text}");
            return output;
        }
    }
}