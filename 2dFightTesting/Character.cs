using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2dFightTesting
{
    public class Character
    {
        #region fields
        //movement attributes
        float x, y;
        int xSpeed = 0;
        const int runningSpeed = 10; // make as property
        float ySpeed = 0;
        public bool onGround = false;
        int floorY = 335;
        public bool facingRight = true;

        public int stunTicks = 0; //amount of frames the player is stunned for
        public float knockbackSpeed = 0; //How much the player is pushed back when hit

        private Stopwatch jumpBufferStopwatch = new Stopwatch(); // jump buffer

        //Double Jump attributes    
        public int jumpCounter = 0;
        public int maxJumps = 2;
        public bool isJumping = false;


        //animation attributes
        public int animationCounter = 0;

        //attacking attributes
        bool stunned = false;
        public bool hitLanded = false; //to check if the current hit has landed
        public String currentState = "idle"; // idle, attack1, attack2, jump, etc.
        public Attack currentAttack = null;
        #endregion

        #region properties
        // ANY VARIABLES THAT DIFFER BY SUBCLASS NEED TO BE MADE AS PROPERTIES SO THEY CAN BE OVERWRITTEN
        public string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private int _health;
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        Image[] _idleFrames;
        public Image[] IdleFrames
        {
            get { return _idleFrames; }
            set { _idleFrames = value; }
        }

        Image[] _runFrames;
        public Image[] RunFrames
        {
            get { return _runFrames; }
            set { _runFrames = value; }
        }

        Image[] _attack1Frames;
        public Image[] Attack1Frames
        {
            get { return _attack1Frames; }
            set { _attack1Frames = value; }
        }

        Image[] _attack2Frames;
        public Image[] Attack2Frames
        {
            get { return _attack2Frames; }
            set { _attack2Frames = value; }
        }

        Image[] _attack3Frames;
        public Image[] Attack3Frames
        {
            get { return _attack3Frames; }
            set { _attack3Frames = value; }
        }

        Image[] _jumpFrames;
        public Image[] JumpFrames
        {
            get { return _jumpFrames; }
            set { _jumpFrames = value; }
        }

        Image[] _fallFrames;
        public Image[] FallFrames
        {
            get { return _fallFrames; }
            set { _fallFrames = value; }
        }

        Image[] _damagedFrames;
        public Image[] DamagedFrames
        {
            get { return _damagedFrames; }
            set { _damagedFrames = value; }
        }

        Attack _light2;
        public Attack Light2
        {
            get { return _light2; }
            set { _light2 = value; }
        }

        Attack _heavy2;
        public Attack Heavy2
        {
            get { return _heavy2; }
            set { _heavy2 = value; }
        }

        Attack _lightAir;
        public Attack LightAir
        {
            get { return _lightAir; }
            set { _lightAir = value; }
        }
        #endregion

        public Character(float _x, float _y)
        {
            x = _x;
            y = _y;
        }

        public void Move(bool _left, bool _right, bool _up)
        {
            //if player is stunned then no controls should work
            if (stunTicks > 0)
            {
                stunTicks--;
                if (stunTicks == 0) currentState = "idle"; // reset state to idle after stun ends
                //Knockback during stun
                x += knockbackSpeed; //Move the player by the knockback amonut

                //Reduce the knockback amount each frame giving a nice friction effect
                knockbackSpeed *= 0.85f;

                //Apply gravity fall when stunned
                ApplyGravity();
                y += ySpeed;
                return;

            }
            // set lateral movement speed
            xSpeed = (_left) ? -runningSpeed : (_right) ? runningSpeed : 0;

            //skip the rest if attacking on ground
            if (currentAttack != null && onGround) return;

            // jump if possible
            if (_up && jumpCounter < maxJumps) Jump();

            ApplyGravity();

            // last direction the player moved - condensed with ternary operator
            facingRight = (_left) ? false : (_right) ? true : facingRight;

            // update pos
            x += xSpeed;
            y += ySpeed;

            // set default movement frames when not attacking
            if (currentAttack == null)
            {
                if (onGround)
                {
                    currentState = (xSpeed == 0) ? "idle" : "run";
                }
                else
                {
                    currentState = (ySpeed <= 0) ? "jump" : "fall";
                }
            }
        }

        public void Jump()
        {
            if (!jumpBufferStopwatch.IsRunning)
            {
                ySpeed = -35;
                onGround = false;
                jumpBufferStopwatch.Restart(); // reset jump buffer timer
                jumpCounter++; //Increase jump counter
            }
            if (jumpBufferStopwatch.ElapsedMilliseconds > 100) // reset jump buffer after 200ms
            {
                jumpBufferStopwatch.Stop();
                jumpBufferStopwatch.Reset();
            }
        }

        public void ApplyGravity()
        {
            if (!onGround)
            {
                if (y <= floorY)
                {
                    ySpeed += (float)(9.8 * 0.4);
                }

                else if (y > floorY)
                {
                    y = floorY - 1;
                    ySpeed = 0;
                    onGround = true;
                    jumpCounter = 0;
                }
            }
        }

        public void DrawFrame(Graphics g, int frameCount)  //uses Attack data
        {
            // initialize rectangle and image for drawing
            Rectangle rect = new Rectangle(0, 0, 1, 1);
            Image currentImage = null;

            //assign rectangle and image using switch
            switch (currentState)
            {
                case "idle":
                    if (animationCounter >= IdleFrames.Length) animationCounter = 0; // reset to first frame if needed (some attacks have more elements)
                    rect = new Rectangle((int)x, (int)y, 64, 64);
                    currentImage = IdleFrames[animationCounter]; // default frame
                    break;
                case "run":
                    rect = new Rectangle((int)x, (int)y, 64, 64);
                    currentImage = RunFrames[animationCounter]; // default frame
                    break;
                case "jump":
                    rect = new Rectangle((int)x, (int)y, 64, 64);
                    currentImage = JumpFrames[animationCounter % JumpFrames.Length]; // jump frame
                    break;
                case "fall":
                    rect = new Rectangle((int)x, (int)y, 64, 64);
                    currentImage = FallFrames[animationCounter % FallFrames.Length]; // fall frame
                    break;
                case "stunned":
                    rect = new Rectangle((int)x, (int)y, 64, 64);
                    currentImage = DamagedFrames[animationCounter]; // fall frame
                    break;
                case "attack1":
                    if (facingRight)
                    {
                        rect = new Rectangle((int)x - 48, (int)y - 18, 200, 78);
                    }
                    else
                    {
                        rect = new Rectangle((int)x - 88, (int)y - 18, 200, 78); // shift to the left
                    }
                    currentImage = Light2.Frames[animationCounter]; // attack frame
                    break;
                case "attack2":
                    if (facingRight)
                    {
                        rect = new Rectangle((int)x - 48, (int)y - 18, 200, 78);
                    }
                    else
                    {
                        rect = new Rectangle((int)x - 88, (int)y - 18, 200, 78); // shift to the left
                    }
                    currentImage = Heavy2.Frames[animationCounter]; // attack frame
                    break;
                case "lightAir":
                    if (facingRight)
                    {
                        rect = new Rectangle((int)x - 48, (int)y - 18, 200, 78);
                    }
                    else
                    {
                        rect = new Rectangle((int)x - 88, (int)y - 18, 200, 78); // shift to the left
                    }
                    currentImage = LightAir.Frames[animationCounter]; // attack frame
                    break;
            }


            if (facingRight) // draw normally
            {
                g.DrawImage(currentImage, rect);
            }
            else // draw flipped horizontally
            {
                Image flippedImage = new Bitmap(currentImage);
                flippedImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                g.DrawImage(flippedImage, rect);
            }


            if (frameCount % 3 == 0) // change frame every 3 ticks
            {
                animationCounter++;

                switch (currentState)
                {
                    case "idle":
                        if (animationCounter >= IdleFrames.Length)
                        {
                            animationCounter = 0; // reset to first frame
                        }
                        break;
                    case "run":
                        if (animationCounter >= RunFrames.Length)
                        {
                            animationCounter = 0; // reset to first frame
                        }
                        break;
                    case "jump":
                        if (animationCounter >= JumpFrames.Length)
                        {
                            animationCounter = 0; // reset to first frame
                        }
                        break;
                    case "fall":
                        if (animationCounter >= FallFrames.Length)
                        {
                            animationCounter = 0; // reset to first frame
                        }
                        break;
                    case "stunned":
                        if (animationCounter >= DamagedFrames.Length)
                        {
                            animationCounter = DamagedFrames.Length - 1; // reset to first frame
                        }
                        break;
                    case "attack1":
                        if (animationCounter >= Light2.Frames.Count)
                        {
                            animationCounter = 0; // reset to first frame
                            currentState = "idle"; // revert to idle after attack
                            currentAttack = null;
                            hitLanded = false;

                            x += (facingRight) ? 25 : -25; // move right or left after attack to match animation
                        }
                        break;
                    case "attack2":
                        if (animationCounter >= Attack2Frames.Length)
                        {
                            animationCounter = 0; // reset to first frame
                            currentState = "idle"; // revert to idle after attack
                            currentAttack = null;
                            hitLanded = false;
                        }
                        break;
                    case "lightAir":
                        if (animationCounter >= LightAir.Frames.Count)
                        {
                            animationCounter = 0; // reset to first frame
                            currentState = "idle"; // revert to idle after attack
                            currentAttack = null;
                            hitLanded = false;
                            x += (facingRight) ? 20 : -20; // move right or left after attack to match animation
                        }
                        break;
                }
            }
        }

        // rectangle area that can be hit by an attack
        public virtual Rectangle GetHurtBox()
        {
            // default -> gets changed by character via polymorphism
            return new Rectangle((int)x, (int)y, 64, 64);
        }

        // region where an attack is active and can Health opponent
        public Rectangle GetHitBox() //uses Attack Data
        {
            // return an empty rectangle if animation is not in active frames
            if (animationCounter < currentAttack.StartupFrames) return new Rectangle(0, 0, 0, 0);
            if (animationCounter >= currentAttack.StartupFrames + currentAttack.ActiveFrames) return new Rectangle(0, 0, 0, 0);
            // return empty rectangle if not attacking
            if (currentAttack == null) return new Rectangle(0, 0, 0, 0);

            // hitbox for the current frame of the animation
            Rectangle attackBox = currentAttack.Hitboxes[0];

            // if the character is facing left, we need to flip the hitbox
            if (!facingRight)
            {
                return new Rectangle((int)(x - attackBox.Width), (int)y + attackBox.Y, attackBox.Width, attackBox.Height);
            }
            //if the character is facing right, we don't need to flip the hitbox
            else
            {
                return new Rectangle((int)(x + attackBox.X), (int)y + attackBox.Y, attackBox.Width, attackBox.Height);
            }
        }

        public void SetAttack(string attackName)
        {
            // no moves if stunned
            if (stunTicks > 0) return;

            // dont set attack if already attacking
            if (currentAttack != null) return;


            if (onGround) // ground attacks
            {
                switch (attackName)
                {
                    case "light2":
                        currentAttack = Light2;
                        currentState = "attack1";
                        break;
                    case "heavy2":
                        currentAttack = Heavy2;
                        currentState = "attack2";
                        break;
                    default:
                        return; // invalid attack
                }
                hitLanded = false; //rest hit landed for next attack
            }
            else // air attacks
            {
                switch (attackName)
                {
                    case "lightAir":
                        currentAttack = LightAir;
                        currentState = "lightAir";
                        break;
                    default:
                        return;
                }
                hitLanded = false; //rest hit landed for next attack
            }

            xSpeed = 0; // reset speeds when changing moves

            animationCounter = 0; // // reset animation frame counter
        }
    }
}
