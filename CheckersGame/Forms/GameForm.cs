using CheckersGameLogic.ComputerPlayer;
using CheckersGameForms.Controls;
using System.Windows.Forms;
using CheckersGameLogic;
using System.Drawing;
using System;

namespace CheckersGameForms
{
    partial class GameForm : Form
    {
        private Form m_MenuForm;
        private GameInfo m_GameInfo;
        private GameLogic m_GameLogic;
        private PictureBox[,] m_Pieces;
        private ComputerLogic m_ComputerLogic;

        private BoardPiece selectedPiece = null;
        private BoardPiece targetPiece = null;

        private int m_boardSize;
        private int m_pieceSize;
        private int m_fromX;

        public GameForm(Form i_MenuForm, GameInfo i_GameInfo)
        {
            InitializeComponent();
            InitializeGame(i_MenuForm, i_GameInfo);
        }

        private void InitializeGame(Form i_MenuForm, GameInfo i_GameInfo)
        {
            ResetPieces();
            m_MenuForm = i_MenuForm;
            m_GameInfo = i_GameInfo;
            m_Pieces = new PictureBox[m_GameInfo.BoardSize, m_GameInfo.BoardSize];
            m_GameLogic = new GameLogic(m_GameInfo.BoardSize);
            pictureBoxTurnPiece.SizeMode = PictureBoxSizeMode.StretchImage;

            m_GameInfo.ResetGame(m_GameLogic);
            m_boardSize = m_GameLogic.GetBoardSize();
            m_pieceSize = dataGridViewDamka.Height / m_boardSize - 1;
            m_fromX = dataGridViewDamka.Width - m_pieceSize * m_boardSize - 5;
            if (m_GameInfo.GameType == GameLogic.eGameType.PlayerVsComputer)
                m_ComputerLogic = new ComputerLogic(m_GameLogic);

            DrawPieces();
            UpdateScores();
        }

        private void UpdateScores()
        {
            string pleayerTurn;
            labelPlayer1Score.Text = $"{m_GameInfo.Player1Name}: {m_GameInfo.Player1Score}";
            labelPlayer2Score.Text = $"{m_GameInfo.Player2Name}: {m_GameInfo.Player2Score}";
            if (m_GameInfo.GameTurn == GameLogic.eGameTurn.Player1)
                pleayerTurn = m_GameInfo.Player1Name;
            else
                pleayerTurn = m_GameInfo.Player2Name;
            labelTurn.Text = $"Turn: {pleayerTurn}";
            if (pleayerTurn == m_GameInfo.Player1Name)
            {
                pictureBoxTurnPiece.Image = Properties.Resources.redPiece;
            }
            else
            {
                pictureBoxTurnPiece.Image = Properties.Resources.blackPiece;
            }
        }

        private void DrawBoard()
        {
            int fromY = 6;

            for (int i = 0; i < m_boardSize; i++)
            {
                for (int j = 0; j < m_boardSize; j++)
                {
                    Brush brush = (i + j) % 2 == 0 ? Brushes.SlateGray : Brushes.AntiqueWhite;
                    DrawRectangle(m_fromX + j * m_pieceSize, fromY + i * m_pieceSize, m_pieceSize, brush);
                }
            }
        }

        private void ResetPieces()
        {
            for (int i = 0; i < m_boardSize; i++)
            {
                for (int j = 0; j < m_boardSize; j++)
                {
                    if (m_Pieces != null && m_Pieces[i, j] != null)
                        dataGridViewDamka.Controls.Remove(m_Pieces[i, j]);
                }
            }
        }

        private void DrawPieces()
        {
            Player[,] boardPieces = m_GameLogic.GetBoardArray();
            PiecePictureBox piecePictureBox;

            for (int i = 0; i < m_boardSize; i++)
            {
                for (int j = 0; j < m_boardSize; j++)
                {
                    if (boardPieces[i, j] != null)
                    {
                        piecePictureBox = new PiecePictureBox(boardPieces[i, j], m_pieceSize, m_fromX, i, j);
                        piecePictureBox.PieceIsSelected += PiecePictureBox_PieceIsSelected;
                    }
                    else
                    {
                        Color color = (i + j) % 2 == 0 ? Color.SlateGray : Color.AntiqueWhite;
                        piecePictureBox = new PiecePictureBox(color, m_pieceSize, m_fromX, i, j);
                        piecePictureBox.TargetPieceIsSelected += PiecePictureBox_TargetPieceIsSelected;
                    }
                    m_Pieces[i, j] = piecePictureBox;
                    dataGridViewDamka.Controls.Add(piecePictureBox);
                }
            }
        }

        private void PiecePictureBox_PieceIsSelected(object sender, EventArgs e)
        {
            BoardPiece pieceFrom = sender as BoardPiece;
            SetSelectedPlayer(pieceFrom);
        }

        private void SetSelectedPlayer(BoardPiece pieceFrom)
        {
            if (pieceFrom.IsSelected)
            {
                if (selectedPiece != null && !selectedPiece.Equals(pieceFrom))
                {
                    selectedPiece.IsSelected = false;
                }
                selectedPiece = pieceFrom;
            }
            else
                selectedPiece = null;
        }

        private void PiecePictureBox_TargetPieceIsSelected(object sender, EventArgs e)
        {
            targetPiece = sender as BoardPiece;
            if (selectedPiece != null && selectedPiece.IsSelected)
            {
                PerformPlayerMove();
            }
        }

        private void PerformPlayerMove()
        {
            PlayerMove playerMove;
            string errorMessage;
            bool isValidMove;

            if (selectedPiece != null)
            {
                playerMove = new PlayerMove(selectedPiece.Y, selectedPiece.X, targetPiece.Y, targetPiece.X);
                isValidMove = m_GameLogic.IsValidMove((int)m_GameInfo.GameTurn, playerMove, out errorMessage);
                if (timerComputerMove.Enabled)
                {
                    isValidMove = false;
                    errorMessage = "Its the Computer's Turn!";
                }

                if (isValidMove)
                {
                    PerformLogicMove(playerMove);
                    timerPieceMove.Enabled = true;
                    targetPiece.IsSelected = false;
                }
                else
                {
                    MessageBox.Show(errorMessage, "Ivalid Move!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            selectedPiece.IsSelected = false;
            CheckGameStatuse();
            CheckIfComputerTurn();
        }

        private void PerformComputerPlay()
        {
            PlayerMove computerMove = m_ComputerLogic.GetNextComputerMove();
            selectedPiece = m_Pieces[computerMove.FromY, computerMove.FromX] as BoardPiece;
            targetPiece = m_Pieces[computerMove.ToY, computerMove.ToX] as BoardPiece;
            PerformLogicMove(computerMove);
            timerPieceMove.Enabled = true;
            CheckIfComputerTurn();
        }

        private void CheckIfComputerTurn()
        {
            CheckGameStatuse();

            if (m_GameInfo.GameTurn == GameLogic.eGameTurn.Player2
                && m_GameInfo.GameType == GameLogic.eGameType.PlayerVsComputer)
            {
                timerComputerMove.Enabled = true;
            }
        }

        private void CheckGameStatuse()
        {
            DialogResult dialogResult = DialogResult.No;
            bool gameOver = true;

            switch (m_GameLogic.GetGameStatus())
            {
                case GameLogic.eGameStatus.Playing:
                    gameOver = false;
                    break;
                case GameLogic.eGameStatus.Player1Won:
                    dialogResult = MessageBox.Show($"{m_GameInfo.Player1Name} Won!\nAnother Round?",
                        "Game Finished", MessageBoxButtons.YesNo);
                    break;
                case GameLogic.eGameStatus.Player2Won:
                    dialogResult = MessageBox.Show($"{m_GameInfo.Player2Name} Won!\nAnother Round?",
                        "Game Finished", MessageBoxButtons.YesNo);
                    break;
                case GameLogic.eGameStatus.Tie:
                    dialogResult = MessageBox.Show($"It's a Tie!\nAnother Round?",
                        "Game Finished", MessageBoxButtons.YesNo);
                    break;
            }

            if (gameOver)
            {
                if (dialogResult == DialogResult.No)
                {
                    Dispose();
                }
                else
                {
                    timerComputerMove.Stop();
                    m_GameInfo.CheckGameStatus(m_GameLogic);
                    m_GameInfo.ResetGame(m_GameLogic);
                    InitializeGame(m_MenuForm, m_GameInfo);
                }
            }
        }

        private void PerformLogicMove(PlayerMove playerMove)
        {
            bool haveNextEatMove;
            m_GameLogic.PerformMove(playerMove, out haveNextEatMove);
            m_GameInfo.SwitchPlayerTurn(m_GameLogic, haveNextEatMove);
            UpdateScores();
        }

        private void PerformUIMove()
        {
            for (int i = 0; i < m_boardSize; i++)
            {
                for (int j = 0; j < m_boardSize; j++)
                {
                    dataGridViewDamka.Controls.Remove(m_Pieces[j, i]);
                    dataGridViewDamka.Controls.Remove(m_Pieces[j, i]);
                }
            }
            DrawPieces();
        }

        private void GameForm_Load(object sender, EventArgs e) { }

        private void DrawRectangle(int i_x, int i_y, int i_size, Brush i_brush)
        {
            Graphics g = dataGridViewDamka.CreateGraphics();
            Pen pen = new Pen(Color.Black, 2);
            g.DrawRectangle(pen, i_x, i_y, i_size, i_size);
            g.FillRectangle(i_brush, i_x, i_y, i_size, i_size);
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_MenuForm?.Dispose();
        }

        private void DataGridViewDamka_Paint(object sender, PaintEventArgs e)
        {
            DrawBoard();
        }

        private void TimerComputerMove_Tick(object sender, EventArgs e)
        {
            timerComputerMove.Enabled = false;
            PerformComputerPlay();
        }

        private int ticks = 0;
        private int stepX;
        private int stepY;
        private void TimerPieceMove_Tick(object sender, EventArgs e)
        {
            int time = 30;
            Control selctedPieceBox = selectedPiece as PictureBox;
            Control targetPieceBox = targetPiece as PictureBox;

            if (ticks == 0)
            {
                DisablePieces();
                selctedPieceBox.BringToFront();
                stepX = (targetPieceBox.Location.X - selctedPieceBox.Location.X) / (time / timerPieceMove.Interval);
                stepY = (targetPieceBox.Location.Y - selctedPieceBox.Location.Y) / (time / timerPieceMove.Interval);
            }

            selctedPieceBox.Location =
                new Point(selctedPieceBox.Location.X + stepX,
                          selctedPieceBox.Location.Y + stepY);

            if (ticks++ > time / timerPieceMove.Interval)
            {
                timerPieceMove.Enabled = false;
                PerformUIMove();
                ticks = 0;
            }
        }

        private void DisablePieces()
        {
            for (int i = 0; i < m_boardSize; i++)
            {
                for (int j = 0; j < m_boardSize; j++)
                {
                    (m_Pieces[i, j] as PiecePictureBox).RemoveListeners();
                }
            }
        }


    }
}