using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IDictionary<int, Game> _games = new Dictionary<int, Game>();
        private int _nextId = 1;

        // POST /games
        [HttpPost]
        public IActionResult CreateGame()
        {
            int gameId = _nextId++;
            _games[gameId] = new Game();
            return Ok(new { gameId });
        }

        // GET /games/{gameId}
        [HttpGet("{gameId}")]
        public IActionResult GetGame(int gameId)
        {
            if (!_games.ContainsKey(gameId))
            {
                return NotFound();
            }

            Game game = _games[gameId];
            return Ok(game);
        }

        // POST /games/{gameId}/moves
        [HttpPost("{gameId}/moves")]
        public IActionResult MakeMove(int gameId, Move move)
        {
            if (!_games.ContainsKey(gameId))
            {
                return NotFound();
            }

            Game game = _games[gameId];
            bool success = game.MakeMove(move);
            if (!success)
            {
                return BadRequest("Invalid move");
            }

            if (game.IsGameOver)
            {
                return Ok(new { game.Winner });
            }

            return Ok();
        }
       
    }

    public class Game
    {
        private readonly int[,] _board = new int[3, 3];
        private int _nextPlayer = 1;

        public bool IsGameOver { get; private set; }
        public int? Winner { get; private set; }

        public bool MakeMove(Move move)
        {
            if (_board[move.Row, move.Column] != 0)
            {
                return false;
            }

            _board[move.Row, move.Column] = _nextPlayer;
            _nextPlayer = -_nextPlayer;

            CheckForWinner();
            return true;
        }

        private void CheckForWinner()
        {
            // Check rows
            for (int row = 0; row < 3; row++)
            {
                int sum = _board[row, 0] + _board[row, 1] + _board[row, 2];
                if (sum == 3 || sum == -3)
                {
                    IsGameOver = true;
                    Winner = sum / 3;
                    return;
                }
            }

            // Check columns
            for (int column = 0; column < 3; column++)
            {
                int sum = _board[0, column] + _board[1, column] + _board[2, column];
                if (sum == 3 || sum == -3)
                {
                    IsGameOver = true;
                    Winner = sum / 3;
                    return;
                }
            }

            // Check diagonals
            int diagonal1Sum = _board[0, 0] + _board[1, 1] + _board[2, 2];
            int diagonal2Sum = _board[0, 2] + _board[1, 1] + _board[2, 0];
            if (diagonal1Sum == 3 || diagonal1Sum == -3)
            {
                IsGameOver = true;
                Winner = diagonal1Sum / 3;
                return;
            }
            if (diagonal2Sum == 3 || diagonal2Sum == -3)
            {
                IsGameOver = true;
                Winner = diagonal2Sum / 3;
                return;
            }

            // Check for tie
            if (_board.Cast<int>().All(cell => cell != 0))
            {
                IsGameOver = true;
                Winner = null;
            }
        }
    }

    public class Move
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
