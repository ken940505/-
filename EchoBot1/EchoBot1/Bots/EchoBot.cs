// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.9.2

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EchoBot1.Dialog;
using EchoBot1.Model;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace EchoBot1.Bots
{
    public class EchoBot : ActivityHandler
    {
        private readonly EchoBotAccessors _accessors;
        private readonly ILogger _logger;
        private readonly HotelDialogSet _dialogs;

        public EchoBot(EchoBotAccessors accessors, ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<EchoBot>();
            _logger.LogTrace("EchoBot turn start.");
            _accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));

            _dialogs = new HotelDialogSet(_accessors.DialogState, accessors);
        }

        
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                var dc = await _dialogs.CreateContextAsync(
                    turnContext, cancellationToken);

                await dc.ContinueDialogAsync(cancellationToken);

                if (!turnContext.Responded)
                {
                    await dc.BeginDialogAsync
                        ("root", null, cancellationToken);
                }
            }
            else
            {
                await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
            }

            await _accessors.ConversationState.SaveChangesAsync(turnContext);
        }

        private async Task<CounterState> GetCounterState(ITurnContext turnContext)
        {
            // Get the conversation state from the turn context.
            return await _accessors.CounterState.GetAsync(turnContext, () => new CounterState());
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "您好，請問要什麼樣的服務？ 請輸入「訂房」";
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
