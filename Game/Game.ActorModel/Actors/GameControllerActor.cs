﻿using Akka.Actor;
using Game.ActorModel.Messages;
using System.Collections.Generic;

namespace Game.ActorModel.Actors
{
    public class GameControllerActor : ReceiveActor
    {
        private readonly Dictionary<string, IActorRef> _players;

        public GameControllerActor()
        {
            _players = new Dictionary<string, IActorRef>();

            Receive<JoinGameMessage>(message => JoinGame(message));

            Receive<AttackPlayerMessage>(message =>
            {
                _players[message.PlayerName].Forward(message);
            });
        }

        private void JoinGame(JoinGameMessage message)
        {
            var playersNeedsCreating = !_players.ContainsKey(message.PlayerName);

            if (playersNeedsCreating)
            {
                IActorRef newPlayerActor = Context.ActorOf(Props.Create(() => new PlayerActor(message.PlayerName)), message.PlayerName);
                _players.Add(message.PlayerName, newPlayerActor);
                
                foreach(var player in _players.Values)
                {
                    player.Tell(new RefreshPlayerStatusMessage(), Sender);
                }
            }
        }
    }
}
