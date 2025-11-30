using ReDominoWPF.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ReDominoWPF.Pages
{
    public partial class GamePage : Page
    {
        private GameController _gameController;
        private string _playerName;
        private int _initialDominos;
        private IPlayer _humanPlayer;
        private IPlayer _computerPlayer;
        private bool _isGameRunning = true;

        public GamePage(string playerName, int initialDominos)
        {
            InitializeComponent();
            _playerName = playerName;
            _initialDominos = initialDominos;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _gameController = new GameController { InitialDomino = _initialDominos };
                _gameController.StartNewGame(_playerName);

                var players = _gameController.PlayerDominos.Keys.ToList();
                _humanPlayer = players[0];
                _computerPlayer = players[1];
                RefreshUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing game: {ex.Message}", "Initialization Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshUI()
        {
            if (!_isGameRunning) return;

            try
            {
                TurnTextBlock.Text = $"Current Turn: {(_gameController.CurrentPlayerIndex == 0 ? _humanPlayer.Name : _computerPlayer.Name)}";
                GameStatusTextBlock.Text = $"Board ends: {_gameController.LeftEnd} | {_gameController.RightEnd}";

                DrawBoard();
                DrawComputerDominos();
                DrawPlayerDominos();

                if (_gameController.CurrentPlayerIndex == 1)
                {
                    Dispatcher.BeginInvoke(new Action(() => ComputerTurn()),
                        System.Windows.Threading.DispatcherPriority.Normal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating UI: {ex.Message}", "UI Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DrawBoard()
        {
            BoardPanel.Children.Clear();
            if (_gameController?.Board?.dominos == null) return;

            foreach (var domino in _gameController.Board.dominos)
            {
                var dominoControl = CreateDominoControl(domino.A, domino.B);
                BoardPanel.Children.Add(dominoControl);
            }
        }

        private void DrawComputerDominos()
        {
            ComputerDominosPanel.Children.Clear();
            int count = _gameController.PlayerDominos[_computerPlayer].Count;
            for (int i = 0; i < count; i++)
            {
                ComputerDominosPanel.Children.Add(CreateFaceDownDomino());
            }
        }

        private void DrawPlayerDominos()
        {
            PlayerDominosPanel.Children.Clear();
            var playerHand = _gameController.PlayerDominos[_humanPlayer];

            for (int i = 0; i < playerHand.Count; i++)
            {
                var domino = playerHand[i];
                var dominoControl = CreateDominoface(domino.A, domino.B);
                dominoControl.Cursor = Cursors.Hand;
                int index = i;
                dominoControl.MouseLeftButtonDown += (s, e) => PlayerDominoClick(index);
                PlayerDominosPanel.Children.Add(dominoControl);
            }
        }

        private void PlayerDominoClick(int index)
        {
            if (_gameController.CurrentPlayerIndex != 0) return;

            var playerHand = _gameController.PlayerDominos[_humanPlayer];
            if (index < 0 || index >= playerHand.Count) return;

            var domino = playerHand[index];
            if (!_gameController.CanPlacePiece(domino))
            {
                MessageBox.Show("This domino cannot be placed on the board.", "Invalid Move",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _gameController.PlacePiece(domino);
                _gameController.RemovePieceFromPlayer(_humanPlayer, index);

                if (!_gameController.HasPieces(_humanPlayer))
                {
                    ShowWinnerPopup(_humanPlayer.Name);
                    return;
                }

                _gameController.CurrentPlayerIndex = 1;
                RefreshUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Game Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (_gameController.CurrentPlayerIndex != 0) return;

            var drawn = _gameController.Draw();
            if (drawn != null)
            {
                _gameController.AddPieceToPlayer(_humanPlayer, drawn);
            }
            else
            {
                MessageBox.Show("No dominos left to draw.", "Draw Pile Empty", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            _gameController.CurrentPlayerIndex = 1;
            RefreshUI();
        }

        private void ComputerTurn()
        {
            System.Threading.Thread.Sleep(500);

            var computerHand = _gameController.PlayerDominos[_computerPlayer];
            var playablePiece = computerHand.FirstOrDefault(d => _gameController.CanPlacePiece(d));

            if (playablePiece != null)
            {
                try
                {
                    _gameController.PlacePiece(playablePiece);
                    _gameController.RemovePieceFromPlayer(_computerPlayer, computerHand.IndexOf(playablePiece));

                    if (!_gameController.HasPieces(_computerPlayer))
                    {
                        ShowWinnerPopup(_computerPlayer.Name);
                        return;
                    }
                }
                catch
                {
                    var drawn = _gameController.Draw();
                    if (drawn != null)
                        _gameController.AddPieceToPlayer(_computerPlayer, drawn);
                }
            }
            else
            {
                var drawn = _gameController.Draw();
                if (drawn != null)
                    _gameController.AddPieceToPlayer(_computerPlayer, drawn);
            }

            _gameController.CurrentPlayerIndex = 0;
            RefreshUI();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to restart?", "Restart Game",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new LandingPage());
            }
        }

        private void ShowWinnerPopup(string winner)
        {
            _isGameRunning = false;
            var result = MessageBox.Show($"🎉 {winner} wins the game!\n\nWould you like to play again?",
                "Game Over", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new LandingPage());
            }
        }

        private Border CreateDominoControl(int leftValue, int rightValue)
        {
            bool isDouble = leftValue == rightValue;
            double width = isDouble ? 60 : 120;
            double height = isDouble ? 120 : 60;

            var border = new Border
            {
                Width = width,
                Height = height,
                Margin = new Thickness(4),
                Background = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(4)
            };

            var grid = new Grid();

            if (isDouble)
            {
                // Vertical layout for doubles (same value on both sides)
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Pixel) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                var topText = new TextBlock
                {
                    Text = leftValue.ToString(),
                    FontSize = 24,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Colors.Black),
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(topText, 0);

                var divider = new Rectangle
                {
                    Fill = new SolidColorBrush(Colors.Black),
                    Height = 2
                };
                Grid.SetRow(divider, 1);

                var bottomText = new TextBlock
                {
                    Text = rightValue.ToString(),
                    FontSize = 24,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Colors.Black),
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(bottomText, 2);

                grid.Children.Add(topText);
                grid.Children.Add(divider);
                grid.Children.Add(bottomText);
            }
            else
            {
                // Horizontal layout for non-doubles
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Pixel) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                var leftText = new TextBlock
                {
                    Text = leftValue.ToString(),
                    FontSize = 24,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Colors.Black),
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(leftText, 0);

                var divider = new Rectangle
                {
                    Fill = new SolidColorBrush(Colors.Black),
                    Width = 2
                };
                Grid.SetColumn(divider, 1);

                var rightText = new TextBlock
                {
                    Text = rightValue.ToString(),
                    FontSize = 24,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Colors.Black),
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(rightText, 2);

                grid.Children.Add(leftText);
                grid.Children.Add(divider);
                grid.Children.Add(rightText);
            }

            border.Child = grid;
            return border;
        }
        private Border CreateDominoface(int topValue, int bottomValue, bool interactive = true)
        {
            var border = new Border
            {
                Width = 60,
                Height = 120,
                Margin = new Thickness(4),
                Background = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(4)
            };

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Pixel) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            var topText = new TextBlock
            {
                Text = topValue.ToString(),
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Black),
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetRow(topText, 0);

            var divider = new Rectangle
            {
                Fill = new SolidColorBrush(Colors.Black),
                Height = 2
            };
            Grid.SetRow(divider, 1);

            var bottomText = new TextBlock
            {
                Text = bottomValue.ToString(),
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Black),
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetRow(bottomText, 2);

            grid.Children.Add(topText);
            grid.Children.Add(divider);
            grid.Children.Add(bottomText);

            border.Child = grid;
            return border;
        }

        private Border CreateFaceDownDomino()
        {
            var border = new Border
            {
                Width = 60,
                Height = 120,
                Margin = new Thickness(4),
                Background = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(4)
            };

            var pattern = new TextBlock
            {
                Text = "X",
                FontSize = 48,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Black),
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Opacity = 0.3
            };

            border.Child = pattern;
            return border;
        }
    }
}