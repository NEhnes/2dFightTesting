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

        String winnerName;

        class Player
        {
            public string name;
            public string wins;
        }
        public WinScreen(string _winnerName, string _lastScreen)
        {
            InitializeComponent();

            winnerName = _winnerName;

            winnerLabel.Text = $"{winnerName} wins!!!";

            IncrementWinner(winnerName, _lastScreen);
        }

        private void IncrementWinner(String _winnerName, String _lastScreen)
        {
            LoadStats();

            Player winnerWinner = CheckListForWinner(_winnerName);

            if (_lastScreen == "gameScreen")
            {
                AddWinPoint(winnerWinner, _winnerName);
            }

            SortPlayersList();

            OverwriteXml();
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

        private Player CheckListForWinner(String _winnerName)
        {
            foreach (Player p in players)
            {
                if (p.name == _winnerName)
                {
                    return p;
                }
            }
            return null;
        }

        private void AddWinPoint(Player winnerWinner, String _winnerName)
        {
            if (winnerWinner == null) // doesn't already exist
            {
                Player newPlayer = new Player();
                newPlayer.name = _winnerName;
                newPlayer.wins = "1";
                players.Add(newPlayer);
            }
            else // already exists
            {
                foreach (Player p in players)
                {
                    if (p.name == winnerWinner.name)
                    {
                        int wins = int.Parse(p.wins);
                        wins++;
                        p.wins = wins.ToString();
                        return;
                    }
                }
            }
        }

        private void SortPlayersList()
        {
            players = players.OrderByDescending(p => int.Parse(p.wins)).ToList();
        }


        private void OverwriteXml()
        {
            XmlWriter writer = XmlWriter.Create("Resources/WinRecords.xml", null);

            writer.WriteStartDocument();
            writer.WriteStartElement("Players");
            foreach (Player p in players)
            {
                writer.WriteStartElement("Player");
                writer.WriteAttributeString("name", p.name);
                writer.WriteAttributeString("wins", p.wins);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Close();
        }

        private void leaderboard_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new LeaderBoardScreen(winnerName));
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