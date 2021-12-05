using MorpionGame.Enums;
using MorpionGame.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MorpionGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Game : ContentPage
    {
        private Color _defaultColor = Color.White;

        GameViewModel _gameViewModel;

        public Game()
        {
            InitializeComponent();
            _gameViewModel = new GameViewModel(_defaultColor);
            _gameViewModel.InitGame(grid);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (_gameViewModel.IsFinishedGame()) return;

            var button = sender as Button;
            if (button != null && button.BackgroundColor.Equals(_defaultColor))
            {
                button.BackgroundColor = _gameViewModel.GetCurrentColor();
                _gameViewModel.SwitchPlayer();
            }

            if (_gameViewModel.GetWinnerPlayerColor() != _defaultColor)
            {
                _gameViewModel.SetStatusGame(GameStatus.Finished);
            }
        }

        private void ResetGrid(object sender, EventArgs e)
        {
            _gameViewModel.ResetGrid();
        }
    }
}