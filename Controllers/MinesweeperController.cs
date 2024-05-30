using Microsoft.AspNetCore.Mvc;
using Milestone1_350.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Milestone1_350.Controllers
{
    public class MinesweeperController : Controller
    {
        static List<ButtonModel> buttons = new List<ButtonModel>();
        Random random = new Random();
        const int GRID_SIZE = 36;
        BoardModel gameBoard = HomeController.gameBoard;




        public IActionResult Index()
        {
            buttons = new List<ButtonModel>();

            for (int row = 0; row < gameBoard.Grid.GetLength(0); row++)
            {
                for (int col = 0; col < gameBoard.Grid.GetLength(1); col++)
                {
                    buttons.Add(new ButtonModel(row, col, 0));
                }
            }
            return View("Index", buttons);
        }

        public IActionResult HandleButtonClick(string buttonNumber)
        {
            int bN = int.Parse(buttonNumber);

            buttons.ElementAt(bN).ButtonState = (buttons.ElementAt(bN).ButtonState + 1) % 3;

            return View("Index", buttons);
        }
    }
}