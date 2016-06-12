
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> DeeQuation, SaraDR
 *  @version >> 02.00.07
 *  @release >> 12.06.16
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
    public class Anxious : Parser
    {
        #region
            
            /**
             *  This method is called when the application starts up and is used
             *  to load in any specific settings that needs to be applied before
             *  any configurations is read, and before any connections happen in
             *  the networking class. It is guarantee to always run as the first
             *  thing in the program and to only ever get called ones throughout
             */
            public override void OnApplicationStart ()
            {
                // -------------------------------------------------------------
                // Permission                                    {blacklist.txt}
                // -------------------------------------------------------------
                
                Permission.OnApplicationStart();
                
                // -------------------------------------------------------------
                // Genre                    {genres.txt, themes.txt, people.txt}
                // -------------------------------------------------------------
                
                Genre.OnApplicationStart();
                
                // -------------------------------------------------------------
                // Joke                                              {jokes.txt}
                // -------------------------------------------------------------
                
                Joke.OnApplicationStart();
                
                // -------------------------------------------------------------
                // Wisdom                                          {sayings.txt}
                // -------------------------------------------------------------
                
                Wisdom.OnApplicationStart();
                
                // -------------------------------------------------------------
                // Title                                            {titles.txt}
                // -------------------------------------------------------------
                
                Title.OnApplicationStart();
                
                // -------------------------------------------------------------
                // Hate                                           {offenses.txt}
                // -------------------------------------------------------------
                
                Hate.OnApplicationStart();
                
                // -------------------------------------------------------------
                // Question                                      {questions.txt}
                // -------------------------------------------------------------
                
                Question.OnApplicationStart();
                
                // -------------------------------------------------------------
                // Remind                                         {messages.txt}
                // -------------------------------------------------------------
                
                Remind.OnApplicationStart();
            }
            
            /**
             *  This method is called only ones, when the program exits, or when
             *  an unhandled exception occours, anywhere in the code. But unlike
             *  the OnApplicationStart method there is no guarantee that this is
             *  actually called, and even more there's no guarantee that it gets
             *  called to an end. This is based on how windows handle exiting of
             *  console applications, using some specific time interval as grace
             */
            public override void OnApplicationClose ()
            {
                // -------------------------------------------------------------
                // Remind                                         {messages.txt}
                // -------------------------------------------------------------
                
                Remind.OnApplicationClose();
            }
            
        #endregion
        
        #region
            
            /**
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             */
            public override void OnPrivateMessage (string user, string room, string text)
            {
                Match match;
                
                if ((match = Regex.Match(text, "^!?" + Network.m_configuration.m_handleCurrent + "[:,> ]+(.+)", RegexOptions.IgnoreCase)).Success)
                {
                    OnMessage(user, room, match.Groups[1].Value);
                }
                else
                if ((match = Regex.Match(text, "^!(.+)", RegexOptions.IgnoreCase)).Success)
                {
                    OnMessage(user, room, match.Groups[1].Value);
                }
                else
                {
                    OnMessage(user, room, text);
                }
            }
            
            /**
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             */
            public override void OnChannelMessage (string user, string room, string text)
            {
                Match match;
                
                if ((match = Regex.Match(text, "^!?" + Network.m_configuration.m_handleCurrent + "[:,> ]+(.+)", RegexOptions.IgnoreCase)).Success)
                {
                    OnMessage(user, room, match.Groups[1].Value);
                }
                else
                if ((match = Regex.Match(text, "^!(.+)", RegexOptions.IgnoreCase)).Success)
                {
                    OnMessage(user, room, match.Groups[1].Value);
                }
                else
                {
                    Header.OnMessage(user, room ,text);
                }
            }
            
        #endregion
        
        #region
            
            /**
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             */
            public override void OnUserJoin (string user, string room)
            {
                // -------------------------------------------------------------
                // Remind                                                     {}
                // -------------------------------------------------------------
                
                Remind.OnUserJoin(user, room);
            }
            
            /**
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             */
            public override void OnUserPart (string user, string room)
            {
                // -------------------------------------------------------------
                // Hate                                                       {}
                // -------------------------------------------------------------
                
                Hate.OnUserPart(user, room);
            }
            
        #endregion
        
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
                
                // -------------------------------------------------------------
                // Permission                                                 {}
                // -------------------------------------------------------------
                
                if (Permission.IsBlacklisted(user))
                {
                    return;
                }
                
                // -------------------------------------------------------------
                // Die                           {die, dice, roll, rolls, throw}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(die|dice|roll|rolls|throw)[\s]*(.*)$", RegexOptions.IgnoreCase)).Success)
                {
                    Die.OnMessage
                    (
                        user,
                        room,
                        match.Groups[2].Value
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Remind                           {remind, inform, notify, ps}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(remind|inform|notify|ps)[\s]+(\S+.*)", RegexOptions.IgnoreCase)).Success)
                {
                    Remind.OnMessage
                    (
                        user,
                        room,
                        match.Groups[2].Value
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Coin                            {coin, coinflip, coins, flip}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(coin|coinflip|coins|flip)\s*(\d{0,5})$", RegexOptions.IgnoreCase)).Success)
                {
                    Coin.OnMessage
                    (
                        user,
                        room,
                        match.Groups[2].Value
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Card                                      {card, cards, draw}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(card|cards|draw)(.*)$", RegexOptions.IgnoreCase)).Success)
                {
                    Card.OnMessage
                    (
                        user,
                        room,
                        match.Groups[2].Value
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Google                         {google, search, lookup, find}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(google|search|lookup|find)[\s]+(\S+.*)", RegexOptions.IgnoreCase)).Success)
                {
                    Google.OnMessage
                    (
                        user,
                        room,
                        match.Groups[2].Value
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Metacritic         {metacritic, score, meta, critic, ranking}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(metacritic|score|meta|critic|ranking)[\s]+(\S+.*)", RegexOptions.IgnoreCase)).Success)
                {
                    Metacritic.OnMessage
                    (
                        user,
                        room,
                        match.Groups[2].Value
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // News                                              {bbc, news}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(bbc|news)[\s]+(\S+.*)", RegexOptions.IgnoreCase)).Success)
                {
                    News.OnMessage
                    (
                        user,
                        room,
                        match.Groups[2].Value
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Stackoverflow                {question, stack, stackoverflow}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(question|stack|stackoverflow)[\s]+(\S+.*)", RegexOptions.IgnoreCase)).Success)
                {
                    Stackoverflow.OnMessage
                    (
                        user,
                        room,
                        match.Groups[2].Value
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Reddit                                               {reddit}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(reddit)[\s]+(\S+.*)", RegexOptions.IgnoreCase)).Success)
                {
                    Reddit.OnMessage
                    (
                        user,
                        room,
                        match.Groups[2].Value
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Tumblr                                      {tumblr, tumbler}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(tumblr|tumbler)[\s]+(\S+.*)", RegexOptions.IgnoreCase)).Success)
                {
                    Tumblr.OnMessage
                    (
                        user,
                        room,
                        match.Groups[2].Value
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Wikipedia                       {wiki, wikipedia, definition}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(wiki|wikipedia|definition)[\s]+(\S+.*)", RegexOptions.IgnoreCase)).Success)
                {
                    Wikipedia.OnMessage
                    (
                        user,
                        room,
                        match.Groups[2].Value
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Youtube                                      {youtube, video}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(youtube|video)[\s]+(\S+.*)", RegexOptions.IgnoreCase)).Success)
                {
                    Youtube.OnMessage
                    (
                        user,
                        room,
                        match.Groups[2].Value
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Joke                              {joke, jokes, humor, funny}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(joke|jokes|humor|funny)", RegexOptions.IgnoreCase)).Success)
                {
                    Joke.OnMessage
                    (
                        user,
                        room,
                        text
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Genre                             {genre, idea, design, game}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(genre|idea|design|game)", RegexOptions.IgnoreCase)).Success)
                {
                    Genre.OnMessage
                    (
                        user,
                        room,
                        text
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Wisdom                              {wisdom, chinese, advice}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(wisdom|chinese|advice)\s*(.*)", RegexOptions.IgnoreCase)).Success)
                {
                    Wisdom.OnMessage
                    (
                        user,
                        room,
                        match.Groups[2].Value
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Fallout4                         {fallout, fallout4, fo4, fo}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(fallout|fallout4|fo4|fo)\s*$", RegexOptions.IgnoreCase)).Success)
                {
                    Fallout4.OnMessage
                    (
                        user,
                        room,
                        text
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Question {does, is, were, are, will, can, have, am, was, ...}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(does|is|were|are|will|can|have|am|was|did|should|do|has)\s(.*)", RegexOptions.IgnoreCase)).Success)
                {
                    Question.OnMessage
                    (
                        user,
                        room,
                        text
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Cat                                               {cat, cats}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(cat|cats)(.*)", RegexOptions.IgnoreCase)).Success)
                {
                    Cat.OnMessage
                    (
                        user,
                        room,
                        text
                    );
                    
                    return;
                }
                
                // -------------------------------------------------------------
                // Help     {help, command, commands, overview, credits, manual}
                // -------------------------------------------------------------
                
                if ((match = Regex.Match(text, @"^(help|command|commands|overview|credits|manual)", RegexOptions.IgnoreCase)).Success)
                {
                    Help.OnMessage
                    (
                        user,
                        room,
                        text
                    );
                    
                    return;
                }
            }
            
            /**
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
             */
            public static void Send (string room, string text, bool priority = false)
            {
                Network.Send("PRIVMSG " + room + " :>> " + text, priority);
            }
            
        #endregion
    }
}
