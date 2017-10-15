using System;
using System.Collections.Generic;
using System.Text;

namespace RpsContract
{
    public static class GameStatus
    {
        // SKYLER: instead of all these strings, we should send some json info to the client in the future with info (like emojis)
        public static readonly string WaitingForPlayer = "Waiting-Alone";
        public static readonly string WaitingForMoves = "Waiting-BothMoves";
        public static readonly string WaitingForMovesOpponent = "Waiting-OpponentMove";
        public static readonly string WaitingForMovesPlayer = "Waiting-PlayerMove";
        public static readonly string Draw = "GameDone-Draw";
        public static readonly string Winner = "GameDone-Winner";
        public static readonly string Loser = "GameDone-Loser";

        // Haha this is just in case
        public static readonly string Unknown = "Unknown";
    }
}
