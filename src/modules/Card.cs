
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> DeeQuation
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
    public static class Card
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
                if (current == 50)
                {
                    remaining = new string[]
                    {
                        "Ace of clubs",
                        "Two of clubs",
                        "Three of clubs",
                        "Four of clubs",
                        "Five of clubs",
                        "Six of clubs",
                        "Seven of clubs",
                        "Eight of clubs",
                        "Nine of clubs",
                        "Ten of clubs",
                        "Jack of clubs",
                        "Queen of clubs",
                        "King of clubs",
                        
                        "Ace of diamonds",
                        "Two of diamonds",
                        "Three of diamonds",
                        "Four of diamonds",
                        "Five of diamonds",
                        "Six of diamonds",
                        "Seven of diamonds",
                        "Eight of diamonds",
                        "Nine of diamonds",
                        "Ten of diamonds",
                        "Jack of diamonds",
                        "Queen of diamonds",
                        "King of diamonds",
                        
                        "Ace of spades",
                        "Two of spades",
                        "Three of spades",
                        "Four of spades",
                        "Five of spades",
                        "Six of spades",
                        "Seven of spades",
                        "Eight of spades",
                        "Nine of spades",
                        "Ten of spades",
                        "Jack of spades",
                        "Queen of spades",
                        "King of spades",
                        
                        "Ace of hearts",
                        "Two of hearts",
                        "Three of hearts",
                        "Four of hearts",
                        "Five of hearts",
                        "Six of hearts",
                        "Seven of hearts",
                        "Eight of hearts",
                        "Nine of hearts",
                        "Ten of hearts",
                        "Jack of hearts",
                        "Queen of hearts",
                        "King of hearts",
                        
                        "Joker"
                    };
                    
                    remaining = remaining.OrderBy(n => random.Next()).ToArray();
                    
                    current   = 0;
                }
                
                Anxious.Send(room, "Deals a <" + remaining[current++] + "> to " + Title.GetTitle(user) + ".");
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
            
            public static int      current   = 50;
            
            public static string[] remaining = new string[]
            {
                
            };
        #endregion
    }
}
