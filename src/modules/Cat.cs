
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> Adib
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

namespace IRC
{
    public static class Cat
    {
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
            public static void OnMessage (string user, string room, string text)
            {
                try
                {
                    HttpWebRequest request      = (HttpWebRequest)WebRequest.Create("http://thecatapi.com/api/images/get?format=src&type=gif");
                    
                    request.AllowAutoRedirect   = false;
                    
                    HttpWebResponse response    = (HttpWebResponse)request.GetResponse();
                    
                    string r                    = response.Headers["Location"];
                    
                    response.Close();
                    
                    if (r.Length > 0)
                    {
                        Anxious.Send(room, r);
                    }
                }
                catch
                {
                    
                }
            }
            
        #endregion
    }
}
