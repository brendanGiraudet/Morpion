using System.Collections.Generic;

using Xamarin.Forms;

namespace MorpionGame.Dtos
{
    public class GameGrid
    {
        public List<GameGridCell> Cells { get; set; } = new List<GameGridCell>();
        private Color _defaultColor;

        public GameGrid(Color defaultColor)
        {
            _defaultColor = defaultColor;
        }

        public void ResetCellsToDefaultColor()
        {
            foreach (var cell in Cells)
            {
                cell.View.BackgroundColor = _defaultColor;
            }
        }
    }
}