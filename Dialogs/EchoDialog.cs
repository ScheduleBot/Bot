using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using SimpleEchoBot;
using System.Collections.Generic;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        /// <summary>
        /// When the bot is initialized this is ran
        /// </summary>
        /// <param name="context">Context (I believe) is the environment pertaining to the message came from, Slack / Web Chatbox / Skype </param>
        /// <returns> A "listener" for a message (I believe) </returns>
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            MessageHandler _responseHandler = new MessageHandler();

            var message = await argument;

            string text = message.Text.ToLower();
            //When the bot is "mentioned" in slack, the string sent back to here, so if we find any message in slack that starts the message by mentioning our bot we want to do something.
            if (text.Substring(0, 7) == "@bot202")
            {
                // A message letting the user know we are making calls to the api, (which can be quite slow)
                await context.PostAsync("Booting up windows '98. One moment please!");
                List<string> output = _responseHandler.Handle(text);
                foreach(string str in output)
                {
                    await context.PostAsync(str);
                }
                
            }

        }
    }
}