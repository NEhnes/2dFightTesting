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
        //movement properties
        float x, y;
        int xSpeed = 0;
        const int runningSpeed = 10;
        float ySpeed = 0;
        bool onGround = false;
        int floorY = 250;
        bool facingRight = true;

        //animation properties
        int animationCounter = 0;

        //attacking properties
        bool stunned = false;
        public String currentState = "idle"; // idle, attack1, attack2, jump, etc.

        // ANY VARIABLES THAT DIFFER BY SUBCLASS NEED TO BE MADE AS PROPERTIES SO THEY CAN BE OVERWRITTEN
        // ANIMATIONS ARE NOW DEPENDENT ON SUBCLASS :)
        private int _health;
        public int Health
        {
            get { return _health; }
            set { _health = value; }
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

        public Character(float _x, float _y)
        {
            x = _x;
            y = _y;
        }

        public void Move(bool _left, bool _right, bool _up)
        {
            // set lateral movement speed
            xSpeed = (_left) ? -runningSpeed : (_right) ? runningSpeed : 0;

            // jump is possible
            if (_up && onGround)
            {
                Jump();
            }

            // apply gravity
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
                }
            }

            // last direction the player moved - condensed with ternary operator
            facingRight = (_left) ? false : (_right) ? true : facingRight;

            // update pos
            x += xSpeed;
            y += ySpeed;
        }

        public void Jump()
        {
            ySpeed = -35;
            onGround = false;
        }

        public void DrawNextFrame(Graphics g, int frameCount)
        {
            // set default movement frames when not attacking
            if (!currentState.StartsWith("attack"))
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

            // initialize rectangle and image for drawing
            Rectangle rect = new Rectangle(0, 0, 1, 1);
            Image currentImage = null;

            //assign rectangle and image using switch
            switch (currentState)
            {
                case "idle":
                    rect = new Rectangle((int)x - 32, (int)y, 64, 64);
                    currentImage = IdleFrames[animationCounter]; // default frame
                    break;
                case "run":
                    rect = new Rectangle((int)x - 32, (int)y, 64, 64);
                    currentImage = RunFrames[animationCounter]; // default frame
                    break;
                case "jump":
                    rect = new Rectangle((int)x - 32, (int)y, 64, 64);
                    currentImage = JumpFrames[animationCounter % JumpFrames.Length]; // jump frame
                    break;
                case "fall":
                    rect = new Rectangle((int)x - 32, (int)y, 64, 64);
                    currentImage = FallFrames[animationCounter % FallFrames.Length]; // fall frame
                    break;
                case "attack1":
                    if (facingRight)
                    {
                        rect = new Rectangle((int)x - 80, (int)y - 18, 200, 78);
                    }
                    else
                    {
                        rect = new Rectangle((int)x - 120, (int)y - 18, 200, 78); // shift to the left
                    }
                    currentImage = Attack1Frames[animationCounter]; // attack frame
                    break;
                case "attack2":
                    if (facingRight)
                    {
                        rect = new Rectangle((int)x - 80, (int)y - 18, 200, 78);
                    }
                    else
                    {
                        rect = new Rectangle((int)x - 120, (int)y - 18, 200, 78); // shift to the left
                    }
                    currentImage = Attack2Frames[animationCounter]; // attack frame
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
                    case "attack1":
                        if (animationCounter >= Attack1Frames.Length)
                        {
                            animationCounter = 0; // reset to first frame
                            if (facingRight)
                                x += 25; // move right after attack
                            else
                            {
                                x -= 25; // move left after attack
                            }
                            currentState = "idle"; // revert to idle after attack
                        }
                        break;
                    case "attack2":
                        if (animationCounter >= Attack2Frames.Length)
                        {
                            animationCounter = 0; // reset to first frame
                            if (facingRight)
                                x += 25; // move right after attack
                            else
                            {
                                x -= 25; // move left after attack
                            }
                            currentState = "idle"; // revert to idle after attack
                        }
                        break;
                }
            }
        }

        public void SetMove(string move)
        {
            if (!stunned)
            {
                if (move.StartsWith("attack"))
                {
                    if (onGround)
                    {
                        currentState = move; // set new move
                    }
                }
                else
                {
                    currentState = move; // set new move
                }


                xSpeed = 0; // reset speeds when changing moves
                ySpeed = 0;

                animationCounter = 0; // // reset animation frame counter
            }
        }
    }
}
