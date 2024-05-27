using System;

namespace Milestone1_350.Models
{
    [Serializable()]
    public class CellModel
    {
        public string CellId { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public bool Visited { get; set; }

        public bool Flagged { get; set; }
        public bool Live { get; set; }
        public int LiveNeighbors { get; set; }

        public CellModel()
        {
            Column = -1;
            Row = -1;
            Visited = false;
            Live = false;
            Flagged = false;
            LiveNeighbors = 0;
        }
        public CellModel(int col, int row, bool visited, bool live, int liveNeighbors)
        {
            Column = col;
            Row = row;
            Visited = visited;
            Live = live;
            Flagged = false;
            LiveNeighbors = liveNeighbors;
        }
    }
}