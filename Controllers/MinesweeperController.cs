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
                    int n = random.Next(10);
                    buttons.Add(new ButtonModel(row, col, 0, n));
                }
            }
            return View("Index", buttons);
        }

        public IActionResult HandleButtonClick(string buttonNumber)
        {
            int bN = int.Parse(buttonNumber);

            // only only the user to interact with a tile that is either unclicked or flagged
            if (buttons.ElementAt(bN).ButtonState <= 1) 
            {
                // if the tile is at the default button state then place the flag
                if (buttons.ElementAt(bN).ButtonState == 0)
                {
                    buttons.ElementAt(bN).ButtonState = (buttons.ElementAt(bN).ButtonState + 1);
                }
                else
                {
                    // if the tile has already been flagged
                    // set the button state equal to the number of neighbors (the number of bomb tiles it is touching)
                    // 2 is added to this value to compensate for unchecked at 0, and flagged at 1
                    // example: if the tile has NumberOfNeighbors of 1 the buttonState would be set to 3 which displays the 1.png image
                    buttons.ElementAt(bN).ButtonState = (buttons.ElementAt(bN).NumberOfNeighbors + 2);
                }
                
            }

            return View("Index", buttons);
        }
    }
}