
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> DeeQuation
 *  @version >> 02.00.00
 *  @release >> 29.05.16
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
    public static class Wikipedia
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
                Anxious.Send(room, "https://simple.wikipedia.org/w/index.php?search=" + WebUtility.UrlEncode(text));
            }
            
        #endregion
    }
}
