using DominoWPF.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DominoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game _game = new();
        private int _currentPlayerIndex = 0;
        private bool _gameRunning = false;

        public MainWindow()
        {
            InitializeComponent();
            UpdateUI();
        }

        // Single Start / Reset button handler
        private void BtnStartReset_Click(object sender, RoutedEventArgs e)
        {
            if (!_gameRunning)
            {
                StartNewGame();
            }
            else
            {
                ResetGame();
            }
        }

        private void StartNewGame()
        {
            _game = new Game();
            var human = new HumanPlayer("You");
            var computer = new ComputerPlayer("Computer");

            _game.AddPlayer(human);
            _game.AddPlayer(computer);

            // Deal initial pieces (3 by default; change if you want)
            _game.DealInitialPieces(3);

            _currentPlayerIndex = 0;
            _gameRunning = true;
            btnStartReset.Content = "Reset Game";
            tbStatus.Text = "Game started.";
            UpdateUI();

            if (_game.Players[_currentPlayerIndex] is ComputerPlayer)
            {
                RunComputerTurnAsync();
            }
            else
            {
                EnableHumanControls(true);
            }
        }

        private void ResetGame()
        {
            _game = new Game();
            _gameRunning = false;
            btnStartReset.Content = "Start Game";
            lbHand.Items.Clear();
            icBoard.Items.Clear();
            lblCurrentPlayer.Text = "";
            tbEnds.Text = "";
            tbStatus.Text = "Reset.";
            EnableHumanControls(false);
        }

        private void UpdateUI()
        {
            // Update board view — add DominoPiece objects (templates bind LeftValue/RightValue)
            icBoard.Items.Clear();
            foreach (var piece in _game.Board.pieces)
            {
                icBoard.Items.Add(piece); // <-- was piece.ToString() before
            }
            tbEnds.Text = $"Ends: {_game.Board.LeftEnd} | {_game.Board.RightEnd}";

            // Current player text
            if (_game.Players.Count > 0)
            {
                var cp = _game.Players[_currentPlayerIndex];
                lblCurrentPlayer.Text = $"Current: {cp.GetName()}";
            }
            else
            {
                lblCurrentPlayer.Text = "";
            }

            // Update human hand (horizontal ListBox) — still adding objects so hand uses template
            lbHand.Items.Clear();
            var human = _game.Players.FirstOrDefault(p => p is HumanPlayer) as HumanPlayer;
            if (human != null)
            {
                foreach (var piece in human.Hand)
                {
                    lbHand.Items.Add(piece);
                }
            }

            // Enable human controls only if it's human's turn and game running
            EnableHumanControls(_gameRunning && _game.Players.Count > 0 && _game.Players[_currentPlayerIndex] is HumanPlayer);
        }

        private void EnableHumanControls(bool enable)
        {
            btnPlay.IsEnabled = enable;
            btnDraw.IsEnabled = enable;
            lbHand.IsEnabled = enable;
        }

        // Check if game is blocked (no boneyard and nobody can move)
        private bool IsGameBlocked()
        {
            if (!_game.DominoSet.IsEmpty()) return false;

            foreach (var player in _game.Players)
            {
                if (player.CanMakeMove(_game.Board))
                {
                    return false;
                }
            }

            return true;
        }

        private void DeclareWinnerByScoreAndStop()
        {
            var winner = _game.Players.OrderBy(p => p.GetScore()).First();
            var scores = string.Join("\n", _game.Players.OrderBy(p => p.GetScore()).Select(p => $"{p.GetName()}: {p.GetScore()}"));
            var message = $"Game over (blocked).\nWinner: {winner.GetName()} with lowest score: {winner.GetScore()}\n\nFinal scores:\n{scores}";
            tbStatus.Text = $"🏆 {winner.GetName()} wins with lowest score: {winner.GetScore()}";
            MessageBox.Show(this, message, "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
            EnableHumanControls(false);
            _gameRunning = false;
            btnStartReset.Content = "Start Game";
        }

        private async void RunComputerTurnAsync()
        {
            if (!_gameRunning) return;

            EnableHumanControls(false);
            tbStatus.Text = "Computer is thinking...";
            await Task.Run(() =>
            {
                var cp = _game.Players[_currentPlayerIndex] as ComputerPlayer;
                if (cp == null) return;
                var played = cp.MakeMove(_game.Board);

                Dispatcher.Invoke(() =>
                {
                    if (played != null)
                    {
                        try
                        {
                            _game.Board.PlacePiece(played);
                            tbStatus.Text = $"{cp.GetName()} played {played}";
                        }
                        catch (InvalidOperationException ex)
                        {
                            cp.AddPiece(played);
                            tbStatus.Text = $"Invalid move by {cp.GetName()}: {ex.Message}";
                        }
                    }
                    else
                    {
                        var drawn = _game.DominoSet.DrawPiece();
                        if (drawn != null)
                        {
                            cp.AddPiece(drawn);
                            tbStatus.Text = $"{cp.GetName()} drew a piece.";
                        }
                        else
                        {
                            tbStatus.Text = $"{cp.GetName()} passes (no pieces to draw).";
                        }
                    }

                    // Immediate winner
                    if (!cp.HasPieces())        
                    {
                        tbStatus.Text = $"{cp.GetName()} wins!";
                        EnableHumanControls(false);
                        UpdateUI();
                        _gameRunning = false;
                        btnStartReset.Content = "Start Game";
                        return;
                    }

                    // Blocked game detection
                    if (IsGameBlocked())
                    {
                        DeclareWinnerByScoreAndStop();
                        UpdateUI();
                        return;
                    }

                    // Advance turn
                    _currentPlayerIndex = (_currentPlayerIndex + 1) % _game.Players.Count;
                    UpdateUI();

                    if (_game.Players[_currentPlayerIndex] is ComputerPlayer)
                    {
                        RunComputerTurnAsync();
                    }
                    else
                    {
                        EnableHumanControls(true);
                    }
                });
            });
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (!_gameRunning) return;

            var human = _game.Players.FirstOrDefault(p => p is HumanPlayer) as HumanPlayer;
            if (human == null) return;
            if (lbHand.SelectedIndex < 0)
            {
                tbStatus.Text = "Select a piece to play.";
                return;
            }

            var index = lbHand.SelectedIndex;
            var piece = human.PlayPieceAt(index);
            if (piece == null) return;

            try
            {
                _game.Board.PlacePiece(piece);
                tbStatus.Text = $"You played {piece}";
            }
            catch (InvalidOperationException ex)
            {
                human.AddPiece(piece);
                tbStatus.Text = $"Invalid play: {ex.Message}";
                UpdateUI();
                return;
            }

            // Immediate winner
            if (!human.HasPieces())
            {
                tbStatus.Text = "You win!";
                EnableHumanControls(false);
                UpdateUI();
                _gameRunning = false;
                btnStartReset.Content = "Start Game";
                return;
            }

            // Check blocked game
            if (IsGameBlocked())
            {
                DeclareWinnerByScoreAndStop();
                UpdateUI();
                return;
            }

            // Advance to computer
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _game.Players.Count;
            UpdateUI();

            if (_game.Players[_currentPlayerIndex] is ComputerPlayer)
            {
                RunComputerTurnAsync();
            }
        }

        private void BtnDraw_Click(object sender, RoutedEventArgs e)
        {
            if (!_gameRunning) return;

            var human = _game.Players.FirstOrDefault(p => p is HumanPlayer) as HumanPlayer;
            if (human == null) return;

            var drawn = _game.DominoSet.DrawPiece();
            if (drawn != null)
            {
                human.AddPiece(drawn);
                tbStatus.Text = $"You drew {drawn}";
                UpdateUI();
            }
            else
            {
                tbStatus.Text = "No pieces left to draw.";
            }

            // After draw, check blocked game
            if (IsGameBlocked())
            {
                DeclareWinnerByScoreAndStop();
                UpdateUI();
                return;
            }

            // Advance to next player
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _game.Players.Count;
            UpdateUI();

            if (_game.Players[_currentPlayerIndex] is ComputerPlayer)
            {
                RunComputerTurnAsync();
            }
        }
    }
}