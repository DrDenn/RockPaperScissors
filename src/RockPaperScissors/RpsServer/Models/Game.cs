using System;

namespace RpsServer.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid Player1 { get; set; }
        public PlayerState player1State { get; set; }
        public Guid Player2 { get; set; }
        public PlayerState player2State { get; set; }

        //public bool IsActive
        //{
        //    get { return Player2 != null && Player2.Id != default(Guid); }
        //}

        //public bool IsPlaying(Guid playerId)
        //{
        //    if(Player1.Id == playerId || Player2.Id == playerId)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //public Player GetPlayer(Guid playerId)
        //{
        //    if (Player1.Id == playerId)
        //    {
        //        return Player1;
        //    }

        //    if (Player2.Id == playerId)
        //    {
        //        return Player2;
        //    }

        //    return null;
        //}

        //public bool IsWaiting
        //{
        //    get { return Player1.State == PlayerState.Waiting || Player2.State == PlayerState.Waiting; }
        //}

        //public Guid Winner
        //{
        //    get
        //    {
        //        if (Player1 == default(Guid) || Player2 == default(Guid))
        //        {
        //            return default(Guid);
        //        }

        //        int diff = (Player1.State - Player2.State);

        //        if (diff == 0)
        //        {
        //            return default(Guid);
        //        }

        //        if (diff == 1 || diff == -2)
        //        {
        //            return Player1.Id;
        //        }

        //        return Player2.Id;
        //    }
        //}
    }
}
