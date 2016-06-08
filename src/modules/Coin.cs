
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> Feyyd, DeeQuation
 *  @version >> 02.00.00
 *  @release >> 31.06.16
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
    public static class Coin
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
                Match match;
                
                int n = 1;
                
                if ((match = Regex.Match(text, @"^(\d{1,6})", RegexOptions.IgnoreCase)).Success)
                {
                    n = Int32.Parse(match.Groups[1].Value);
                }
                
                if (1 <= n && n <= 5)
                {
                    Anxious.Send(room, "Flips <" + Flips(n) + "> for " + Title.GetTitle(user) + ".");
                }
            }
            
        #endregion
        
        #region
            
            public static Random _random;
            
            public static Random  random
            {
                get
                {
                    if (_random == null)
                    {
                        _random = new Random();
                    }
                    
                    return (_random);
                }
            }
            
            /**
             *  Takes in how many flips are desired, returns a list of heads and
             *  tails, in a neatly formatted comma seperated style for the reply
             */
            public static string Flips (int n)
            {
                string result = "";
                
                for (int i = 0; i < n; i++)
                {
                    result = result + (random.Next(0, 2) == 0 ? "Head" : "Tail") + ((i < n - 1) ? ", " : "");
                }
                
                return (result);
            }
            
        #endregion
    }
}
