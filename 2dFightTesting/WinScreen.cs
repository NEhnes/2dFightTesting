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
    public partial class WinScreen : UserControl
    {
        public WinScreen(string _winnerName)
        {
            InitializeComponent();

            winnerLabel.Text = $"{_winnerName} wins!!!";

            // check xml for name

            // if exists, update score

            // if doe snot exist, create new element with name and score
        }

        private void leaderboard_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new LeaderBoardScreen());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new MenuScreen());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
