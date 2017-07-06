using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RpsServer.Models;

namespace RpsServer.Controllers
{
    [Route("api/[controller]")]
    public class RpsController : Controller
    {
        private readonly GameContext context;

        public RpsController(GameContext context)
        {
            this.context = context;
        }

        #region APIs
        [HttpGet]
        public IActionResult CreateUser()
        {
            User newUser = new User();
            this.context.Users.Add(newUser);
            this.context.SaveChanges();
            return new ObjectResult(newUser.Id);
        }

        [HttpGet("{playerId}")]
        public IActionResult GetOrCreateGame(Guid playerId)
        {
            // Assumption: Existing game with this player doesn't exist
            Game game = this.AddOrUpdateGame(playerId);
            Player player = this.AddOrUpdatePlayer(playerId, game.Id);
            this.context.SaveChanges();
            return new OkResult();
        }

        [HttpGet("status/{playerId}")]
        public IActionResult CheckStatus(Guid playerId)
        {
            Player player = this.context.Players.Find(playerId);
            Game game = this.context.Games.Find(player.Game);
            return this.GetGameStatus(game, playerId);
        }

        [HttpPost("{playerId}/{move}")]
        public IActionResult MakeMove(Guid playerId, PlayerState move)
        {
            if (move == PlayerState.Waiting)
            {
                return new BadRequestResult();
            }

            Player player = this.context.Players.Find(playerId);
            Game game = this.context.Games.Find(player.Game);

            this.UpdatePlayerMove(game, player, move);

            return this.GetGameStatus(game, playerId);
        }
        #endregion

        #region GetOrCreateGame
        private Player AddOrUpdatePlayer(Guid playerId, Guid gameId)
        {
            Player player = this.GetPlayerById(playerId);

            if (player == null)
            {
                player = this.CreatePlayer(playerId);
                player.Game = gameId;
                this.context.Players.Add(player);
            }
            else
            {
                player.Game = gameId;
                this.context.Players.Update(player);
            }

            return player;
        }
        private Player GetPlayerById(Guid playerId)
        {
            return this.context.Players.FirstOrDefault(cur => cur.Id == playerId);
        }

        private Player CreatePlayer(Guid id)
        {
            return new Player(id);
        }

        private Game GetOpenGame()
        {
            return this.context.Games.FirstOrDefault(cur => cur.Player2 == default(Guid));
        }

        private Game CreateGame()
        {
            var game = new Game();
            return game;
        }

        private Game AddPlayerToGame(Game game, Player player)
        {
            if (game.Player1 == default(Guid))
            {
                game.Player2 = player.Id;
            }
            else
            {
                game.Player1 = player.Id;
            }

            return game;
        }

        private void SetPlayer2(Game game, Player player)
        {
            game.Player2 = player.Id;
            this.context.Games.Update(game);
        }

        private Game AddOrUpdateGame(Guid playerId)
        {
            Game game = this.GetOpenGame();

            if (game == null)
            {
                game = CreateGame();
                game.Player1 = playerId;
                this.context.Games.Add(game);
            }
            else
            {
                game.Player2 = playerId;
                this.context.Games.Update(game);
            }

            return game;
        }
        #endregion

        #region PlayGame
        private void UpdatePlayerMove(Game game, Player player, PlayerState move)
        {
            if (player.Id == game.Player1)
            {
                game.Player1State = move;
            }
            else
            {
                game.Player2State = move;
            }

            this.context.Players.Update(player);
            this.context.SaveChanges();
        }

        // Most of these (thise that just use game and player Id) can probably be moved elsewhere (to the Game Model? As Properties?)
        private IActionResult GetGameStatus(Game game, Guid playerId)
        {
            if (this.IsWaiting(game))
            {
                return new ObjectResult("Waiting...");
            }

            if(this.IsDraw(game))
            {
                return new ObjectResult("Draw...");
            }

            if(this.IsWinner(game, playerId))
            {
                return new ObjectResult("Winner!!!");
            }

            return new ObjectResult("Loser...");
        }

        private bool IsWinner(Game game, Guid playerId)
        {
            return this.GetWinner(game) == playerId;
        }

        private bool IsWaiting(Game game)
        {
            return game.Player1State == PlayerState.Waiting || game.Player2State == PlayerState.Waiting;
        }

        private bool IsDraw(Game game)
        {
            return game.Player1State == game.Player2State;
        }

        private Guid GetWinner(Game game)
        {
            int diff = game.Player1State - game.Player2State;

            if (diff == 0)
            {
                return default(Guid);
            }

            return (diff == 1 || diff == -2) ? game.Player1 : game.Player2;
        }
        #endregion PlayGame
    }
}
