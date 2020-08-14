// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.9.2

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly DialogSet _dialogs;
        private readonly DialogSet _dialogsWaterfall;

        public EchoBot(EchoBotAccessors accessors, ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<EchoBot>();
            _logger.LogTrace("EchoBot turn start.");
            _accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));

            _dialogs = new DialogSet(_accessors.DialogState);
            _dialogs.Add(new TextPrompt("askName"));

            _dialogsWaterfall = new DialogSet(_accessors.DialogState);

            var waterfallSteps = new WaterfallStep[]
            {
                GetStartStayDateAsync,
                GetStayDayAsync,
                GetNumberOfOccupantAsync,
                GetBedSizeAsync,
                GetConfirmAsync,
                GetSummaryAsync,
            };

            _dialogsWaterfall.Add(new WaterfallDialog("formFlow", waterfallSteps));
            _dialogsWaterfall.Add(new DateTimePrompt("dateTime"));
            _dialogsWaterfall.Add(new NumberPrompt<int>("number"));
            _dialogsWaterfall.Add(new ChoicePrompt("choice"));
            _dialogsWaterfall.Add(new ConfirmPrompt("confirm"));
        }

        private async Task<DialogTurnResult> GetStartStayDateAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync("dateTime",
            new PromptOptions()
            {
                Prompt = MessageFactory.Text("�п�J�J����"),
            },
            cancellationToken);
        }
        private async Task<DialogTurnResult> GetStayDayAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            (await GetCounterState(stepContext.Context)).RoomReservation.StartDate =
                DateTime.Parse(((List<DateTimeResolution>)stepContext.Result).First().Value);

            return await stepContext.PromptAsync("number", new PromptOptions()
            {
                Prompt = MessageFactory.Text("�п�J�n��X��"),
            },
            cancellationToken);
        }

        private async Task<DialogTurnResult> GetNumberOfOccupantAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            (await GetCounterState(stepContext.Context)).RoomReservation.NumberOfNightToStay = (int)stepContext.Result - 1;

            return await stepContext.PromptAsync("number",
            new PromptOptions()
            {
                Prompt = MessageFactory.Text("�X�H�J��"),
            },
            cancellationToken);
        }

        private async Task<DialogTurnResult> GetBedSizeAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            (await GetCounterState(stepContext.Context))
                .RoomReservation.NumberOfPepole = (int)stepContext.Result;

            var choices = new List<Choice>()
            {
                new Choice("��H��"),
                new Choice("���H��"),
            };

            return await stepContext.PromptAsync("choice",new PromptOptions()
            {
                Prompt = MessageFactory.Text("�п�ܧɫ�"),
                Choices = choices,
            },cancellationToken);
        }

        private async Task<DialogTurnResult> GetConfirmAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var roomReservation = (await GetCounterState(stepContext.Context)).RoomReservation;

            roomReservation.BedSize = ((FoundChoice)stepContext.Result).Value;

            return await stepContext.PromptAsync("confirm", new PromptOptions()
            {
                Prompt = MessageFactory.Text($"�нT�{�z���q�б���G{Environment.NewLine}" +
                $"{roomReservation}")
            });
        }

        private async Task<DialogTurnResult> GetSummaryAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((bool)stepContext.Result)
            {
                await stepContext.Context.SendActivityAsync
                ($"�q��U�w�����A�q�渹�G{DateTime.Now.Ticks}");
            }
            else
            {
                await stepContext.Context.SendActivityAsync("�w�g�����q��");
            }

            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }

        private async Task<CounterState> GetCounterState(ITurnContext turnContext)
        {
            // Get the conversation state from the turn context.
            return await _accessors.CounterState.GetAsync(turnContext, () => new CounterState());
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                CounterState state = await GetCounterState(turnContext);

                var userInfo = await _accessors.UserInfo.GetAsync(turnContext, () => new Model.UserInfo());

                var dialogWaterfallContext = await _dialogsWaterfall.CreateContextAsync(turnContext, cancellationToken);
                var waterfallResult = await dialogWaterfallContext.ContinueDialogAsync(cancellationToken);

                if (turnContext.Activity.Text == "�q��")
                {
                    await dialogWaterfallContext.BeginDialogAsync("formFlow",
                        null, cancellationToken);
                }
                else if (waterfallResult.Status != DialogTurnStatus.Empty)
                {

                }
                else
                {
                    // Bump the turn count for this conversation.
                    state.TurnCount++;

                    // Set the property using the accessor.
                    await _accessors.CounterState.SetAsync(turnContext, state);

                    // Save the new turn count into the conversation state.

                    // Echo back to the user whatever they typed.
                    var responseMessage = $"Name: {userInfo.Name} Turn {state.TurnCount}: You sent '{turnContext.Activity.Text}'\n";
                    await turnContext.SendActivityAsync(responseMessage);
                }
            }
            else
            {
                await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
            }

            await _accessors.ConversationState.SaveChangesAsync(turnContext);
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
