using System;

namespace RpsServer.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public Guid Game { get; set; }

        public Player() { }

        public Player(Guid id)
        {
            this.Id = id;
        }
    }
}
