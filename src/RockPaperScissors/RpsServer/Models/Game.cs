using System;

namespace RpsServer.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid Player1 { get; set; }
        public PlayerState Player1State { get; set; }
        public Guid Player2 { get; set; }
        public PlayerState Player2State { get; set; }
    }
}
