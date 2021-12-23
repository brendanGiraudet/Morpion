using Xamarin.Forms;

namespace MorpionGame.Dtos
{
    public class GameGridCell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public View View { get; set; }

        public override string ToString()
        {
            return $"X : {X} " +
                $"Y : {Y} " +
                $"Color: {View.BackgroundColor}";
        }
    }
}