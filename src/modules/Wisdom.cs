
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> DeeQuation, SaraDR, Bladesfist
 *  @version >> 02.00.02
 *  @release >> 30.06.16
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
    public static class Wisdom
    {
        #region
            
            /**
             *  This method is called when the application loads up, and is used
             *  for loading up any persistent data, that have been saved between
             *  sessions, or to initialize fields, that are used in the function
             *  runtime. If any text file loading is required, the loading needs
             *  to be wrapped in a try-catch. Furthermore no external loading is
             *  allowed here, so no loading of any resources outside the servers
             */
            public static void OnApplicationStart ()
            {
                try
                {
                    wisdom = File.ReadAllLines("data/sayings.txt").OrderBy(x => random.Next()).ToArray();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
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
                if (wisdom.Length > 0)
                {
                    Match match;
                    
                    if ((match = Regex.Match(text, @"^(\S+)", RegexOptions.IgnoreCase)).Success)
                    {
                        int[] matches = (from current in wisdom.Select((value, index) => new { value, index }) where (current.value.IndexOf(match.Groups[1].Value, StringComparison.OrdinalIgnoreCase) >= 0) select current.index).ToArray();
                        
                        if (matches.Length > 0)
                        {
                            Anxious.Send(room, wisdom[matches[random.Next(0, matches.Length)]]);
                        }
                        else
                        {
                            Anxious.Send(room, wisdom[current++ % wisdom.Length]);
                        }
                    }
                    else
                    {
                        Anxious.Send(room, wisdom[current++ % wisdom.Length]);
                    }
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
            
            public static int       current     = 0;
            
            public static string[]  wisdom      = new string[]
            {
                
            };
            
        #endregion
    }
}
