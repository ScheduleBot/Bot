using SimpleEchoBot.Controllers;
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
        TodoController _todo = new TodoController();

        public MessageHandler()
        {
        }
        public void Handle(string text)
        {
            if (text.Contains("canvas"))
            {
                CanvasTimeDecider(text);
            }
            else if (text.Contains("todo"))
            {

            }

        }


        /// <summary>
        /// Once Handle method determines the user wants a canvas request, it decides what time parameters they would like their request to be in.
        /// There are 3 options for time. Today, This week, and the next 3 days.
        /// </summary>
        /// <param name="text"></param>
        public void CanvasTimeDecider(string text)
        {
            if (text.Contains("today"))
            {
                _canvas.TodayAsync(text);
            }
            else if (text.Contains("week"))
            {
                _canvas.Week(text);
            }
            else if (text.Contains("next"))
            {
                _canvas.Next(text);
            }
            else
            {
                HandlerError(text);
            }
        }



        /// <summary>
        /// If our bot can't decipher what the user is requesting for, this method is called
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string HandlerError(string text)
        {
            return $"Something went wrong with your request for {text}";
        }
    }
}