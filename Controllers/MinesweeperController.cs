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
                    int n = gameBoard.Grid[row, col].LiveNeighbors;
                    User.Add(new ButtonModel(row, col, 0, n));
                }
            }
            return View("Index", User);
        }

        public IActionResult HandleButtonClick(string buttonNumber)
        {
            int bN = int.Parse(buttonNumber);

            if (User.ElementAt(bN).ButtonState <= 1)
            {
                // if the tile is at the default button state then place the flag
                if (User.ElementAt(bN).ButtonState == 0)
                {
                    User.ElementAt(bN).ButtonState = (User.ElementAt(bN).ButtonState + 1);
                }
                else
                {
                    // if the tile has already been flagged
                    // set the button state equal to the number of neighbors (the number of bomb tiles it is touching)
                    // 2 is added to this value to compensate for unchecked at 0, and flagged at 1
                    // example: if the tile has NumberOfNeighbors of 1 the buttonState would be set to 3 which displays the 1.png image
                    User.ElementAt(bN).ButtonState = (User.ElementAt(bN).NumberOfNeighbors + 2);

                    // perform flood fill action if the space has no dangerous neighbors
                    if (User.ElementAt(bN).NumberOfNeighbors == 0)
                    {
                        int r = User.ElementAt(bN).Row;
                        int c = User.ElementAt(bN).Col;
                        floodFill(r, c);
                    }
                }
            }
            return View("Index", User);
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
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r - 1 && User.ElementAt(i).Col == c) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r - 1, c)) // NE
                {
                    if (gameBoard.Grid[r - 1, c].LiveNeighbors == 0) floodFill(r - 1, c + 1);
                    else gameBoard.Grid[r - 1, c].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r - 1 && User.ElementAt(i).Col == c) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r, c + 1)) // E
                { 
                    if (gameBoard.Grid[r, c + 1].LiveNeighbors == 0) floodFill(r, c + 1); 
                    else gameBoard.Grid[r, c + 1].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r && User.ElementAt(i).Col == c + 1) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r - 1, c)) // SE
                {
                    if (gameBoard.Grid[r - 1, c].LiveNeighbors == 0) floodFill(r - 1, c + 1);
                    else gameBoard.Grid[r - 1, c].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r - 1 && User.ElementAt(i).Col == c) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r + 1, c)) // S
                { 
                    if (gameBoard.Grid[r + 1, c].LiveNeighbors == 0) floodFill(r + 1, c); 
                    else gameBoard.Grid[r + 1, c].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r + 1 && User.ElementAt(i).Col == c) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r - 1, c)) // SW
                {
                    if (gameBoard.Grid[r - 1, c].LiveNeighbors == 0) floodFill(r + 1, c - 1);
                    else gameBoard.Grid[r - 1, c].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r - 1 && User.ElementAt(i).Col == c) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r, c - 1)) // W
                { 
                    if (gameBoard.Grid[r, c - 1].LiveNeighbors == 0) floodFill(r, c - 1); 
                    else gameBoard.Grid[r, c - 1].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r && User.ElementAt(i).Col == c - 1) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r - 1, c)) // NW
                {
                    if (gameBoard.Grid[r - 1, c].LiveNeighbors == 0) floodFill(r - 1, c - 1);
                    else gameBoard.Grid[r - 1, c].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r - 1 && User.ElementAt(i).Col == c) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
            }
        }
    }
}
