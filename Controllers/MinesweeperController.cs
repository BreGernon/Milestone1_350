using Microsoft.AspNetCore.Mvc;
using Milestone1_350.Controllers;
using Milestone1_350.Models;

namespace Milestone1_350.Controllers
{
    public class MinesweeperController : Controller
    {
        static List<ButtonModel> User = new List<ButtonModel>();
        Random random = new Random();
        const int GRID_SIZE = 36;
        BoardModel gameBoard = HomeController.gameBoard;


        public IActionResult Index()
        {
            User = new List<ButtonModel>();

            for (int row = 0; row < gameBoard.Grid.GetLength(0); row++)
            {
                for (int col = 0; col < gameBoard.Grid.GetLength(1); col++)
                {
                    User.Add(new ButtonModel(row, col, 0, 0));
                }
            }
            return View("Index", User);
        }

        public IActionResult HandleButtonClick(string buttonNumber)
        {
            int bN = int.Parse(buttonNumber);

            User.ElementAt(bN).ButtonState = (User.ElementAt(bN).ButtonState + 1) % 4;

            return View("Index", User);
        }
    }
}
