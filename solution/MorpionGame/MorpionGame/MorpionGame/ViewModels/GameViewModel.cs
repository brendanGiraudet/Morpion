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

        public int PlayerScore { get; set; } = 0;

        public int IAScore { get; set; } = 0;

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

            PlayerScore = 0;

            IAScore = 0;

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

        public void UpdateWinnerScore(Color winnerColor)
        {
            if (winnerColor == _playerColor)
                PlayerScore++;
            
            if (winnerColor == _iAColor)
                IAScore++;
        }

        public void SwitchPlayer() => _isPlayerToGame = !_isPlayerToGame;

        public Color GetWinnerPlayerColor()
        {
            if (IsWinner(_iAColor))
                return _iAColor;
            
            if (IsWinner(_playerColor))
                return _playerColor;

            return _defaultColor;
        }
        
        public bool IsWinner(Color winnerColor)
        {
            // First row
            if (_gameGrid.Cells[0].View.BackgroundColor == winnerColor && _gameGrid.Cells[0].View.BackgroundColor == _gameGrid.Cells[1].View.BackgroundColor && _gameGrid.Cells[1].View.BackgroundColor == _gameGrid.Cells[2].View.BackgroundColor)
                return true;
            
            // Second row
            if (_gameGrid.Cells[3].View.BackgroundColor == winnerColor && _gameGrid.Cells[3].View.BackgroundColor == _gameGrid.Cells[4].View.BackgroundColor && _gameGrid.Cells[4].View.BackgroundColor == _gameGrid.Cells[5].View.BackgroundColor)
                return true;
            
            // Third row
            if (_gameGrid.Cells[6].View.BackgroundColor == winnerColor && _gameGrid.Cells[6].View.BackgroundColor == _gameGrid.Cells[7].View.BackgroundColor && _gameGrid.Cells[7].View.BackgroundColor == _gameGrid.Cells[8].View.BackgroundColor)
                return true;
            
            // First column
            if (_gameGrid.Cells[0].View.BackgroundColor == winnerColor && _gameGrid.Cells[0].View.BackgroundColor == _gameGrid.Cells[3].View.BackgroundColor && _gameGrid.Cells[3].View.BackgroundColor == _gameGrid.Cells[6].View.BackgroundColor)
                return true;
            
            // Second column
            if (_gameGrid.Cells[1].View.BackgroundColor == winnerColor && _gameGrid.Cells[1].View.BackgroundColor == _gameGrid.Cells[4].View.BackgroundColor && _gameGrid.Cells[4].View.BackgroundColor == _gameGrid.Cells[7].View.BackgroundColor)
                return true;
            
            // Third column
            if (_gameGrid.Cells[2].View.BackgroundColor == winnerColor && _gameGrid.Cells[2].View.BackgroundColor == _gameGrid.Cells[5].View.BackgroundColor && _gameGrid.Cells[5].View.BackgroundColor == _gameGrid.Cells[8].View.BackgroundColor)
                return true;
            
            // First diagonal
            if (_gameGrid.Cells[0].View.BackgroundColor == winnerColor && _gameGrid.Cells[0].View.BackgroundColor == _gameGrid.Cells[4].View.BackgroundColor && _gameGrid.Cells[4].View.BackgroundColor == _gameGrid.Cells[8].View.BackgroundColor)
                return true;

            // Second diagonal
            if (_gameGrid.Cells[2].View.BackgroundColor == winnerColor && _gameGrid.Cells[2].View.BackgroundColor == _gameGrid.Cells[4].View.BackgroundColor && _gameGrid.Cells[4].View.BackgroundColor == _gameGrid.Cells[6].View.BackgroundColor)
                return true;

            return false;
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
