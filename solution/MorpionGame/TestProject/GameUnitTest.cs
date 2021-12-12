using MorpionGame.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using Xunit;

namespace TestProject
{
    public class GameUnitTest
    {
        private Color _defaultColor = Color.White;
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
                grid.Children.Add(new Button() { AutomationId = "00" }, 0, 0);
                grid.Children.Add(new Button() { AutomationId = "01" }, 0, 1);
                grid.Children.Add(new Button() { AutomationId = "02" }, 0, 2);

                // Second row
                grid.Children.Add(new Button() { AutomationId = "10" }, 1, 0);
                grid.Children.Add(new Button() { AutomationId = "11" }, 1, 1);
                grid.Children.Add(new Button() { AutomationId = "12" }, 1, 2);

                // Third row
                grid.Children.Add(new Button() { AutomationId = "20" }, 2, 0);
                grid.Children.Add(new Button() { AutomationId = "21" }, 2, 1);
                grid.Children.Add(new Button() { AutomationId = "22" }, 2, 2);

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
        [MemberData(nameof(GetCustomGridDataTests))]
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
        private static IEnumerable<object> GetCustomGridDataTests()
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
    }
}