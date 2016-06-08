
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> llamapixel, SaraDR
 *  @version >> 02.00.00
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
    public static class Title
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
                Match match;
                
                try
                {
                    foreach (string line in File.ReadAllLines("data/titles.txt"))
                    {
                        if ((match = Regex.Match(line, @"(\S+)\s*(.+)", RegexOptions.IgnoreCase)).Success)
                        {
                            titles.Add(match.Groups[1].Value.ToLower(), match.Groups[2].Value);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
        #endregion
        
        #region
            
            public static Dictionary<string, string> titles = new Dictionary<string, string>
            (
                
            );
            
            /**
             *  This checks if a username has some defined title in a dictionary
             *  by first checking if it exists, if it does, then return back the
             *  title found, and if not return back the usernames original input
             */
            public static string GetTitle (string username)
            {
                if (titles.ContainsKey(username.ToLower()))
                {
                    return (titles[username.ToLower()]);
                }
                else
                {
                    return (username);
                }
            }
            
        #endregion
    }
}
