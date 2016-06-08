
/**
 *  @project >> Internet Relay Chat: Chatbot
 *  @authors >> DeeQuation
 *  @version >> 02.00.00
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
    public static class Help
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
                Anxious.Send(room, "Sending a command list in private.");
                
                Anxious.Send(user, "MODULE:                     >> EXAMPLE:                       >> SYNONYMS:");
                Anxious.Send(user, "Coin flipping:              >> !flip<number>                  >> {coin, coinflip, coins, flip}");
                Anxious.Send(user, "Dice rolling:               >> !roll <number>d<sides>         >> {die, dice, roll, rolls, throw}");
                Anxious.Send(user, "Card drawing:               >> !draw                          >> {card, cards, draw}");
                Anxious.Send(user, "Genre generator:            >> !idea                          >> {genre, idea, design, game}");
                Anxious.Send(user, "Joke telling:               >> !joke                          >> {joke, jokes, humor, funny}");
                Anxious.Send(user, "Reminding others:           >> !remind <person> <message>     >> {remind, inform, notify, ps}");
                Anxious.Send(user, "Chinese wisdom:             >> !wisdom                        >> {wisdom, chinese, advice}");
                Anxious.Send(user, "Google searching:           >> !google <sentence>             >> {google, search, lookup, find}");
                Anxious.Send(user, "Youtube search lookup:      >> !youtube <sentence>            >> {youtube, video}");
                
                Anxious.Send(user, "More at: http://hastebin.com/ofogapugot.xml");
            }
            
        #endregion
    }
}
