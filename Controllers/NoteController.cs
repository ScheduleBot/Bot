using Newtonsoft.Json;
using SimpleEchoBot.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SimpleEchoBot.Controllers
{
    public class NoteController
    {
        private object client;

        /// <summary>
        /// The Bot has found the key word Note(s) in the user input, so the user is directed here to determine which time-based method to call
        /// </summary>
        /// <param name="text"> the input from the User </param>
        /// <returns> A List of strings coming from the method called </returns>
        public List<string> NoteTimeDecider(string text)
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
                // if the bot cannot find any of the key words, the user is directed to a helper method in the HelpController
                HelpController _help = new HelpController();
                return _help.HandlerError(text);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<List<string>> Today(string text)
        {
            using (var client = new HttpClient())
            {

                // BaseAddress of the api URL we want to hit
                client.BaseAddress = new Uri("http://schedulingbot.azurewebsites.net");

                // Declaring the list of strings we want the bot to spit out
                List<string> output = new List<string>();
                
                //Json object from 3rd party api : the .Result is important for us to extract the result of the response from the call
                var response = client.GetAsync("/api/schedule/today/1").Result;

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var stringListResult = await response.Content.ReadAsStringAsync();
                    List<Note> noteList = JsonConvert.DeserializeObject<List<Note>>(stringListResult);
                    List<Note> k = noteList.Where(x => x.StartTime < DateTime.Now.AddHours(5)).Take(5).OrderBy(x => x.StartTime).ToList();
                    foreach (Note note in k)
                    {
                        output.Add($"{note.Description}");
                    }
                    return output;

                }
                else
                {
                    output.Add("Something went wrong with your request for today's Assignments");
                    return output;
                }
            }
        }

        public async Task<List<string>> Week(string text)
        {
            using (var client = new HttpClient())
            {

                // BaseAddress of the api URL we want to hit
                client.BaseAddress = new Uri("http://schedulingbot.azurewebsites.net");

                //The list of strings we are going to return to the user
                List<string> output = new List<string>();

                //Json object from 3rd party api : the .Result is important for us to extract the result of the response from the call
                var response = client.GetAsync("/api/schedule/week/1").Result;
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var stringListResult = await response.Content.ReadAsStringAsync();
                    List<Note> noteList = JsonConvert.DeserializeObject<List<Note>>(stringListResult);
                    List<Note> k = noteList.Where(x => x.StartTime < DateTime.Now.AddDays(7)).Take(9).OrderBy(x => x.StartTime).ToList();
                    foreach (Note note in k)
                    {
                        output.Add($"{note.Description}");
                    }
                    return output;

                }
                else
                {
                    output.Add("Something went wrong with your request for this week's notes");
                    return output;

                }
            }
        }
        public async Task<List<string>> Upcoming(string text)
        {
            using (var client = new HttpClient())
            {

                // add the appropriate properties on top of the client base address.
                client.BaseAddress = new Uri("http://schedulingbot.azurewebsites.net");

                // declaring A list of strings that will be sent back to the user
                List<string> output = new List<string>();

                //Json object from 3rd party api : the .Result is important for us to extract the result of the response from the call
                var response = client.GetAsync("/api/schedule/threeday/1").Result;
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var stringListResult = await response.Content.ReadAsStringAsync();
                    List<Note> noteList = JsonConvert.DeserializeObject<List<Note>>(stringListResult);
                    List<Note> k = noteList.Where(x => x.StartTime < DateTime.Now.AddHours(5)).Take(5).OrderBy(x => x.StartTime).ToList();
                    foreach (Note note in k)
                    {
                        output.Add($"{note.Description}");
                    }
                    return output;

                }
                else
                {
                    output.Add("Something went wrong with your request for upcoming Notes");
                    return output;

                }
            }
        }
    }
}