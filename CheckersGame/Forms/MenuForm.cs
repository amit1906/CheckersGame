using System.Windows.Forms;
using CheckersGameLogic;
using System;

namespace CheckersGameForms
{
    partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void MenuForm_Load(object sender, EventArgs e) { }

        private void StartButton_Click(object sender, EventArgs e)
        {
            bool isValidNames = true;
            int gameSize = GetRadioButtonsValue();
            GameLogic.eGameType gameType = checkBoxPlayer2.Checked ? 
                GameLogic.eGameType.PlayerVsPlayer : GameLogic.eGameType.PlayerVsComputer;
            GameInfo gameInfo = new GameInfo(textBoxPlayer1.Text, textBoxPlayer2.Text, gameSize, gameType);

            if (!InputsValidations.IsInputNameValid(textBoxPlayer1.Text))
            {
                isValidNames = false;
            }
            else if (checkBoxPlayer2.Checked&& !InputsValidations.IsInputNameValid(textBoxPlayer2.Text))
            {
                isValidNames = false;
            }
            if (isValidNames)
            {
                Hide();
                new GameForm(this, gameInfo).Show();
            }
            else
            {
                MessageBox.Show("invalid name - should be 2-20 characters.", "error!");
            }
        }

        private int GetRadioButtonsValue()
        {
            RadioButton[] radioButtons = { radioButton6, radioButton8, radioButton10 };
            int value = 0;

            foreach (RadioButton radioButton in radioButtons)
            {
                if (radioButton.Checked)
                {
                    value = int.Parse(radioButton.Text.Substring(0, 2));
                    break;
                }
            }
            return value;
        }

        private void CheckBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxPlayer2.Checked)
            {
                textBoxPlayer2.Text = "";
                textBoxPlayer2.Enabled = true;
            }
            else
            {
                textBoxPlayer2.Text = "[Computer]";
                textBoxPlayer2.Enabled = false;
            }
        }
    }
}