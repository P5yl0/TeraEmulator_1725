using System;
using System.Collections.Generic;
using Communication.Interfaces;
using Data.Structures.Player;
using Network.Server;

namespace Tera.Services
{
    class EmotionService :  IEmotionService
    {
        public void Action()
        {
            
        }

        public void StartEmotion(Player player, int emoteId)
        {
            player.CurrentEmotion = new KeyValuePair<int, int>(emoteId, (int)DateTime.Now.Ticks);
            Communication.Global.VisibleService.Send(player, new SpCharacterEmotions(player, emoteId));

            //TODO find emotions in SpCharacterInfo
        }
    }
}
