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
        public WinScreen()
        {
            InitializeComponent();

            if(GameScreen.player1wins == true)
            {
                winnerLabel.Text = "Player 1 wins!!!";
            }

            if(GameScreen.player2wins == true)
            {
                winnerLabel.Text = "Player 2 wins!!!";
            }
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
