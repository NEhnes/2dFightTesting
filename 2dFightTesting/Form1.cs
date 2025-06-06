using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace _2dFightTesting
{
    public partial class Form1 : Form
    {
        int p1_xSpeed = 0;
        int p1_ySpeed = 0;
        Character player1 = new Character(100, 100);

        bool aPressed = false;
        bool dPressed = false;
        bool wPressed = false;
        Stopwatch wBufferStopwatch = new Stopwatch();
        bool tabPressed = false;

        int frameCount = 0;

        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            gameTimer.Enabled = true;
        }

        private void gameTimer_Tick_1(object sender, EventArgs e)
        {

            if (wBufferStopwatch.ElapsedMilliseconds > 100) // remove later after testing
            {
                wPressed = false;
                wBufferStopwatch.Stop();
            }

            if(player1.currentMove != "attack1")
            {

                player1.Move(aPressed, dPressed, wPressed);
            }

            frameCount++;
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            player1.GetNextFrame(g, frameCount);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)  // now registering
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    //System.Console.WriteLine("A pressed");
                    aPressed = true;
                    break;
                case Keys.D:
                    //System.Console.WriteLine("D pressed");
                    dPressed = true;
                    break;
                case Keys.W:
                    wPressed = true;
                    wBufferStopwatch.Restart();
                    break;
                case Keys.Q: // attack 1
                    player1.SetMove("attack1");
                    break;
                case Keys.Escape:
                    break;
                case Keys.Tab:
                    tabPressed = true;
                    break;
                default:
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode.ToString() + "______");
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.A:
                    aPressed = false;
                    System.Console.WriteLine("A released");
                    break;
                case Keys.D:
                    dPressed = false;
                    System.Console.WriteLine("D released");
                    break;
                case Keys.W:
                    break;
                case Keys.Escape:
                    tabPressed = false;
                    break;
                default:
                    break;
            }
        }
    }
}
