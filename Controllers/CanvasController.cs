using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Sample.SimpleEchoBot;
using Newtonsoft.Json;
using SimpleEchoBot.Controllers;
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

        /// <summary>
        /// Once the bot finds the key word Canvas, it is directed here to decide which time-based method to call
        /// </summary>
        /// <param name="text">The Input from the user</param>
        /// <returns>A List of strings for the bot to spit back to the user</returns>
        public List<string> CanvasTimeDecider(string text)
        {
            if (text.Contains("today"))
            {
                return Today(text).Result;
            }
            else if (text.Contains("week"))
            {
                return Week(text).Result;
            }
            else if (text.Contains("upcoming"))
            {
                return Upcoming(text).Result;
            }
            else
            {
                HelpController _help = new HelpController();
                return _help.HandlerError(text);
            }
        }

        /// <summary>
        /// If the users requests for CANVAS assignments for TODAY, they are sent here
        /// </summary>
        /// <param name="text">The input from the user</param>
        /// <returns>A string List containing the assignments and due dates</returns>
        public async Task<List<string>> Today(string text)
        {
            using (var client = new HttpClient())
            {
                // add the appropriate properties on top of the client base address.
                client.BaseAddress = new Uri("https://canvas.instructure.com/api/v1/courses/");
                string key = "7~2nBgVhvuHwrVPZ3VJhIAv7bV4SXfQOCRHBKsP4mjVa2lFylbUu3TkRPprKuSHggi";
                //Json object from 3rd party api : the .Result is important for us to extract the result of the response from the call
                var response = client.GetAsync($"1338200/assignments?bucket=upcoming&access_token={key}").Result;
                List<string> output = new List<string>();
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var stringListResult = await response.Content.ReadAsStringAsync();
                    List<RootObject> canvasObj = JsonConvert.DeserializeObject<List<RootObject>>(stringListResult);
                    List<RootObject> organize = canvasObj.Where(x => x.due_at < DateTime.Now.AddHours(24)).ToList();
                    foreach (RootObject obj in organize)
                    {
                        output.Add($"{obj.name} is due today at {obj.due_at.ToString("MM/dd/yyyy hh:mm tt")}");
                    }
                    return output;
                }
                else
                {
                    output.Add("Something went wrong with today's Assignments");
                    return output;
                    
                }
            }
        }

        /// <summary>
        /// When the User requests for the CANVAS assignments that are due this WEEK, they are sent here
        /// </summary>
        /// <param name="text">The input from the user</param>
        /// <returns>A string List containing a max of 9 assigments</returns>
        public async Task<List<string>> Week(string text)
        {
            using (var client = new HttpClient())
            {

                // add the appropriate properties on top of the client base address.
                client.BaseAddress = new Uri("https://canvas.instructure.com/api/v1/courses/");
                List<string> output = new List<string>();
                string key = "7~2nBgVhvuHwrVPZ3VJhIAv7bV4SXfQOCRHBKsP4mjVa2lFylbUu3TkRPprKuSHggi";
                //Json object from 3rd party api : the .Result is important for us to extract the result of the response from the call
                var response = client.GetAsync($"1338200/assignments?bucket=upcoming&access_token={key}").Result;
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var stringListResult = await response.Content.ReadAsStringAsync();
                    List<RootObject> canvasObj = JsonConvert.DeserializeObject<List<RootObject>>(stringListResult);
                    var j = (from e in canvasObj
                             orderby e.due_at
                             select e);
                    List<RootObject> organize = canvasObj.Where(x => x.due_at < DateTime.Now.AddDays(7)).Take(9).OrderBy(x => x.due_at).ToList();
                    foreach (RootObject obj in organize)
                    {
                        output.Add($"{obj.name} is due at {obj.due_at.ToString("MM/dd/yyyy hh:mm tt")}");
                    }
                    return output;

                }
                else
                {
                    output.Add("Something went wrong with your request for Canvas Assignments this week");
                    return output;

                }
            }
        }

        /// <summary>
        /// When the user requests for UPCOMING CANVAS assignments they are directed here
        /// </summary>
        /// <param name="text">The input from the user</param>
        /// <returns>A List of strings containing the next 5 assignments that are due</returns>
        public async Task<List<string>> Upcoming(string text)
        {
            using (var client = new HttpClient())
            {

                // add the appropriate properties on top of the client base address.
                client.BaseAddress = new Uri("https://canvas.instructure.com/api/v1/courses/");
                List<string> output = new List<string>();
                string key = "7~2nBgVhvuHwrVPZ3VJhIAv7bV4SXfQOCRHBKsP4mjVa2lFylbUu3TkRPprKuSHggi";
                //Json object from 3rd party api : the .Result is important for us to extract the result of the response from the call
                var response = client.GetAsync($"1338200/assignments?bucket=upcoming&access_token={key}").Result;
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var stringListResult = await response.Content.ReadAsStringAsync();
                    List<RootObject> canvasObj = JsonConvert.DeserializeObject<List<RootObject>>(stringListResult);
                    List<RootObject> organize = canvasObj.Where(x => x.due_at < DateTime.Now.AddDays(5)).Take(5).OrderBy(x => x.due_at).ToList();
                    foreach (RootObject obj in organize)
                    {
                        output.Add($"{obj.name} is due at {obj.due_at.ToString("MM/dd/yyyy hh:mm tt")}");
                    }
                    return output;

                }
                else
                {
                    output.Add("Something went wrong with your request for Upcoming Canvas Assignments");
                    return output;

                }
            }
        }
    }
}