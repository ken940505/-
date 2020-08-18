﻿using EchoBot1.Bots;
using EchoBot1.Model;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EchoBot1.Dialog
{
    public class HotelDialogSet : DialogSet
    {
        private EchoBotAccessors _accessors;

        public string askNameWaterfall { get; } = "askNameWaterfall";

        public HotelDialogSet
            (IStatePropertyAccessor<DialogState> dialogState, EchoBotAccessors accessors)
            : base(dialogState)
        {
            _accessors = accessors;

            var askNameDialogSet = new WaterfallStep[]
            {
                StartPromptName,
                ProcessPromptName,
            };

            Add(new WaterfallDialog(askNameWaterfall, askNameDialogSet));

            Add(new TextPrompt("textPrompt"));


            var waterfallSteps = new WaterfallStep[]
            {
                GetStartStayDateAsync,
                GetStayDayAsync,
                GetNumberOfOccupantAsync,
                GetBedSizeAsync,
                GetConfirmAsync,
                GetSummaryAsync,
            };

            Add(new WaterfallDialog("bookRoom", waterfallSteps));
            Add(new DateTimePrompt("dateTime"));
            Add(new NumberPrompt<int>("number"));
            Add(new ChoicePrompt("choice"));
            Add(new ConfirmPrompt("confirm"));

            var rootSteps = new WaterfallStep[]
            {
                StartRootAsync,
                ProcessRootAsync,
                LoopRootAsync,
            };

            Add(new WaterfallDialog("root", rootSteps));
        }

        private async Task<DialogTurnResult> LoopRootAsync
            (WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.ReplaceDialogAsync("root", null, cancellationToken);
        }

        private async Task<DialogTurnResult> ProcessRootAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var userInfo = await _accessors.UserInfo.GetAsync(
                stepContext.Context, () => new Model.UserInfo());

            if (string.IsNullOrEmpty(userInfo.Name))
            {
                return await stepContext.BeginDialogAsync(
                    askNameWaterfall, null, cancellationToken);
            }
            else if (stepContext.Result.ToString() == "離開")
            {
                await stepContext.Context.SendActivityAsync("已經取消訂單");
                return await stepContext.ReplaceDialogAsync("root", null, cancellationToken);
            }
            else if (stepContext.Result.ToString() == "訂房")
            {
                return await stepContext.BeginDialogAsync(
                    "bookRoom", null, cancellationToken);
            }
            else
            {
                CounterState state = await GetCounterState(stepContext.Context);

                state.TurnCount++;

                // Set the property using the accessor.
                await _accessors.CounterState.SetAsync(stepContext.Context, state);

                // Save the new turn count into the conversation state.

                // Echo back to the user whatever they typed.
                var responseMessage = $"Name: {userInfo.Name} Turn {state.TurnCount}: You sent '{stepContext.Result}'\n";
                await stepContext.Context.SendActivityAsync(responseMessage);

                return await stepContext.ContinueDialogAsync(cancellationToken);
            }
        }

        private async Task<DialogTurnResult> StartRootAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            CounterState state = await GetCounterState(stepContext.Context);

            if (state.TurnCount == 0)
            {
                await stepContext.Context.SendActivityAsync(
                MessageFactory.Text("若要中途重新啟動，請輸入「離開」"),
                cancellationToken);
                state.TurnCount++;
                return await stepContext.NextAsync();
            }

            return await stepContext.PromptAsync("textPrompt", new PromptOptions()
            {
                Prompt = MessageFactory.Text("您好，能夠幫到您什麽？"),
            },
            cancellationToken);
        }

        #region askName
        private async Task<DialogTurnResult> ProcessPromptName
            (WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var userInfo = await _accessors.UserInfo.GetAsync
                (stepContext.Context, () => new Model.UserInfo());

            userInfo.Name = stepContext.Result.ToString();

            await _accessors.UserInfo.SetAsync(stepContext.Context, userInfo);
            await _accessors.UserState.SaveChangesAsync(stepContext.Context);

            await stepContext.Context.SendActivityAsync($"{userInfo.Name} 您好");

            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }

        private async Task<DialogTurnResult> StartPromptName
            (WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync("textPrompt", new PromptOptions()
            {
                Prompt = MessageFactory.Text("請問尊姓大名？"),
            },
            cancellationToken);
        }
        #endregion

        #region bookRoom
        private async Task<DialogTurnResult> GetSummaryAsync
          (WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((bool)stepContext.Result)
            {
                await stepContext.Context.SendActivityAsync
                    ($"訂單下定完成，訂單號：{DateTime.Now.Ticks}");
            }
            else
            {
                await stepContext.Context.SendActivityAsync("已經取消訂單");
            }

            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }

        private async Task<DialogTurnResult> GetConfirmAsync
            (WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var roomReservation = (await GetCounterState(stepContext.Context))
                .RoomReservation;

            roomReservation.BedSize = ((FoundChoice)stepContext.Result).Value;

            return await stepContext.PromptAsync("confirm", new PromptOptions()
            {
                Prompt = MessageFactory.Text($"請確認您的訂房條件：{Environment.NewLine}" +
                $"{roomReservation}")
            });
        }

        private async Task<DialogTurnResult> GetBedSizeAsync
            (WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            (await GetCounterState(stepContext.Context))
                .RoomReservation.NumberOfPepole = (int)stepContext.Result;

            if (stepContext.Result == null) { }
            else if (stepContext.Result.ToString() == "離開")
            {
                await stepContext.Context.SendActivityAsync("已經取消訂單");
                return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
            }

            var choices = new List<Choice>()
            {
                new Choice("單人床"),
                new Choice("雙人床"),
            };

            return await stepContext.PromptAsync("choice",
                new PromptOptions()
                {
                    Prompt = MessageFactory.Text("請選擇床型"),
                    Choices = choices,
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> GetNumberOfOccupantAsync
            (WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            (await GetCounterState(stepContext.Context))
                .RoomReservation.NumberOfNightToStay = (int)stepContext.Result - 1;

            if (stepContext.Result == null) { }
            else if (stepContext.Result.ToString() == "離開")
            {
                await stepContext.Context.SendActivityAsync("已經取消訂單");
                return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
            }

            return await stepContext.PromptAsync("number",
                new PromptOptions()
                {
                    Prompt = MessageFactory.Text("幾人入住"),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> GetStayDayAsync
            (WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            (await GetCounterState(stepContext.Context))
                .RoomReservation.StartDate =
                DateTime.Parse(((List<DateTimeResolution>)stepContext.Result).First().Value);

            if (stepContext.Result == null) { }
            else if (stepContext.Result.ToString() == "離開")
            {
                await stepContext.Context.SendActivityAsync("已經取消訂單");
                return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
            }

            return await stepContext.PromptAsync("number", new PromptOptions()
            {
                Prompt = MessageFactory.Text("請輸入要住幾天"),
            },
            cancellationToken);
        }

        private async Task<DialogTurnResult> GetStartStayDateAsync
            (WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result == null) { }
            else if (stepContext.Result.ToString() == "離開")
            {
                await stepContext.Context.SendActivityAsync("已經取消訂單");
                return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
            }

            return await stepContext.PromptAsync("dateTime",
                new PromptOptions()
                {
                    Prompt = MessageFactory.Text("請輸入入住日期"),
                },
                cancellationToken);
        }
        #endregion

        private async Task<CounterState> GetCounterState(ITurnContext turnContext)
        {
            // Get the conversation state from the turn context.
            return await _accessors.CounterState.GetAsync(turnContext, () => new CounterState());
        }
    }
}