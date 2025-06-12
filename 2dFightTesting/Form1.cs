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
using System.Media;

namespace _2dFightTesting
{
    public partial class Form1 : Form
    {
        Samurai player1 = new Samurai(100, 100);

        bool aPressed = false;
        bool dPressed = false;
        bool wPressed = false;
        Stopwatch wBufferStopwatch = new Stopwatch();
        bool tabPressed = false;

        Samurai player2 = new Samurai(600, 100);
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
            if (player1.currentAttack == null)
            {
                player1.Move(aPressed, dPressed, wPressed);
            }

            //Move player 2 if not attacking
            if (player2.currentAttack == null)
            {
                player2.Move(leftPressed, rightPressed, upPressed);
            }

            CheckWallCollisons();

            //Checks for the collision between hit/hurt boxes
            CheckCollision();

            frameCount++;
            Refresh();
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
                    player1.SetAttack("light2");
                    break;
                case Keys.E: // attack 2 (p2)
                    player1.SetAttack("heavy2");
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
                    player2.SetAttack("light2");
                    break;
                case Keys.NumPad3: // Player 2 attack 2
                    player2.SetAttack("heavy2");
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
        private void CheckCollision()
        {
            //check if player 1 hit player 2
            if(player1.currentAttack != null && !player1.hitLanded)
            {
                //Gets the hit box of where the player is attacking
                Rectangle hitbox = player1.GetHitBox();

                //Get the area of where player two can be hit
                Rectangle hurtbox = player2.GetHurtBox();

                //Check if the hitbox overlaps the hurtbox to check for collison
                if (hitbox.IntersectsWith(hurtbox))
                {
                    //Take away health
                    player2.Damage += 10;
                    player1.hitLanded = true; //Sets the attack as landed

                    player2.stunTimer = 20;

                    //Add Knockback away from the attacker
                    if(player1.facingRight == true)
                    {
                        player2.knockbackSpeed = 15; //Push the player 2 to the right
                    }
                    else
                    {
                        player2.knockbackSpeed = -15; //Push the player 2 to the left
                    }
                    //Screen shake on collision
                    ScreenShake(10, 50);
                }
            }

            //check if the player 2 is attackingd
            if(player2.currentAttack != null && !player2.hitLanded)
            {
                //Gets the hit box of where the player is attacking
                Rectangle hitbox = player2.GetHitBox();

                //Get the area of where player one can be hit
                Rectangle hurtbox = player1.GetHurtBox();

                //Check if the hitbox overlaps the hurtbox to check for collison
                if (hitbox.IntersectsWith(hurtbox))
                {
                    //Take away health
                    player1.Damage += 10;
                    player2.hitLanded = true; //Sets the attack as landed

                    player1.stunTimer = 20;
                    //Add Knockback away from the attacker
                    if (player2.facingRight == true)
                    {
                        player1.knockbackSpeed = 15; //Push the player 2 to the right
                    }
                    else
                    {
                        player1.knockbackSpeed = -15; //Push the player 2 to the left
                    }
                    //Screen shake on collision
                    ScreenShake(10, 50);
                }
            }
        }

        private void CheckWallCollisons()
        {
            const int playerWidth = 64;          // Width of player sprite

            //Check player 1 left wall collision
            if (player1.X <= 0)
            {
                player1.X = 0;  // Stop player at left wall
            }

            // Check player 1 right wall collision  
            if (player1.X + playerWidth >= this.Width)
            {
                player1.X = this.Width - playerWidth;  // Stop player at right wall
            }

            // Check player 2 left boundary collision
            if (player2.X <= 0)
            {
                player2.X = 0;  // Stop player at left wall
            }

            // Check player 2 right boundary collision
            if (player2.X + playerWidth >= this.Width)
            {
                player2.X = this.Width - playerWidth;  // Stop player at right wall
            }
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        { 
            Graphics g = e.Graphics;
            player1.DrawFrame(g, frameCount);
            player2.DrawFrame(g, frameCount);

            //CODE ONLY THERE FOR TESTING PURPOSES

            // Draw the hurtboxes (where the players can be hit) in blue
            g.DrawRectangle(Pens.Blue, player1.GetHurtBox());
            g.DrawRectangle(Pens.Blue, player2.GetHurtBox());

            // Draw the hitboxes (active attack zones) in red
            if (player1.currentState == "attack2" || player1.currentState == "attack1")
                g.DrawRectangle(Pens.Red, player1.GetHitBox());

            if (player2.currentState == "attack2" || player2.currentState == "attack1")
                g.DrawRectangle(Pens.Red, player2.GetHitBox());

            // 1. Setup font and brush to draw text
            Font healthFont = new Font("Arial", 20, FontStyle.Bold); // font for health
            Brush healthBrush = Brushes.White; // color of health text

            // 2. Draw Player 1 health (top-left)
            e.Graphics.DrawString("P1 DMG: " + player1.Damage, healthFont, healthBrush, 10, 10);

            // 3. Draw Player 2 health (top-right)
            e.Graphics.DrawString("P2 DMG: " + player2.Damage, healthFont, healthBrush, this.Width - 180, 10);

            //TODO draw player indicators or someway of seperating the player maybe change the player color or a label above them or something
        }
    }
}