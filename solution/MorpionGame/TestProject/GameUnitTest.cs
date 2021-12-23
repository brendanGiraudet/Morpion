using MorpionGame.Dtos;
using MorpionGame.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using Xunit;

namespace TestProject
{
    public class GameUnitTest
    {
        private static Color _defaultColor = Color.White;
        private static Color _playerColor = Color.Red;
        private static Color _iAColor = Color.Blue;
        GameViewModel CreateGameViewModel() => new GameViewModel(_defaultColor);

        static Grid DefaultGrid
        {
            get
            {
                var grid = new Grid();

                // Add rows
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());

                // Add columns
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                // First row
                grid.Children.Add(new Button() { AutomationId = "00", BackgroundColor = _defaultColor }, 0, 0);
                grid.Children.Add(new Button() { AutomationId = "01", BackgroundColor = _defaultColor }, 0, 1);
                grid.Children.Add(new Button() { AutomationId = "02", BackgroundColor = _defaultColor }, 0, 2);

                // Second row
                grid.Children.Add(new Button() { AutomationId = "10", BackgroundColor = _defaultColor }, 1, 0);
                grid.Children.Add(new Button() { AutomationId = "11", BackgroundColor = _defaultColor }, 1, 1);
                grid.Children.Add(new Button() { AutomationId = "12", BackgroundColor = _defaultColor }, 1, 2);

                // Third row
                grid.Children.Add(new Button() { AutomationId = "20", BackgroundColor = _defaultColor }, 2, 0);
                grid.Children.Add(new Button() { AutomationId = "21", BackgroundColor = _defaultColor }, 2, 1);
                grid.Children.Add(new Button() { AutomationId = "22", BackgroundColor = _defaultColor }, 2, 2);

                return grid;
            }
        }

        #region GetWinnerPlayerColor
        [Fact]
        public void ShouldHaveDefaultColorWhenGetWinnerPlayerColorWithNoWinner()
        {
            // Arrange
            var gameViewModel = CreateGameViewModel();
            gameViewModel.InitGame(DefaultGrid);

            var expectedColor = Color.White;

            // Act
            var winnerPlayerColor = gameViewModel.GetWinnerPlayerColor();

            // Assert
            Assert.Equal(expectedColor, winnerPlayerColor);
        }

        [Theory]
        [MemberData(nameof(GetWinnerPlayerColorCustomGridDataTests))]
        public void ShouldHaveWinnerColorWhenGetWinnerPlayerColorWitWinner(Grid customGrid)
        {
            // Arrange
            var expectedColor = Color.Blue;

            var gameViewModel = CreateGameViewModel();
            gameViewModel.InitGame(customGrid);

            // Act
            var winnerPlayerColor = gameViewModel.GetWinnerPlayerColor();

            // Assert
            Assert.Equal(expectedColor, winnerPlayerColor);
        }
        private static IEnumerable<object> GetWinnerPlayerColorCustomGridDataTests()
        {
            var winnerColor = Color.Blue;

            yield return new object[] { GetFirstRowWinnerGrid(winnerColor) };
            yield return new object[] { GetSecondRowWinnerGrid(winnerColor) };
            yield return new object[] { GetThirdRowWinnerGrid(winnerColor) };

            yield return new object[] { GetFirstColumnWinnerGrid(winnerColor) };
            yield return new object[] { GetSecondColumnWinnerGrid(winnerColor) };
            yield return new object[] { GetThirdColumnWinnerGrid(winnerColor) };

            yield return new object[] { GetFirstDiagonalWinnerGrid(winnerColor) };
            yield return new object[] { GetSecondDiagonalWinnerGrid(winnerColor) };
        }

        private static Grid GetFirstRowWinnerGrid(Color winnerColor)
        {
            var customGrid = DefaultGrid;

            customGrid.Children[0].BackgroundColor = winnerColor;

            customGrid.Children[1].BackgroundColor = winnerColor;

            customGrid.Children[2].BackgroundColor = winnerColor;

            return customGrid;
        }

        private static Grid GetSecondRowWinnerGrid(Color winnerColor)
        {
            var customGrid = DefaultGrid;

            customGrid.Children[3].BackgroundColor = winnerColor;

            customGrid.Children[4].BackgroundColor = winnerColor;

            customGrid.Children[5].BackgroundColor = winnerColor;

            return customGrid;
        }

        private static Grid GetThirdRowWinnerGrid(Color winnerColor)
        {
            var customGrid = DefaultGrid;

            customGrid.Children[6].BackgroundColor = winnerColor;

            customGrid.Children[7].BackgroundColor = winnerColor;

            customGrid.Children[8].BackgroundColor = winnerColor;

            return customGrid;
        }

        private static Grid GetFirstColumnWinnerGrid(Color winnerColor)
        {
            var customGrid = DefaultGrid;

            customGrid.Children[0].BackgroundColor = winnerColor;

            customGrid.Children[3].BackgroundColor = winnerColor;

            customGrid.Children[6].BackgroundColor = winnerColor;

            return customGrid;
        }

        private static Grid GetSecondColumnWinnerGrid(Color winnerColor)
        {
            var customGrid = DefaultGrid;

            customGrid.Children[1].BackgroundColor = winnerColor;

            customGrid.Children[4].BackgroundColor = winnerColor;

            customGrid.Children[7].BackgroundColor = winnerColor;

            return customGrid;
        }

        private static Grid GetThirdColumnWinnerGrid(Color winnerColor)
        {
            var customGrid = DefaultGrid;

            customGrid.Children[2].BackgroundColor = winnerColor;

            customGrid.Children[5].BackgroundColor = winnerColor;

            customGrid.Children[8].BackgroundColor = winnerColor;

            return customGrid;
        }

        private static Grid GetFirstDiagonalWinnerGrid(Color winnerColor)
        {
            var customGrid = DefaultGrid;

            customGrid.Children[0].BackgroundColor = winnerColor;

            customGrid.Children[4].BackgroundColor = winnerColor;

            customGrid.Children[8].BackgroundColor = winnerColor;

            return customGrid;
        }

        private static Grid GetSecondDiagonalWinnerGrid(Color winnerColor)
        {
            var customGrid = DefaultGrid;

            customGrid.Children[2].BackgroundColor = winnerColor;

            customGrid.Children[4].BackgroundColor = winnerColor;

            customGrid.Children[6].BackgroundColor = winnerColor;

            return customGrid;
        }

        #endregion

        #region GetBestCellToMove
        [Theory]
        [MemberData(nameof(GetBestCellToMoveCustomGridDataTests))]
        public void ShouldHaveTheRightCells(Grid customGrid, int expectedX, int expectedY, int expectedHeuristicValue, int depth)
        {
            // Arrange
            var gameViewModel = CreateGameViewModel();
            gameViewModel.InitGame(customGrid);

            // Act
            (int heuristicValue, GameGridCell nextMoveCell) = gameViewModel.GetBestCellToMove(gameViewModel.GameGrid, null, depth, false);

            // Assert
            Assert.Equal(expectedX, nextMoveCell.X);
            Assert.Equal(expectedY, nextMoveCell.Y);
            Assert.Equal(expectedHeuristicValue, heuristicValue);
        }

        private static IEnumerable<object> GetBestCellToMoveCustomGridDataTests()
        {
            yield return new object[] { GetFirstSituation(), 2, 1, 10, 1 };
            yield return new object[] { GetSecondSituation(), 2, 1, 0, 2 };
            yield return new object[] { GetThirdSituation(), 0, 1, 0, 3 };
            yield return new object[] { GetForthSituation(), 0, 1, 0, 5 };
            yield return new object[] { GetFifthSituation(), 1, 1, 0, 6 };
        }

        private static Grid GetFirstSituation()
        {
            var customGrid = DefaultGrid;

            customGrid.Children[0].BackgroundColor = _playerColor;
            customGrid.Children[8].BackgroundColor = _playerColor;
            customGrid.Children[3].BackgroundColor = _iAColor;
            customGrid.Children[4].BackgroundColor = _iAColor;

            return customGrid;
        }

        private static Grid GetSecondSituation()
        {
            var customGrid = DefaultGrid;

            customGrid.Children[0].BackgroundColor = _iAColor;
            customGrid.Children[3].BackgroundColor = _playerColor;
            customGrid.Children[4].BackgroundColor = _playerColor;

            return customGrid;
        }

        private static Grid GetThirdSituation()
        {
            var customGrid = DefaultGrid;

            customGrid.Children[0].BackgroundColor = _playerColor;
            customGrid.Children[2].BackgroundColor = _playerColor;
            customGrid.Children[7].BackgroundColor = _playerColor;
            customGrid.Children[1].BackgroundColor = _iAColor;
            customGrid.Children[4].BackgroundColor = _iAColor;
            customGrid.Children[6].BackgroundColor = _iAColor;

            return customGrid;
        }

        private static Grid GetForthSituation()
        {
            var customGrid = DefaultGrid;

            customGrid.Children[0].BackgroundColor = _playerColor;
            customGrid.Children[7].BackgroundColor = _playerColor;
            customGrid.Children[1].BackgroundColor = _iAColor;
            customGrid.Children[4].BackgroundColor = _iAColor;

            return customGrid;
        }

        private static Grid GetFifthSituation()
        {
            var customGrid = DefaultGrid;

            customGrid.Children[0].BackgroundColor = _playerColor;

            return customGrid;
        }
        #endregion
    }
}