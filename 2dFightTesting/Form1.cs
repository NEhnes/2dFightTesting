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
        Character player1 = new Character(100, 100);
        bool aPressed = false;
        bool dPressed = false;
        bool wPressed = false;
        Stopwatch wBufferStopwatch = new Stopwatch();
        int frameCount = 0;

        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            gameTimer.Enabled = true;
        }

        private void gameTimer_Tick_1(object sender, EventArgs e)
        {
            if (wBufferStopwatch.ElapsedMilliseconds > 100)
            {
                wPressed = false;
                wBufferStopwatch.Stop();
            }

            if (player1.currentAttack == null)
            {
                player1.Move(aPressed, dPressed, wPressed);
            }

            frameCount++;
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            player1.DrawNextFrame(g, frameCount);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
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
                case Keys.Q: // Trigger light punch
                    List<Rectangle> hitboxes = new List<Rectangle> { new Rectangle(20, 20, 40, 40) };
                    List<Rectangle> hurtboxes = new List<Rectangle> { new Rectangle(10, 10, 30, 50) };
                    List<Image> frames = new List<Image>
                    {
                        Properties.Resources.attack1_1,
                        Properties.Resources.attack1_2,
                        Properties.Resources.attack1_3,
                        Properties.Resources.attack1_4,
                        Properties.Resources.attack1_5,
                        Properties.Resources.attack1_6,
                    };

                    Attack lightPunch = new Attack("lightPunch", 2, 2, 3, 10, 5, hitboxes, hurtboxes, frames);
                    player1.StartAttack(lightPunch);
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
            }
        }

        private async void ScreenShake(int intensity = 5, int duration = 100)
        {
            var originalLocation = this.Location;
            Random randgen = new Random();
            int elapsed = 0;
            int interval = 16;

            while (elapsed < duration)
            {
                int offsetX = randgen.Next(-intensity, intensity + 1);
                int offsetY = randgen.Next(-intensity, intensity + 1);
                this.Location = new Point(originalLocation.X + offsetX, originalLocation.Y + offsetY);
                await Task.Delay(interval);
                elapsed += interval;
            }

            this.Location = originalLocation;
        }
    }
}
