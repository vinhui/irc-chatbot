
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> Adib, SaraDR
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
    public static class Remind
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
                    foreach (string line in File.ReadAllLines("data/messages.txt"))
                    {
                        if ((match = Regex.Match(line, @"(.*?)\s(.*?)\s(.*)")).Success)
                        {
                            messages.Add(new Message(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value));
                        }
                    }
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
				Match match;
				
                if ((match = Regex.Match(text, @"^(\S*)\s+(\S.*)", RegexOptions.IgnoreCase)).Success)
                {
                    if (match.Groups[1].Value.Equals(Network.m_configuration.m_handleCurrent, StringComparison.OrdinalIgnoreCase))
                    {
                        
                    }
                    else
                    if (match.Groups[1].Value.Equals("nickserv", StringComparison.OrdinalIgnoreCase))
                    {
                        
                    }
                    else
                    if (match.Groups[1].Value.Equals(user, StringComparison.OrdinalIgnoreCase))
                    {
                        messages.Add(new Message(match.Groups[1].Value, user, match.Groups[2].Value));
                        
                        Anxious.Send(room, "You will be notified next time you join the room.");
                    }
                    else
                    {
                        messages.Add(new Message(match.Groups[1].Value, user, match.Groups[2].Value));
                        
                        Anxious.Send(room, Title.GetTitle(match.Groups[1].Value) + " will be notified next time they join the room.");
                    }
                }
            }
            
            /**
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             */
            public static void OnUserJoin (string user, string room)
            {
                for (int i = (messages.Count - 1); (i + 1) > 0; i--)
                {
                    if (messages[i].t.Equals(user, StringComparison.OrdinalIgnoreCase))
                    {
                        if (messages[i].t.Equals(messages[i].f, StringComparison.OrdinalIgnoreCase))
                        {
                            Anxious.Send(room, Title.GetTitle(messages[i].t) + ", You asked me to remind you: " + messages[i].m);
                        }
                        else
                        {
                            Anxious.Send(room, Title.GetTitle(messages[i].t) + ", " + Title.GetTitle(messages[i].f) + " has asked me to remind you: " + messages[i].m);
                        }
                        
                        messages.RemoveAt(i);
                        
                        return;
                    }
                }
            }
            
            /**
             *  This method is called only ones, when the program exits, or when
             *  an unhandled exception occours, anywhere in the code. But unlike
             *  the OnApplicationStart method there is no guarantee that this is
             *  actually called, and even more there's no guarantee that it gets
             *  called to an end. This is based on how windows handle exiting of
             *  console applications, using some specific time interval as grace
             */
            public static void OnApplicationClose ()
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    
                    foreach (Message message in messages)
                    {
                        sb.AppendLine(message.t + "\t" + message.f + "\t" + message.m);
                    }
                    
                    File.WriteAllText("data/messages.txt", sb.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
        #endregion
        
        #region
            
            public static List<Message> messages = new List<Message>
            (
                
            );
            
            public struct Message
            {
                public string t;
                
                public string f;
                
                public string m;
                
                public Message (string t, string f, string m)
                {
                    this.t = t;
                    
                    this.f = f;
                    
                    this.m = m;
                }
            }
            
        #endregion
    }
}
