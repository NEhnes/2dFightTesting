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
            Form1.ChangeScreen(this, new GameScreen(player1NameTextBox.Text, player2NameTextBox.Text));
        }
    }
}
