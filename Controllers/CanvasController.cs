using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json;
using SimpleEchoBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace SimpleEchoBot
{
    public class CanvasController
    {
        //private IDialogContext _context { get; set; }
        //public CanvasController(IDialogContext context)
        //{
        //    _context = context;
        //}
        

        public async void TodayAsync(string text)
        {
            using (var client = new HttpClient())
            {

                // add the appropriate properties on top of the client base address.
                client.BaseAddress = new Uri("https://canvas.instructure.com/api/v1/courses/");

                //Json object from 3rd party api : the .Result is important for us to extract the result of the response from the call
                var response = client.GetAsync($"1338200/assignments?bucket=upcoming&access_token=7~2nBgVhvuHwrVPZ3VJhIAv7bV4SXfQOCRHBKsP4mjVa2lFylbUu3TkRPprKuSHggi").Result;
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var stringListResult = await response.Content.ReadAsStringAsync();
                    List<RootObject> canvasObj = JsonConvert.DeserializeObject<List<RootObject>>(stringListResult);
                    foreach(var obj in canvasObj)
                    {
                        await _context.PostAsync(obj.name);
                        //context.Wait(MessageReceivedAsync);
                        
                    }
                    throw new NotImplementedException();
                }
            }
        }

        public void Week(string text)
        {
            throw new NotImplementedException();
        }

        internal void Next(string text)
        {
            throw new NotImplementedException();
        }
    }
}