using Milestone1_350.Models;
using System.Data.SqlClient;
using System.Text.Json;

namespace Milestone1_350.Services
{
    public class GameService
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = minesweeper; Integrated Security = True; Connect Timeout = 30;";
        public GameService() { }

        public void SaveGameState(List<ButtonModel> gameState)
        {
            var serializedGameState = JsonSerializer.Serialize(gameState);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO SavedGames (GameState) VALUES (@GameState)", connection);
                command.Parameters.AddWithValue("@GameState", serializedGameState);
                command.ExecuteNonQuery();
            }
        }

        public List<ButtonModel> revealSquare(List<ButtonModel> User, BoardModel gameBoard, int bN)
        {
            // set the button state equal to the number of neighbors (the number of bomb tiles it is touching)
            // 2 is added to this value to compensate for unchecked at 0, and flagged at 1
            // example: if the tile has NumberOfNeighbors of 1 the buttonState would be set to 3 which displays the 1.png image
            User.ElementAt(bN).ButtonState = (User.ElementAt(bN).NumberOfNeighbors + 2);

            // perform flood fill action if the space has no dangerous neighbors
            if (User.ElementAt(bN).NumberOfNeighbors == 0)
            {
                int r = User.ElementAt(bN).Row;
                int c = User.ElementAt(bN).Col;
                floodfill(gameBoard, User, r, c);
            }

            return User;
        }

        public bool gameOver(List<ButtonModel> User, BoardModel gameBoard)
        {
            int safeTiles = 0;

            for (int i = 0; i < User.Count(); i++)
            {
                // if the tile is a bomb and is revealed the game is over
                if (User[i].ButtonState == 11)
                {
                    // reveal all other bombs on the board
                    for (int j = 0; j < User.Count(); j++)
                    {
                        if (User[j].NumberOfNeighbors == 9)
                        {
                            User[j].ButtonState = 11;
                        }
                    }
                    break;
                }

                // if tile is not revealed and is not a bomb then the game isn't over
                if (User[i].ButtonState == 0 && User[i].NumberOfNeighbors != 9)
                {
                    safeTiles++;
                }
            }

            if (safeTiles > 0)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public void floodfill(BoardModel gameBoard, List<ButtonModel> User, int r, int c)
        {
            if (!gameBoard.Grid[r, c].Visited && gameBoard.isSafeCell(r, c))
            {
                gameBoard.Grid[r, c].Visited = true;

                if (gameBoard.isSafeCell(r - 1, c)) // N
                {
                    if (gameBoard.Grid[r - 1, c].LiveNeighbors == 0) floodfill(gameBoard, User, r - 1, c);
                    else gameBoard.Grid[r - 1, c].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r - 1 && User.ElementAt(i).Col == c) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r - 1, c)) // NE
                {
                    if (gameBoard.Grid[r - 1, c].LiveNeighbors == 0) floodfill(gameBoard, User, r - 1, c + 1);
                    else gameBoard.Grid[r - 1, c].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r - 1 && User.ElementAt(i).Col == c) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r, c + 1)) // E
                {
                    if (gameBoard.Grid[r, c + 1].LiveNeighbors == 0) floodfill(gameBoard, User, r, c + 1);
                    else gameBoard.Grid[r, c + 1].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r && User.ElementAt(i).Col == c + 1) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r - 1, c)) // SE
                {
                    if (gameBoard.Grid[r - 1, c].LiveNeighbors == 0) floodfill(gameBoard, User, r - 1, c + 1);
                    else gameBoard.Grid[r - 1, c].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r - 1 && User.ElementAt(i).Col == c) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r + 1, c)) // S
                {
                    if (gameBoard.Grid[r + 1, c].LiveNeighbors == 0) floodfill(gameBoard, User, r + 1, c);
                    else gameBoard.Grid[r + 1, c].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r + 1 && User.ElementAt(i).Col == c) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r - 1, c)) // SW
                {
                    if (gameBoard.Grid[r - 1, c].LiveNeighbors == 0) floodfill(gameBoard, User, r + 1, c - 1);
                    else gameBoard.Grid[r - 1, c].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r - 1 && User.ElementAt(i).Col == c) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r, c - 1)) // W
                {
                    if (gameBoard.Grid[r, c - 1].LiveNeighbors == 0) floodfill(gameBoard, User, r, c - 1);
                    else gameBoard.Grid[r, c - 1].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r && User.ElementAt(i).Col == c - 1) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
                if (gameBoard.isSafeCell(r - 1, c)) // NW
                {
                    if (gameBoard.Grid[r - 1, c].LiveNeighbors == 0) floodfill(gameBoard, User, r - 1, c - 1);
                    else gameBoard.Grid[r - 1, c].Visited = true;
                    for (int i = 0; i < User.Count; i++) { if (User.ElementAt(i).Row == r - 1 && User.ElementAt(i).Col == c) { User.ElementAt(i).ButtonState = User.ElementAt(i).NumberOfNeighbors + 2; } }
                }
            }
        }
    }
}
