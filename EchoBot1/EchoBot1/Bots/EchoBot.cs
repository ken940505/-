// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.9.2

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EchoBot1.Model;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace EchoBot1.Bots
{
    public class EchoBot : ActivityHandler
    {
        private readonly EchoBotAccessors _accessors;
        private readonly ILogger _logger;

        public EchoBot(EchoBotAccessors accessors, ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<EchoBot>();
            _logger.LogTrace("EchoBot turn start.");
            _accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            //var replyText = $"Echo: {turnContext.Activity.Text}";
            //await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                // Get the conversation state from the turn context.
                var state = await _accessors.CounterState.GetAsync(turnContext, () => new CounterState());

                var userInfo = await _accessors.UserInfo.GetAsync(turnContext, () => new Model.UserInfo());

                if (string.IsNullOrEmpty(userInfo.Name)
                        && state.CurrentConversationFlow == "askName")
                {
                    state.CurrentConversationFlow = "getName";

                    await _accessors.CounterState.SetAsync(turnContext, state);
                    await _accessors.ConversationState.SaveChangesAsync(turnContext);

                    await turnContext.SendActivityAsync("請問尊姓大名？");
                }
                else if (state.CurrentConversationFlow == "getName")
                {
                    userInfo.Name = turnContext.Activity.Text;
                    state.CurrentConversationFlow = "done";

                    await _accessors.UserInfo.SetAsync(turnContext, userInfo);
                    await _accessors.UserState.SaveChangesAsync(turnContext);

                    await _accessors.CounterState.SetAsync(turnContext, state);
                    await _accessors.ConversationState.SaveChangesAsync(turnContext);

                    await turnContext.SendActivityAsync($"{userInfo.Name} 您好");
                }
                else
                {
                    // Bump the turn count for this conversation.
                    state.TurnCount++;

                    // Set the property using the accessor.
                    await _accessors.CounterState.SetAsync(turnContext, state);

                    // Save the new turn count into the conversation state.
                    await _accessors.ConversationState.SaveChangesAsync(turnContext);

                    // Echo back to the user whatever they typed.
                    var responseMessage = $"Name: {userInfo.Name} Turn {state.TurnCount}: You sent '{turnContext.Activity.Text}'\n";
                    await turnContext.SendActivityAsync(responseMessage);
                }
            }
            else
            {
                await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
            }
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
