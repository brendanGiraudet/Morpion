using MorpionGame.Dtos;
using MorpionGame.Enums;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MorpionGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Game : ContentPage
    {
        private Color _defaultColor = Color.White;

        private int _playerScore { get; set; } = 0;

        private int _iAScore { get; set; } = 0;

        private GameStatus _gameStatus = GameStatus.NotStarted;

        private Color _playerColor = Color.Red;
        
        private Color _iAColor = Color.Blue;
        
        private bool _isPlayerToGame = true;

        private GameGrid _gameGrid;
        
        public Game()
        {
            InitializeComponent();
            
            InitGame();
        }

        public void InitGame()
        {
            _gameStatus = GameStatus.InProgress;

            _playerScore = 0;

            _iAScore = 0;

            _gameGrid = new GameGrid(_defaultColor);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _gameGrid.Cells.Add(new GameGridCell()
                    {
                        Y = i,
                        X = j,
                        View = grid.Children.FirstOrDefault(c => c.AutomationId == $"{i}{j}"),
                    });
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (_gameStatus == GameStatus.Finished) return;

            var button = sender as Button;
            if (button != null && button.BackgroundColor.Equals(_defaultColor))
            {
                button.BackgroundColor = GetCurrentColor();
                _isPlayerToGame = !_isPlayerToGame;
            }
        }

        private Color GetCurrentColor()
        {
            return _isPlayerToGame ? _playerColor : _iAColor;
        }

        private void ResetGrid(object sender, EventArgs e)
        {
            _gameGrid.ResetCellsToDefaultColor();
        }
    }
}