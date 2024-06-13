using Microsoft.AspNetCore.Mvc;
using Milestone1_350.Controllers;
using Milestone1_350.Models;

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
                    int n = gameBoard.Grid[row, col].LiveNeighbors;
                    buttons.Add(new ButtonModel(row, col, 0, n));
                }
            }
            return View("Index", buttons);
        }

        public IActionResult HandleButtonClick(string buttonNumber)
        {
            int bN = int.Parse(buttonNumber);

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

                    // perform flood fill action if the space has no dangerous neighbors
                    if (buttons.ElementAt(bN).NumberOfNeighbors == 0)
                    {
                        int r = buttons.ElementAt(bN).Row;
                        int c = buttons.ElementAt(bN).Col;
                        floodFill(r, c);
                    }
                }
            }
            return View("Index", buttons);
        }

        private void floodFill(int r, int c)
        {
            if (!gameBoard.Grid[r, c].Visited && gameBoard.isSafeCell(r, c))
            {
                gameBoard.Grid[r, c].Visited = true;

                if (gameBoard.isSafeCell(r - 1, c)) // N
                {
                    if (gameBoard.Grid[r - 1, c].LiveNeighbors == 0) floodFill(r - 1, c);
                    else gameBoard.Grid[r - 1, c].Visited = true;
                    for (int i = 0; i < buttons.Count; i++) { if (buttons.ElementAt(i).Row == r - 1 && buttons.ElementAt(i).Col == c) { buttons.ElementAt(i).ButtonState = buttons.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r - 1, c)) // NE
                {
                    if (gameBoard.Grid[r - 1, c].LiveNeighbors == 0) floodFill(r - 1, c + 1);
                    else gameBoard.Grid[r - 1, c].Visited = true;
                    for (int i = 0; i < buttons.Count; i++) { if (buttons.ElementAt(i).Row == r - 1 && buttons.ElementAt(i).Col == c) { buttons.ElementAt(i).ButtonState = buttons.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r, c + 1)) // E
                {
                    if (gameBoard.Grid[r, c + 1].LiveNeighbors == 0) floodFill(r, c + 1);
                    else gameBoard.Grid[r, c + 1].Visited = true;
                    for (int i = 0; i < buttons.Count; i++) { if (buttons.ElementAt(i).Row == r && buttons.ElementAt(i).Col == c + 1) { buttons.ElementAt(i).ButtonState = buttons.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r - 1, c)) // SE
                {
                    if (gameBoard.Grid[r - 1, c].LiveNeighbors == 0) floodFill(r - 1, c + 1);
                    else gameBoard.Grid[r - 1, c].Visited = true;
                    for (int i = 0; i < buttons.Count; i++) { if (buttons.ElementAt(i).Row == r - 1 && buttons.ElementAt(i).Col == c) { buttons.ElementAt(i).ButtonState = buttons.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r + 1, c)) // S
                {
                    if (gameBoard.Grid[r + 1, c].LiveNeighbors == 0) floodFill(r + 1, c);
                    else gameBoard.Grid[r + 1, c].Visited = true;
                    for (int i = 0; i < buttons.Count; i++) { if (buttons.ElementAt(i).Row == r + 1 && buttons.ElementAt(i).Col == c) { buttons.ElementAt(i).ButtonState = buttons.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r - 1, c)) // SW
                {
                    if (gameBoard.Grid[r - 1, c].LiveNeighbors == 0) floodFill(r + 1, c - 1);
                    else gameBoard.Grid[r - 1, c].Visited = true;
                    for (int i = 0; i < buttons.Count; i++) { if (buttons.ElementAt(i).Row == r - 1 && buttons.ElementAt(i).Col == c) { buttons.ElementAt(i).ButtonState = buttons.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r, c - 1)) // W
                {
                    if (gameBoard.Grid[r, c - 1].LiveNeighbors == 0) floodFill(r, c - 1);
                    else gameBoard.Grid[r, c - 1].Visited = true;
                    for (int i = 0; i < buttons.Count; i++) { if (buttons.ElementAt(i).Row == r && buttons.ElementAt(i).Col == c - 1) { buttons.ElementAt(i).ButtonState = buttons.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r - 1, c)) // NW
                {
                    if (gameBoard.Grid[r - 1, c].LiveNeighbors == 0) floodFill(r - 1, c - 1);
                    else gameBoard.Grid[r - 1, c].Visited = true;
                    for (int i = 0; i < buttons.Count; i++) { if (buttons.ElementAt(i).Row == r - 1 && buttons.ElementAt(i).Col == c) { buttons.ElementAt(i).ButtonState = buttons.ElementAt(i).NumberOfNeighbors + 2; } }
                }
            }
        }

        public IActionResult ShowOneButton(int buttonNumber)
        {
            int bN = buttonNumber;

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

                    // perform flood fill action if the space has no dangerous neighbors
                    if (buttons.ElementAt(bN).NumberOfNeighbors == 0)
                    {
                        int r = buttons.ElementAt(bN).Row;
                        int c = buttons.ElementAt(bN).Col;
                        floodFill(r, c);
                    }
                }
            }
            return PartialView("ShowOneButton", buttons.ElementAt(bN));

        }
    }
}
