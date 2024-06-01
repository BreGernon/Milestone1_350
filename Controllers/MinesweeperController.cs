using Microsoft.AspNetCore.Mvc;
using Milestone1_350.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Milestone1_350.Controllers
{
    public class MinesweeperController : Controller
    {
        static BoardModel gameBoard;


        public IActionResult InitializeGame(int size = 16, int difficulty = 20)
        {
            gameBoard = new BoardModel(difficulty, size);
            return Ok(gameBoard);
        }


        public IActionResult HandleCellClick(int row, int col)
        {
            if (gameBoard == null) return BadRequest("Game not initialized.");

            var cell = gameBoard.Grid[row, col];
            if (cell.Live)
            {
                // Cell contains a bomb, game over
                return Ok(new { result = "lose", board = gameBoard });
            }

            if (cell.LiveNeighbors == 0)
            {
                // Perform flood fill to reveal connected safe cells
                gameBoard.floodFill(row, col);
            }
            else
            {
                // Mark cell as visited
                cell.Visited = true;
            }

            // Check if all non-bomb cells are visited (win condition)
            if (gameBoard.AllSafeTilesVisited())
            {
                return Ok(new { result = "win", board = gameBoard });
            }

            return Ok(new { result = "continue", board = gameBoard });
        }


        public IActionResult FlagCell(int row, int col)
        {
            if (gameBoard == null) return BadRequest("Game not initialized.");

            var cell = gameBoard.Grid[row, col];
            cell.Flagged = !cell.Flagged;

            return Ok(new { result = "continue", board = gameBoard });
        }
    }
}

