using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Sample.SimpleEchoBot;
using Newtonsoft.Json;
using SimpleEchoBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SimpleEchoBot
{
    public class CanvasController
    {
        public async Task<List<string>> Today(string text)
        {
            using (var client = new HttpClient())
            {
                // add the appropriate properties on top of the client base address.
                client.BaseAddress = new Uri("https://canvas.instructure.com/api/v1/courses/");
                List<string> output = new List<string>();
                
                //Json object from 3rd party api : the .Result is important for us to extract the result of the response from the call
                var response = client.GetAsync($"1338200/assignments?bucket=upcoming&access_token=7~2nBgVhvuHwrVPZ3VJhIAv7bV4SXfQOCRHBKsP4mjVa2lFylbUu3TkRPprKuSHggi").Result;
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var stringListResult = await response.Content.ReadAsStringAsync();
                    List<RootObject> canvasObj = JsonConvert.DeserializeObject<List<RootObject>>(stringListResult);
                    List<RootObject> k = canvasObj.Where(x => x.due_at < DateTime.Now.AddHours(24)).ToList();
                    foreach (RootObject obj in k)
                    {
                        output.Add($"{obj.name} is due today at {obj.due_at}");
                    }
                    return output;
                }
                else
                {
                    output.Add("Something went wrong");
                    return output;
                    
                }
            }
        }
        public async Task<List<string>> Week(string text)
        {
            using (var client = new HttpClient())
            {

                // add the appropriate properties on top of the client base address.
                client.BaseAddress = new Uri("https://canvas.instructure.com/api/v1/courses/");
                List<string> output = new List<string>();

                //Json object from 3rd party api : the .Result is important for us to extract the result of the response from the call
                var response = client.GetAsync($"1338200/assignments?bucket=upcoming&access_token=7~2nBgVhvuHwrVPZ3VJhIAv7bV4SXfQOCRHBKsP4mjVa2lFylbUu3TkRPprKuSHggi").Result;
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var stringListResult = await response.Content.ReadAsStringAsync();
                    List<RootObject> canvasObj = JsonConvert.DeserializeObject<List<RootObject>>(stringListResult);
                    var j = (from e in canvasObj
                             orderby e.due_at
                             select e);
                    List<RootObject> k = canvasObj.Where(x => x.due_at < DateTime.Now.AddDays(7)).Take(9).OrderBy(x => x.due_at).ToList();
                    foreach (RootObject obj in k)
                    {
                        output.Add($"{obj.name} is due at {obj.due_at}");
                    }
                    return output;

                }
                else
                {
                    output.Add("Something went wrong");
                    return output;

                }
            }
        }
    }
}