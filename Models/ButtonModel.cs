namespace Milestone1_350.Models
{
    public class ButtonModel
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int ButtonState { get; set; }
        public int NumberOfNeighbors { get; set; }

        public ButtonModel(int row, int col, int buttonState, int numberOfNeighbors)
        {
            Row = row;
            Col = col;
            ButtonState = buttonState;
            NumberOfNeighbors = numberOfNeighbors;
        }
    }
}