using MorpionGame.Dtos;
using MorpionGame.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace MorpionGame.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private GameStatus _gameStatus = GameStatus.NotStarted;

        private Color _playerColor = Color.Red;

        private Color _iAColor = Color.Blue;

        private bool _isPlayerToGame = true;

        private GameGrid _gameGrid;

        private int _playerScore = 0;

        private int _iAScore = 0;

        private int _iADifficulty = 1;

        private Color _defaultColor;

        public GameViewModel(Color color)
        {
            _defaultColor = color;
        }

        public int IADifficulty 
        { 
            get 
            { 
                return _iADifficulty; 
            } 
            set 
            {
                if (_iADifficulty == value) return;

                _iADifficulty = value;
                OnPropertyChanged(nameof(IADifficulty));
            } 
        }

        public int PlayerScore
        {
            get => _playerScore;
            set
            {
                if (_playerScore == value) return;

                _playerScore = value;
                OnPropertyChanged(nameof(PlayerScore));
            }
        }
        public int IAScore
        {
            get => _iAScore;
            set
            {
                if (_iAScore == value) return;

                _iAScore = value;
                OnPropertyChanged(nameof(IAScore));
            }
        }
        public bool IsPlayerToGame { get => _isPlayerToGame; set => _isPlayerToGame = value; }



        public GameGrid GameGrid => _gameGrid;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

        public void PlayIATurn()
        {
            (int heuristicValue, GameGridCell nextMoveCell) = GetBestCellToMove(_gameGrid, null, _iADifficulty, false);

            if(nextMoveCell != null) _gameGrid.Cells.Find(c => c.X == nextMoveCell.X && c.Y == nextMoveCell.Y).View.BackgroundColor = _iAColor;
        }

        public (int heuristicValue, GameGridCell bestMoveCell) GetBestCellToMove(GameGrid gameGrid, GameGridCell currentCell, int depth, bool isPlayer)
        {
            var winner = GetWinnerPlayerColor();

            if (winner == _playerColor) return (-10, currentCell);

            if (winner == _iAColor) return (10, currentCell);

            if (depth == 0 || !gameGrid.Cells.Any(c => c.View.BackgroundColor == _defaultColor)) return (0, currentCell);

            int currentHeuristicValue = isPlayer ? 10 : -10;
            var availableCells = gameGrid.Cells.Where(c => c.View.BackgroundColor == _defaultColor);
            GameGridCell currentBestMoveCell = currentCell;
            var debug = new List<string>();
            foreach (var cell in availableCells)
            {
                cell.View.BackgroundColor = isPlayer ? _playerColor : _iAColor;
                (int heuristicValue, GameGridCell nextMoveCell) = GetBestCellToMove(gameGrid, cell, depth - 1, !isPlayer);

                if (currentBestMoveCell == null)
                {
                    currentBestMoveCell = cell;
                }

                if (isPlayer)
                {
                    if (currentHeuristicValue > heuristicValue)
                    {
                        currentHeuristicValue = heuristicValue;
                        currentBestMoveCell = cell;
                    }
                }
                else
                {
                    if (currentHeuristicValue < heuristicValue)
                    {
                        currentHeuristicValue = heuristicValue;
                        currentBestMoveCell = cell;
                    }
                }

                cell.View.BackgroundColor = _defaultColor;
            }

            return (currentHeuristicValue, currentBestMoveCell);
        }

        public void UpdateWinnerScore(Color winnerColor)
        {
            if (winnerColor == _playerColor)
                PlayerScore++;

            if (winnerColor == _iAColor)
                IAScore++;
        }

        public void SwitchPlayer() => IsPlayerToGame = !IsPlayerToGame;

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
            return IsPlayerToGame ? _playerColor : _iAColor;
        }

        public void ResetGrid()
        {
            _gameGrid.ResetCellsToDefaultColor();
        }
    }
}
