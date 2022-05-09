// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.15.2

using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DartInsightBot.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            string url = turnContext.Activity.Text;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8089/api/v1/content");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            Debug.Write(url);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
               
                string json = "{\"title\":\"\"," +
                             $"\"url\":\"{url}" + "\"," +
                             "\"author\":\"master\"," +
                             "\"createdDate\":\"\"," +
                             "\"keywords\":\"\"}";

                streamWriter.Write(json); 
                               

        
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

            var replyText = "전달완료";
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
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
