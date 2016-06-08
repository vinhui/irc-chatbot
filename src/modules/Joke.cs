
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> DeeQuation
 *  @version >> 02.00.01
 *  @release >> 30.05.16
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
    public static class Joke
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
                    jokes = File.ReadAllLines("data/jokes.txt").OrderBy(x => random.Next()).ToArray();
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
                if (jokes.Length > 0)
                {
                    Anxious.Send(room, jokes[current++ % jokes.Length]);
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
            
            public static string[]  jokes       = new string[]
            {
                
            };
            
        #endregion
    }
}
