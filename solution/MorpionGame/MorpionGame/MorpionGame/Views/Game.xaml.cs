using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MorpionGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Game : ContentPage
    {
        public Color PlayerColor { get; set; } = Color.Red;
        public Color IAColor { get; set; } = Color.Blue;
        public Color DefaultColor { get; set; } = Color.White;
        public bool IsPlayerToGame { get; set; } = true;
        public GameGrid GameGrid { get; set; } = new GameGrid();
        public Game()
        {
            InitializeComponent();
            InitGame();
        }

        private void InitGame()
        {
            GameGrid.Status = GameGridStatus.InProgress;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GameGrid.Cells.Add(new GameGridCell()
                    {
                        Y = i,
                        X = j,
                        Id = $"{i}{j}",
                        Color = DefaultColor
                    });
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.BackgroundColor.Equals(DefaultColor))
            {
                button.BackgroundColor = GetCurrentColor();
                UpdateCell(button.AutomationId);
                IsPlayerToGame = !IsPlayerToGame;
            }
        }

        private void UpdateCell(string automationId)
        {
            var cell = GameGrid.Cells.Find(c => c.Id == automationId);
            cell.Color = GetCurrentColor();
        }

        private Color GetCurrentColor()
        {
            return IsPlayerToGame ? PlayerColor : IAColor;
        }
    }
    public class GameGrid
    {
        public List<GameGridCell> Cells { get; set; } = new List<GameGridCell>();
        public GameGridStatus Status { get; set; } = GameGridStatus.NotStarted;
    }
    public enum GameGridStatus
    {
        NotStarted,
        InProgress,
        Finished
    }
    public class GameGridCell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }
        public string Id { get; set; }

        public override string ToString()
        {
            return $"X : {X} "
                + $"Y : {Y} "
                + $"Color : {Color} "
                + $"Id : {Id} ";
        }
    }
}