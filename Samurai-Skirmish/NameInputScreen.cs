using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2dFightTesting
{
    public partial class NameInputScreen : UserControl
    {
        public NameInputScreen()
        {
            InitializeComponent();
        }

        private void confirmName_Click(object sender, EventArgs e)
        {
            // Validate player names

            Form1.ChangeScreen(this, new GameScreen(player1NameTextBox.Text, player2NameTextBox.Text));
        }

        private void player1NameTextBox_Enter(object sender, EventArgs e)
        {
            player1NameTextBox.Text = "";
        }

        private void player2NameTextBox_Enter(object sender, EventArgs e)
        {
            player2NameTextBox.Text = "";
        }
    }
}
