using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace _2dFightTesting
{
    public partial class WinScreen : UserControl
    {
        List<Player> players = new List<Player>();

        class Player
        {
            public string name;
            public string wins;
        }
        public WinScreen(string _winnerName)
        {
            InitializeComponent();

            winnerLabel.Text = $"{_winnerName} wins!!!";

            LoadStats();

            winnerLabel.Text = players[2].wins;

            CheckListForWinner();

            // if exists, update score

            // if doe snot exist, create new element with name and score
        }

        private void LoadStats()
        {
            XmlReader reader = XmlReader.Create("Resources/WinRecords.xml", null);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    Player p = new Player();

                    p.name = reader.GetAttribute("name");
                    p.wins = reader.GetAttribute("wins");

                    players.Add(p);
                }
            }
        }

        private void CheckListForWinner()
        {
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
