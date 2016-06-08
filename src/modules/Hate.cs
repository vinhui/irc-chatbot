
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> SaraDR
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
    public static class Hate
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
                    offenses = File.ReadAllLines("data/offenses.txt").ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
            /**
             *  If the module needs to do anything when a user parts the channel
             *  this method can be used. It gets called whenever a user parts an
             *  observed channel. Modules that don't need to monitor this, won't
             *  be called, and thus this function, can be alltogether be removed
             */
            public static void OnUserPart (string user, string room)
            {
                if (Permission.IsBlacklisted(user))
                {
                    if (offenses.Count > 0)
                    {
                        Anxious.Send(room, offenses[random.Next(0, offenses.Count)].Replace("<name>", user));
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
            
            public static List<string> offenses = new List<string>
            {
                
            };
            
        #endregion
    }
}
