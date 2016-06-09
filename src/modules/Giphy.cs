
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> Vinhui
 *  @version >> 02.00.00
 *  @release >> 06.06.16
 *  @licence >> MIT
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;

namespace IRC
{
    public static class Giphy
    {
        static string apiKey = "dc6zaTOxFJmzC";
        static JavaScriptSerializer serializer = new JavaScriptSerializer();

        #region

        /**
         *  This event is called whenever someone, either in a private query
         *  or in a channel, calls up the bot, with any command that matches
         *  this modules synonyms. Any remaining part of the message is then
         *  delivered here along with the room to reply to, and the user who
         *  called it. In order to send out a message back out, then use the
         *  syntax of 'Anxious.Send(<target>, <message>)'. If the message is
         *  supposed to be send in private then insert the user parameter in
         *  the target, else insert the rooms parameter. In the message part
         *  please keep the replies short to avoid sending too much flooding
         */
        public static void OnMessage(string user, string room, string text)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(WebUtility.UrlEncode("http://api.giphy.com/v1/gifs/random?tag=" + text + "&api_key=" + apiKey));

                request.AllowAutoRedirect = false;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                var headers = response.Headers;

                response.Close();

                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
                {
                    var responseText = reader.ReadToEnd();
                    var responseObject = serializer.DeserializeObject(responseText) as Dictionary<string, Dictionary<string, string>>;

                    var outputText = responseObject["data"]["image_url"];

                    Anxious.Send(room, outputText);
                }
            }
            catch
            {

            }
        }

        #endregion
    }
}
