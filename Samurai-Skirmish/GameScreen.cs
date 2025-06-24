using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2dFightTesting
{
    public partial class GameScreen : UserControl
    {
        #region global_variables_n_objects
        // player 1 object and key states
        RedSamurai player1 = new RedSamurai(100, 100);
        bool aPressed = false;
        bool dPressed = false;
        bool wPressed = false;
        bool tabPressed = false;

        // player 2 object and key states
        BlueSamurai player2 = new BlueSamurai(600, 100);
        bool leftPressed = false;
        bool rightPressed = false;
        bool upPressed = false;

        // Game state variables
        int frameCount = 0;
        bool debugMode = false;  // press escape to toggle hitbox/hurtbox visibility

        public static bool player1wins;
        public static bool player2wins;

        // Variables for round system
        int player1RoundWins = 0;
        int player2RoundWins = 0;
        const int maxDamage = 100;
        const int roundsToWin = 2;
        bool roundOver = false;
        #endregion

        public GameScreen(String _p1Name, String _p2Name)
        {
            InitializeComponent();
            gameTimer.Enabled = true;
            roundEndTimer.Interval = 2000;  // timer for each round

            player1.Name = _p1Name;
            player2.Name = _p2Name;

        }

        private void gameTimer_Tick_1(object sender, EventArgs e)
        {
            //dont update if the round is over
            if (roundOver) return;

            //Move players
            player1.Move(aPressed, dPressed, wPressed);
            player2.Move(leftPressed, rightPressed, upPressed);

            // keep players within bounds
            CheckWallCollisons();

            //Checks for the collision between hit/hurt boxes
            CheckAttackLanded();

            //Checks if the round is over
            CheckRoundOver();

            frameCount++;

            Refresh();
        }

        private void CheckWallCollisons()
        {
            const int playerWidth = 64;          // Width of player sprite

            if (player1.X <= 0) player1.X = 0;  // Stop player at left wall
            if (player1.X + playerWidth >= this.Width) player1.X = this.Width - playerWidth;  // Stop player at right wall

            if (player2.X <= 0) player2.X = 0;  // Stop player at left wall
            if (player2.X + playerWidth >= this.Width) player2.X = this.Width - playerWidth;  // Stop player at right wall
        }

        private void CheckAttackLanded()
        {
            //check if player 1 hit player 2
            if (player1.currentAttack != null && !player1.hitLanded)
            {
                Rectangle hitbox = player1.GetHitBox();


                //Get the area of where player two can be hit
                Rectangle hurtbox = player2.GetHurtBox();

                //Check if the hitbox overlaps the hurtbox to check for collison
                if (hitbox.IntersectsWith(hurtbox))
                {
                    //Take away health
                    player1.hitLanded = true; //Sets the attack as landed

                    player2.Health -= player1.currentAttack.Damage;
                    player2.stunTicks = player1.currentAttack.HitstunFrames;
                    player2.currentState = "stunned";
                    player2.currentAttack = null;
                    player2.animationCounter = 0;

                    //Add Knockback away from the attacker
                    player2.knockbackSpeed = (player1.facingRight) ? 15 : -15;
                    player2.facingRight = !player1.facingRight;

                    //Screen shake on collision
                    ScreenShake(10, 50);
                }
            }

            //check if the player 2 is attacking
            if (player2.currentAttack != null && !player2.hitLanded)
            {
                //Gets the hit box of where the player is attacking
                Rectangle hitbox = player2.GetHitBox();

                //Get the area of where player one can be hit
                Rectangle hurtbox = player1.GetHurtBox();

                //Check if the hitbox overlaps the hurtbox to check for collison
                if (hitbox.IntersectsWith(hurtbox))
                {
                    //Take away health
                    player2.hitLanded = true; //Sets the attack as landed

                    player1.Health -= player2.currentAttack.Damage;
                    player1.stunTicks = player2.currentAttack.HitstunFrames;
                    player1.currentState = "stunned";
                    player1.currentAttack = null;
                    player1.animationCounter = 0;

                    //Add Knockback away from the attacker
                    player1.knockbackSpeed = (player2.facingRight) ? 15 : -15;
                    player1.facingRight = !player2.facingRight;

                    //Screen shake on collision
                    ScreenShake(10, 50);
                }
            }
        }

        private void CheckRoundOver()
        {
            if (player1.Health <= 0)
            {
                //Player 2 wins the round
                player2RoundWins += 1;
                EndRound();
            }
            else if (player2.Health <= 0)
            {
                //Player 1 wins the round
                player1RoundWins += 1;
                EndRound();
            }

        }

        private void EndRound()
        {
            roundOver = true;

            //Check if someone won the round
            if (player1RoundWins >= roundsToWin)
            {
                player1wins = true;
                //ChangeScreen(this, new WinScreen());
                MessageBox.Show("Player 1 Wins!");//TESTING PURPOSES ONLY
                Form1.ChangeScreen(this, new WinScreen(player1.Name, "gameScreen"));

                return;
            }
            else if (player2RoundWins >= roundsToWin)
            {
                player2wins = true;
                //Player 2 wins the match
                //ChangeScreen(this, new WinScreen());
                MessageBox.Show("Player 2 Wins!");//TESTING PURPOSES ONLY
                Form1.ChangeScreen(this, new WinScreen(player2.Name, "gameScreen"));
                return;
            }
            roundEndTimer.Enabled = true;
        }

        private void roundEndTimer_Tick(object sender, EventArgs e)
        {
            roundEndTimer.Stop();
            StartNextRound();
        }

        private void StartNextRound()
        {
            player1.X = 100;
            player1.Y = 335;
            player1.Health = 100;
            player1.stunTicks = 0;
            player1.knockbackSpeed = 0;
            player1.hitLanded = false;
            player1.currentState = "idle";
            player1.currentAttack = null;
            player1.facingRight = true;

            player2.X = 600;
            player2.Y = 335;
            player2.Health = 100;
            player2.stunTicks = 0;
            player2.knockbackSpeed = 0;
            player2.hitLanded = false;
            player2.currentState = "idle";
            player2.currentAttack = null;
            player2.facingRight = false;

            // Reset round state
            roundOver = false;
            frameCount = 0;
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

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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
                    break;
                case Keys.G:
                case Keys.Q: // light attacks
                    if (player1.onGround)
                    {
                        player1.SetAttack("light2");
                    }
                    else
                    {
                        player1.SetAttack("lightAir");
                    }
                    break;
                case Keys.V:
                case Keys.E: // heavy
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
                case Keys.P:
                case Keys.NumPad1: // light attacks
                    if (player2.onGround)
                    {
                        player2.SetAttack("light2");
                    }
                    else
                    {
                        player2.SetAttack("lightAir");
                    }
                    break;
                case Keys.O:
                case Keys.NumPad3: // heavy
                    player2.SetAttack("heavy2");
                    break;

                //Game Control keypresses
                case Keys.Escape:
                    debugMode = !debugMode; // Toggle debug mode
                    break;
                case Keys.Tab:
                    tabPressed = true;
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
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

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // draw character models
            Graphics g = e.Graphics;
            player1.DrawFrame(g, frameCount);
            player2.DrawFrame(g, frameCount);

            // Draw the health bars
            int healthBarMultiplier = 2; // Width of the health bar

            // p1
            Rectangle p1HealthBackdrop = new Rectangle(30, 30, 200, 20);
            Rectangle p1Health = new Rectangle(30, 30, player1.Health * healthBarMultiplier, 20);
            g.FillRectangle(Brushes.White, p1HealthBackdrop); // Player 1 health backdrop
            g.FillRectangle(Brushes.Red, p1Health); // Player 1 health bar

            // p2
            Rectangle p2HealthBackdrop = new Rectangle(this.Width - 30 - 200, 30, 200, 20);
            Rectangle p2Health = new Rectangle(this.Width - 30 - player2.Health * healthBarMultiplier, 30, player2.Health * healthBarMultiplier, 20);
            g.FillRectangle(Brushes.White, p2HealthBackdrop); // Player 2 health backdrop
            g.FillRectangle(Brushes.Red, p2Health); // Player 2 health bar

            // 1. Setup font and brush to draw text
            Font healthFont = new Font("Arial", 14, FontStyle.Bold); // font for health
            Brush healthBrush = Brushes.Black; // color of health text

            // 2. Draw Player 1 health (top-left)
            e.Graphics.DrawString(player1.Name, healthFont, healthBrush, 90, 10);

            // 3. Draw Player 2 health (top-right)
            e.Graphics.DrawString(player2.Name, healthFont, healthBrush, this.Width - 170, 10);

            if (debugMode)
            {
                // Draw the hurtboxes (where the players can be hit) in blue
                g.DrawRectangle(Pens.Blue, player1.GetHurtBox());
                g.DrawRectangle(Pens.Blue, player2.GetHurtBox());

                // Draw the hitboxes (active attack zones) in red
                if (player1.currentAttack != null)
                    g.DrawRectangle(Pens.Red, player1.GetHitBox());

                if (player2.currentAttack != null)
                    g.DrawRectangle(Pens.Red, player2.GetHitBox());
            }
        }
    }
}