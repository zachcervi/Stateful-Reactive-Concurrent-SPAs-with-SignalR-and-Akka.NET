using System;
using System.Collections.Generic;
using System.Text;

namespace Game.ActorModel.Messages
{
    public class AttackPlayerMessage
    {
        public string PlayerName { get; private set; }
        public AttackPlayerMessage(string playerName)
        {
            PlayerName = PlayerName;
        }

    }
}
