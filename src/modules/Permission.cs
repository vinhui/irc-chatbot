
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> DeeQuation
 *  @version >> 02.00.00
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
    public static class Permission
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
                    blacklist = File.ReadAllLines("data/blacklist.txt").ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
        #endregion
        
        #region
            
            /**
             *  This checks if a users name appears on a list, of users who have
             *  banned from using the system. It does comparing case insensitive
             */
            public static bool IsBlacklisted (string user)
            {
                return (blacklist.Contains(user, StringComparer.OrdinalIgnoreCase));
            }
            
            public static List<string> blacklist = new List<string>
            {
                
            };
            
        #endregion
    }
}
