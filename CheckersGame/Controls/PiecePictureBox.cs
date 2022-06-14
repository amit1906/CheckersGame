using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CheckersGameLogic;
using System.Drawing;
using System;

namespace CheckersGameForms.Controls
{
    class PiecePictureBox : PictureBox, BoardPiece
    {
        public event EventHandler PieceIsSelected;
        public event EventHandler TargetPieceIsSelected;

        private bool m_IsSelected;
        private Color m_backColor;

        int BoardPiece.X { get; set; }
        int BoardPiece.Y { get; set; }
        bool BoardPiece.IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                m_IsSelected = value;
                PieceRevertSelectionStatus(false);
            }
        }

        private PiecePictureBox(int pieceSize, int fromX, int i, int j)
        {
            (this as BoardPiece).X = j;
            (this as BoardPiece).Y = i;
            int fromY = 6;
            Size = new Size(pieceSize - 1, pieceSize - 1);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Location = new Point(fromX + j * pieceSize, fromY + i * pieceSize);
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        public PiecePictureBox(Color color, int pieceSize, int fromX, int i, int j) : this(pieceSize, fromX, i, j)
        {
            m_backColor = color;
            Tag = "None";
            SetNonePieceColor();
            MouseClick += NonePlayerImage_MouseClick;
            MouseMove += PiecePictureBox_MouseHover;
            MouseLeave += PiecePictureBox_MouseLeave;
        }

        public void RemoveListeners()
        {
            MouseClick -= NonePlayerImage_MouseClick;
            MouseMove -= PiecePictureBox_MouseHover;
            MouseLeave -= PiecePictureBox_MouseLeave;
        }

        private void NonePlayerImage_MouseClick(object sender, MouseEventArgs e)
        {
            TargetPieceIsSelected(this, EventArgs.Empty);
        }

        public PiecePictureBox(Player player, int pieceSize, int fromX, int i, int j) : this(pieceSize, fromX, i, j)
        {
            if (player.PlayerNumber == Player.ePlayerType.Player1)
            {
                if (player is King)
                {
                    Image = Properties.Resources.redKing;
                    Tag = "player1King";
                }
                else
                {
                    Image = Properties.Resources.redPiece;
                    Tag = "player1";
                }
            }
            else if (player.PlayerNumber == Player.ePlayerType.Player2)
            {
                if (player is King)
                {
                    Image = Properties.Resources.blacKing;
                    Tag = "player2King";
                }
                else
                {
                    Image = Properties.Resources.blackPiece;
                    Tag = "player2";
                }
            }
            SetRegion();
            MouseClick += PlayerImage_MouseClick;
            MouseMove += PiecePictureBox_MouseHover;
            MouseLeave += PiecePictureBox_MouseLeave;
        }

        private void PiecePictureBox_MouseHover(object sender, EventArgs e)
        {
            if (!(this as BoardPiece).IsSelected)
            {
                if (Tag.ToString() != "None")
                    SetRegion(true);
                else
                    SetNonePieceColor(true);
            }
        }

        private void PiecePictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (Tag.ToString() != "None")
                SetRegion();
            else
                SetNonePieceColor();
        }

        private void PlayerImage_MouseClick(object sender, MouseEventArgs e)
        {
            PieceRevertSelectionStatus();
        }

        private void PieceRevertSelectionStatus(bool toRevertStatus = true)
        {
            if (toRevertStatus)
                (this as BoardPiece).IsSelected = !(this as BoardPiece).IsSelected;

            if ((this as BoardPiece).IsSelected)
            {
                if (Tag.ToString() == "player1")
                {
                    Image = Properties.Resources.redPieceUp;
                }
                else if (Tag.ToString() == "player2")
                {
                    Image = Properties.Resources.blackPieceUp;
                }
                else if (Tag.ToString() == "player1King")
                {
                    Image = Properties.Resources.redKingUp;
                }
                else if (Tag.ToString() == "player2King")
                {
                    Image = Properties.Resources.blacKingUp;
                }
                PieceIsSelected(this, EventArgs.Empty);
            }
            else
            {
                if (Tag.ToString() == "player1")
                {
                    Image = Properties.Resources.redPiece;
                }
                else if (Tag.ToString() == "player2")
                {
                    Image = Properties.Resources.blackPiece;
                }
                else if (Tag.ToString() == "player1King")
                {
                    Image = Properties.Resources.redKing;
                }
                else if (Tag.ToString() == "player2King")
                {
                    Image = Properties.Resources.blacKing;
                }
            }
            SetRegion();
        }

        private void SetRegion(bool isHovered = false)
        {
            using (GraphicsPath graphics = new GraphicsPath())
            {
                if (isHovered)
                {
                    graphics.AddRectangle(new Rectangle(0, 0, Width, Height));
                    BackColor = Color.Aqua;
                }
                else if ((this as BoardPiece).IsSelected)
                {
                    graphics.AddRectangle(new Rectangle(0, 0, Width, Height));
                    BackColor = Color.Blue;
                }
                else
                {
                    graphics.AddEllipse(new Rectangle(0, 0, Width, Height));
                    BackColor = Color.WhiteSmoke;
                }
                Region = new Region(graphics);
            }
        }

        private void SetNonePieceColor(bool isHovered = false)
        {
            if (isHovered)
                BackColor = Color.Aqua;
            else
                BackColor = m_backColor;
        }

    }
}