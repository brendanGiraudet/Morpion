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
        private string _playerScoreLabelText
        {
            get { return "a la vie a l'amour " + _gameViewModel.PlayerScore; }
            set { }
        }
        private string _iAScoreLabelText
        {
            get { return "Score de l'IA : " + _gameViewModel.IAScore; }
            set { }
        } 

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

            var winnerColor = _gameViewModel.GetWinnerPlayerColor();
            if (winnerColor != _defaultColor)
            {
                _gameViewModel.UpdateWinnerScore(winnerColor);
                _gameViewModel.SetStatusGame(GameStatus.Finished);
            }
        }

        private void ResetGrid(object sender, EventArgs e)
        {
            _gameViewModel.SetStatusGame(GameStatus.InProgress);
            _gameViewModel.ResetGrid();
        }
    }
}