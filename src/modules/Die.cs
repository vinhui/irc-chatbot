
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> DeeQuation
 *  @version >> 02.00.01
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
    public static class Die
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
                
                int s = 6;
                
                if ((match = Regex.Match(text, @"^(\d{1,6}) ?d ?(\d{1,6})", RegexOptions.IgnoreCase)).Success)
                {
                    n = Int32.Parse(match.Groups[1].Value);
                    
                    s = Int32.Parse(match.Groups[2].Value);
                }
                else
                if ((match = Regex.Match(text, @"^d ?(\d{1,6})", RegexOptions.IgnoreCase)).Success)
                {
                    n = 1;
                    
                    s = Int32.Parse(match.Groups[1].Value);
                }
                
                if (1 <= n && n <= 10 && 2 <= s && s <= 999)
                {
                    Anxious.Send(room, "Rolls <" + Rolls(n, s) + "> for " + Title.GetTitle(user) + ".");
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
             *  This takes in two numbers, first the amount of dice to be rolled
             *  and the second, the sides of each. It then returns the roll list
             */
            public static string Rolls (int n, int s)
            {
                string result = "";
                
                for (int i = 0; i < n; i++)
                {
                    result = result + random.Next(1, s + 1) + ((i < n - 1) ? ", " : "");
                }
                
                return (result);
            }
            
        #endregion
    }
}
