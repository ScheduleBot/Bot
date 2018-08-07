using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using SimpleEchoBot;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        protected int count = 1;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            MessageHandler _context = new MessageHandler();
            var message = await argument;
            message.Text = message.Text.ToLower();

            if (message.Text.Substring(0, 4) == "@bot")
            {
                await context.PostAsync(_context.Handle(message.Text));
                await context.PostAsync($"Speaking to the bot today!");
                context.Wait(MessageReceivedAsync);
                if (message.Text.Contains("canvas"))
                {
                    await context.PostAsync($"Speaking to canvas");
                }
                if (message.Text.Contains("meme"))
                {

                }
                
            }

        }

        public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                this.count = 1;
                await context.PostAsync("Reset count.");
            }
            else
            {
                await context.PostAsync("Did not reset count.");
            }
            context.Wait(MessageReceivedAsync);
        }

    }
}