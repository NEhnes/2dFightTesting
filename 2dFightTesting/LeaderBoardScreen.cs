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
    public partial class LeaderBoardScreen : UserControl
    {
        List<Player> players = new List<Player>();

        class Player
        {
            public string name;
            public string wins;
        }
        public LeaderBoardScreen()
        {
            InitializeComponent();
            LoadStats();

            outputLabel.Text = "Leader Board\n\n";

            foreach (Player p in players)
            {
                outputLabel.Text += $"{p.name} : {p.wins} wins\n";
            }
        }
        private void LoadStats()
        {
            XmlReader reader = XmlReader.Create("Resources/WinRecords.xml", null);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Player")
                {
                    Player p = new Player();

                    p.name = reader.GetAttribute("name");
                    p.wins = reader.GetAttribute("wins");

                    players.Add(p);
                }
            }
            reader.Close();
        }
    }
}
