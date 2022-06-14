
namespace CheckersGameForms
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.labelPlayer1Score = new System.Windows.Forms.Label();
            this.labelPlayer2Score = new System.Windows.Forms.Label();
            this.dataGridViewDamka = new System.Windows.Forms.DataGridView();
            this.labelTurn = new System.Windows.Forms.Label();
            this.pictureBoxTurnPiece = new System.Windows.Forms.PictureBox();
            this.timerComputerMove = new System.Windows.Forms.Timer(this.components);
            this.timerPieceMove = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDamka)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTurnPiece)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPlayer1Score
            // 
            this.labelPlayer1Score.AutoSize = true;
            this.labelPlayer1Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayer1Score.Location = new System.Drawing.Point(94, 47);
            this.labelPlayer1Score.Name = "labelPlayer1Score";
            this.labelPlayer1Score.Size = new System.Drawing.Size(108, 24);
            this.labelPlayer1Score.TabIndex = 14;
            this.labelPlayer1Score.Text = "Player 1: 0";
            // 
            // labelPlayer2Score
            // 
            this.labelPlayer2Score.AutoSize = true;
            this.labelPlayer2Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayer2Score.Location = new System.Drawing.Point(378, 47);
            this.labelPlayer2Score.Name = "labelPlayer2Score";
            this.labelPlayer2Score.Size = new System.Drawing.Size(108, 24);
            this.labelPlayer2Score.TabIndex = 15;
            this.labelPlayer2Score.Text = "Player 2: 0";
            // 
            // dataGridViewDamka
            // 
            this.dataGridViewDamka.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDamka.Location = new System.Drawing.Point(40, 131);
            this.dataGridViewDamka.Name = "dataGridViewDamka";
            this.dataGridViewDamka.Size = new System.Drawing.Size(500, 500);
            this.dataGridViewDamka.TabIndex = 0;
            this.dataGridViewDamka.Paint += new System.Windows.Forms.PaintEventHandler(this.DataGridViewDamka_Paint);
            // 
            // labelTurn
            // 
            this.labelTurn.AutoSize = true;
            this.labelTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTurn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelTurn.Location = new System.Drawing.Point(218, 87);
            this.labelTurn.Name = "labelTurn";
            this.labelTurn.Size = new System.Drawing.Size(135, 24);
            this.labelTurn.TabIndex = 16;
            this.labelTurn.Text = "Turn: Player1";
            // 
            // pictureBoxTurnPiece
            // 
            this.pictureBoxTurnPiece.BackgroundImage = global::CheckersGameForms.Properties.Resources.redPiece;
            this.pictureBoxTurnPiece.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxTurnPiece.Location = new System.Drawing.Point(257, 34);
            this.pictureBoxTurnPiece.Name = "pictureBoxTurnPiece";
            this.pictureBoxTurnPiece.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxTurnPiece.TabIndex = 17;
            this.pictureBoxTurnPiece.TabStop = false;
            // 
            // timerComputerMove
            // 
            this.timerComputerMove.Interval = 2000;
            this.timerComputerMove.Tick += new System.EventHandler(this.TimerComputerMove_Tick);
            // 
            // timerPieceMove
            // 
            this.timerPieceMove.Interval = 1;
            this.timerPieceMove.Tick += new System.EventHandler(this.TimerPieceMove_Tick);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(584, 661);
            this.Controls.Add(this.pictureBoxTurnPiece);
            this.Controls.Add(this.labelTurn);
            this.Controls.Add(this.labelPlayer2Score);
            this.Controls.Add(this.labelPlayer1Score);
            this.Controls.Add(this.dataGridViewDamka);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(600, 700);
            this.MinimumSize = new System.Drawing.Size(600, 700);
            this.Name = "GameForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Damka";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
            this.Load += new System.EventHandler(this.GameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDamka)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTurnPiece)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelPlayer1Score;
        private System.Windows.Forms.Label labelPlayer2Score;
        private System.Windows.Forms.DataGridView dataGridViewDamka;
        private System.Windows.Forms.Label labelTurn;
        private System.Windows.Forms.PictureBox pictureBoxTurnPiece;
        private System.Windows.Forms.Timer timerComputerMove;
        private System.Windows.Forms.Timer timerPieceMove;
    }
}