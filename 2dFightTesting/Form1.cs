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
        Character player1 = new Samurai(100, 100);

        bool aPressed = false;
        bool dPressed = false;
        bool wPressed = false;
        Stopwatch wBufferStopwatch = new Stopwatch();
        bool tabPressed = false;

        Character player2 = new Samurai(600, 100);
        bool leftPressed = false;
        bool rightPressed = false;
        bool upPressed = false;
        Stopwatch upBufferStopwatch = new Stopwatch();

        int frameCount = 0;

        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            gameTimer.Enabled = true;
        }

        private void gameTimer_Tick_1(object sender, EventArgs e)
        {
            //To handle player 1 w key buffer
            if (wBufferStopwatch.ElapsedMilliseconds > 100) // remove later after testing
            {
                wPressed = false;
                wBufferStopwatch.Stop();
            }

            //To handle player 2 up key buffer
            if (upBufferStopwatch.ElapsedMilliseconds > 100)
            {
                upPressed = false;
                upBufferStopwatch.Stop();
            }

            //Move player 1 if not attacking
            if (player1.currentState != "attack1" && player1.currentState != "attack2")
            {

                player1.Move(aPressed, dPressed, wPressed);
            }

            //Move player 2 if not attacking
            if (player2.currentState != "attack1" && player2.currentState != "attack2")
            {
                player2.Move(leftPressed, rightPressed, upPressed);
            }

            frameCount++;
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            player1.DrawFrame(g, frameCount);
            player2.DrawFrame(g, frameCount);

            //TODO draw player indicators or someway of seperating the player maybe change the player color or a label above them or something
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //TODO: Change the Q and E keybind to something that can be controlled by the thumb
                //Player 1 keypresses
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.W:
                    wPressed = true;
                    wBufferStopwatch.Restart();
                    break;
                case Keys.Q: // attack 1 (p1)
                    if (player1.currentState != "attack1") player1.SetMove("attack1");
                    break;
                case Keys.E: // attack 2 (p2)
                    if (player1.currentState != "attack2") player1.SetMove("attack2");
                    break;

                //Player 2 keypresses
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;

                //TODO: Find better keybinds for easier controls maybe something that can be pressed by the pinky finger
                case Keys.NumPad1: // Player 2 attack 1
                    if (player2.currentState != "attack1") player2.SetMove("attack1");
                    break;
                case Keys.NumPad3: // Player 2 attack 2
                    if (player2.currentState != "attack2") player2.SetMove("attack2");
                    break;

                //Game Control keypresses
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
            //player 1 button releases
            switch (e.KeyCode)
            {
                //Player 1 key releases
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.W:
                    wPressed = false;
                    break;

                //Player 2 key releases
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;

                //Game Control key releases
                case Keys.Escape:
                    tabPressed = false;
                    break;
                default:
                    break;
            }
        }

        private async void ScreenShake(int intensity = 5, int duration = 100)
        {
            var originalLocation = this.Location;
            Random randgen = new Random();

            int elapsed = 0;
            int interval = 16; // ~60 FPS

            while (elapsed < duration)
            {
                int offsetX = randgen.Next(-intensity, intensity + 1);
                int offsetY = randgen.Next(-intensity, intensity + 1);
                this.Location = new Point(originalLocation.X + offsetX, originalLocation.Y + offsetY);

                await Task.Delay(interval);
                elapsed += interval;
            }

            this.Location = originalLocation; // reset position
        }
    }
}