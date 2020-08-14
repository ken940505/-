// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.9.2

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EchoBot1.Model;
using EchoBot1.Bots;
using Microsoft.Extensions.Options;
using Microsoft.Bot.Builder.Integration;
using System;
using System.Linq;
using Microsoft.Bot.Configuration;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Bot.Builder.Dialogs;


namespace EchoBot1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            // Create the Bot Framework Adapter with error handling enabled.
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

            // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
            services.AddTransient<IBot, EchoBot>();

            services.AddBot<EchoBot>(options =>
            {
                IStorage dataStore = new MemoryStorage();
                var conversationState = new ConversationState(dataStore);
                var userState = new UserState(dataStore);
                options.State.Add(conversationState);
                options.State.Add(userState);
            });

            services.AddSingleton<EchoBotAccessors>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;
                if (options == null)
                {
                    throw new InvalidOperationException("BotFrameworkOptions must be configured prior to setting up the state accessors");
                }

                var conversationState = options.State.OfType<ConversationState>().FirstOrDefault();
                //if (conversationState == null)
                //{
                //    throw new InvalidOperationException("ConversationState must be defined and added before adding conversation-scoped state accessors.");
                //}

                var userState = options.State.OfType<UserState>().FirstOrDefault();

                // Create the custom state accessor.
                // State accessors enable other components to read and write individual properties of state.
                var accessors = new EchoBotAccessors(conversationState, userState)
                {
                    CounterState = conversationState.CreateProperty<CounterState>(EchoBotAccessors.CounterStateName),
                    UserInfo = userState.CreateProperty<UserInfo>(EchoBotAccessors.UserInfoName)
                };

                return accessors;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseWebSockets()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

            // app.UseHttpsRedirection();
        }

    }
}
