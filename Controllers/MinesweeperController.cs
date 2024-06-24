using Microsoft.AspNetCore.Mvc;
using Milestone1_350.Controllers;
using Milestone1_350.Models;
using Milestone1_350.Services;
using System.Text.Json;

namespace Milestone1_350.Controllers
{
    public class MinesweeperController : Controller
    {
        static List<ButtonModel> User = new List<ButtonModel>();
        Random random = new Random();
        const int GRID_SIZE = 36;
        BoardModel gameBoard = HomeController.gameBoard;
        GameService gs = new GameService();

        public IActionResult Index()
        {
            User = new List<ButtonModel>();

            for (int row = 0; row < gameBoard.Grid.GetLength(0); row++)
            {
                for (int col = 0; col < gameBoard.Grid.GetLength(1); col++)
                {
                    int n = gameBoard.Grid[row, col].LiveNeighbors;
                    User.Add(new ButtonModel(row, col, 0, n));
                }
            }
            return View("Index", User);
        }

        public IActionResult HandleButtonClick(string buttonNumber)
        {
            int bN = int.Parse(buttonNumber);

            // update the button state to show the new image
            if (User.ElementAt(bN).ButtonState == 0)
            {
                gs.revealSquare(User, gameBoard, bN);
            }

            // check if the game is over and display the correct win/lose condition if it is
            if (gs.gameOver(User, gameBoard) == true)
            {
                //return PartialView("_gameover");
                return View("Index", User); // for testing
            }

            return View("Index", User);
        }

        [HttpPost]
        public IActionResult SaveGame()
        {
            var gameState = JsonSerializer.Serialize(User);

            gs.SaveGameState(User);

            return Json(new { success = true, message = "Game saved successfully!" });
        }

        [HttpPost]
        public IActionResult Restart()
        {
            User.Clear();

            // Re-initialize the game board
            gameBoard = new BoardModel(1, 8);

            // Populate the User list with new ButtonModel instances
            for (int row = 0; row < gameBoard.Grid.GetLength(0); row++)
            {
                for (int col = 0; col < gameBoard.Grid.GetLength(1); col++)
                {
                    int n = gameBoard.Grid[row, col].LiveNeighbors;
                    User.Add(new ButtonModel(row, col, 0, n));
                }
            }

            // Return the updated view
            return View("Index", User);
        }
    }
}
