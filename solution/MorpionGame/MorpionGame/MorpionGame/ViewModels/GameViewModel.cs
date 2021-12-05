using MorpionGame.Dtos;
using MorpionGame.Enums;
using System;
using System.Linq;
using Xamarin.Forms;

namespace MorpionGame.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel(Color color)
        {
            _defaultColor = color;
        }

        private Color _defaultColor;

        private int _playerScore { get; set; } = 0;

        private int _iAScore { get; set; } = 0;

        private GameStatus _gameStatus = GameStatus.NotStarted;

        private Color _playerColor = Color.Red;

        private Color _iAColor = Color.Blue;

        private bool _isPlayerToGame = true;

        private GameGrid _gameGrid;

        public void SetStatusGame(GameStatus gameStatus) => _gameStatus = gameStatus;

        public bool IsFinishedGame() => _gameStatus == GameStatus.Finished;

        public void InitGame(Grid grid)
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

        public void SwitchPlayer() => _isPlayerToGame = !_isPlayerToGame;

        public Color GetWinnerPlayerColor()
        {
            if (_gameGrid.Cells[0].View.BackgroundColor == _gameGrid.Cells[1].View.BackgroundColor && _gameGrid.Cells[1].View.BackgroundColor == _gameGrid.Cells[2].View.BackgroundColor)
                return _gameGrid.Cells[0].View.BackgroundColor;

            return _defaultColor;
        }

        public Color GetCurrentColor()
        {
            return _isPlayerToGame ? _playerColor : _iAColor;
        }

        public void ResetGrid()
        {
            _gameGrid.ResetCellsToDefaultColor();
        }
    }
}
