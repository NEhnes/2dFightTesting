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
    public partial class GameScreen : UserControl
    {

        RedSamurai player1 = new RedSamurai(100, 100);

        bool aPressed = false;
        bool dPressed = false;
        bool wPressed = false;
        bool tabPressed = false;

        BlueSamurai player2 = new BlueSamurai(600, 100);
        bool leftPressed = false;
        bool rightPressed = false;
        bool upPressed = false;

        int frameCount = 0;

        bool debugMode = true;

        //Variables for round system
        int player1RoundWins = 0;
        int player2RoundWins = 0;
        const int maxDamage = 100;           //Do health not damage if we dont have time
        const int roundsToWin = 2;
        bool roundOver = false;
        string roundWinner = "";

        public GameScreen()
        {
            InitializeComponent();
            gameTimer.Enabled = true;
            roundEndTimer.Interval = 2000;

        }

        private void gameTimer_Tick_1(object sender, EventArgs e)
        {
            //dont update if the round is over
            if (roundOver) return;

            //Move players
            player1.Move(aPressed, dPressed, wPressed);
            player2.Move(leftPressed, rightPressed, upPressed);

            CheckWallCollisons();

            //Checks for the collision between hit/hurt boxes
            CheckAttackLanded();

            //Checks if the round is over
            CheckRoundOver();

            frameCount++;

            Refresh();
        }

        private void CheckRoundOver()
        {
            if (player1.Damage >= maxDamage)
            {
                //Player 2 wins the round
                player2RoundWins += 1;
                roundWinner = "Player 2";
                EndRound();
            }
            else if (player2.Damage >= maxDamage)
            {
                //Player 1 wins the round
                player1RoundWins += 1;
                roundWinner = "Player 1";
                EndRound();
            }

        }

        private void EndRound()
        {
            roundOver = true;

            //Check if someone won the round
            if (player1RoundWins >= roundsToWin)
            {
                //ChangeScreen(this, new WinScreen());
                MessageBox.Show("Player 1 Wins!");//TESTING PURPOSES ONLY
                return;
            }
            else if (player2RoundWins >= roundsToWin)
            {
                //Player 2 wins the match
                //ChangeScreen(this, new WinScreen());
                MessageBox.Show("Player 2 Wins!");//TESTING PURPOSES ONLY
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
            player1.Y = 250;
            player1.Damage = 0;
            player1.stunTimer = 0;
            player1.knockbackSpeed = 0;
            player1.hitLanded = false;
            player1.currentState = "idle";
            player1.currentAttack = null;
            player1.facingRight = true;

            player2.X = 600;
            player2.Y = 250;
            player2.Damage = 0;
            player2.stunTimer = 0;
            player2.knockbackSpeed = 0;
            player2.hitLanded = false;
            player2.currentState = "idle";
            player2.currentAttack = null;
            player2.facingRight = false;

            // Reset round state
            roundOver = false;
            roundWinner = "";
            frameCount = 0;
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
                case Keys.Q: // light attacks
                    if (player1.onGround) player1.SetAttack("light2");
                    else player1.SetAttack("lightAir");
                    break;
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
                    if (player2.onGround) player2.SetAttack("light2");
                    else player2.SetAttack("lightAir");
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

                    player2.Damage += 10;
                    player2.stunTimer = 20;
                    player2.currentState = "stunned";
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

                    player1.Damage += 10;
                    player1.stunTimer = 20;
                    player1.currentState = "stunned";
                    player1.animationCounter = 0;

                    //Add Knockback away from the attacker
                    player1.knockbackSpeed = (player2.facingRight) ? 15 : -15;
                    player1.facingRight = !player2.facingRight;

                    //Screen shake on collision
                    ScreenShake(10, 50);
                }
            }
        }

        private void CheckWallCollisons()
        {
            const int playerWidth = 64;          // Width of player sprite

            if (player1.X <= 0) player1.X = 0;  // Stop player at left wall
            if (player1.X + playerWidth >= this.Width) player1.X = this.Width - playerWidth;  // Stop player at right wall

            if (player2.X <= 0) player2.X = 0;  // Stop player at left wall
            if (player2.X + playerWidth >= this.Width) player2.X = this.Width - playerWidth;  // Stop player at right wall
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

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            player1.DrawFrame(g, frameCount);
            player2.DrawFrame(g, frameCount);

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
