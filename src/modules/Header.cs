
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> SaraDR
 *  @version >> 02.00.01
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
    public static class Header
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
                
                if ((match = Regex.Match(text, @"((?:https?:\/\/)?www\.\S+\.\S+)", RegexOptions.IgnoreCase)).Success)
                {
                    try
                    {
                        var s = (new WebClient()).DownloadString(match.Groups[1].Value);
                        
                        var r = Regex.Match(s, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
                        
                        if (r.Length > 0)
                        {
                            Anxious.Send(room, WebUtility.HtmlDecode(r), true);
                        }
                    }
                    catch
                    {
                        
                    }
                }
                else
                if ((match = Regex.Match(text, @"(https?:\/\/\S+\.\S+)", RegexOptions.IgnoreCase)).Success)
                {
                    try
                    {
                        var s = (new WebClient()).DownloadString(match.Groups[1].Value);
                        
                        var r = Regex.Match(s, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
                        
                        if (r.Length > 0)
                        {
                            Anxious.Send(room, WebUtility.HtmlDecode(r), true);
                        }
                    }
                    catch
                    {
                        
                    }
                }
            }
            
        #endregion
    }
}
