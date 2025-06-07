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
        float x, y;
        float ySpeed = 0;
        int xSpeed = 0;
        const int absSpeed = 10;
        int floorY = 250;
        int animationCounter = 0;
        bool onGround = false;
        bool facingRight = true;

        public Attack currentAttack = null;
        private int currentAttackFrame = 0;
        private bool isAttacking = false;

        public string currentMove = "idle";

        Image[] idleFrames = new Image[]
        {
            Properties.Resources.idle1, Properties.Resources.idle2, Properties.Resources.idle3,
            Properties.Resources.idle4, Properties.Resources.idle5, Properties.Resources.idle6,
            Properties.Resources.idle7, Properties.Resources.idle8
        };

        Image[] runFrames = new Image[]
        {
            Properties.Resources.run1, Properties.Resources.run2, Properties.Resources.run3,
            Properties.Resources.run4, Properties.Resources.run5, Properties.Resources.run6,
            Properties.Resources.run7, Properties.Resources.run8
        };

        Image[] jumpFrames = new Image[] { Properties.Resources.jump1, Properties.Resources.jump2 };
        Image[] fallFrames = new Image[] { Properties.Resources.fall1, Properties.Resources.fall2 };

        public Character(float _x, float _y)
        {
            x = _x;
            y = _y;
        }

        public void Move(bool _left, bool _right, bool _up)
        {
            xSpeed = (_left) ? -absSpeed : (_right) ? absSpeed : 0;

            if (_up && onGround)
            {
                Jump();
            }

            if (!onGround)
            {
                ySpeed += (float)(9.8 * 0.4);
                if (y > floorY)
                {
                    y = floorY - 1;
                    ySpeed = 0;
                    onGround = true;
                }
            }

            if (_left) facingRight = false;
            if (_right) facingRight = true;

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
            // Handle active attack animation
            if (isAttacking && currentAttack != null)
            {
                if (currentAttackFrame < currentAttack.TotalFrames)
                {
                    Image attackFrameImage = currentAttack.Frames[Math.Min(currentAttackFrame, currentAttack.Frames.Count - 1)];
                    Rectangle rect = new Rectangle((int)x - 32, (int)y - 32, 128, 128);

                    if (facingRight)
                        g.DrawImage(attackFrameImage, rect);
                    else
                    {
                        Image flipped = new Bitmap(attackFrameImage);
                        flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        g.DrawImage(flipped, rect);
                    }

                    if (frameCount % 2 == 0)
                        currentAttackFrame++;
                }
                else
                {
                    isAttacking = false;
                    currentAttack = null;
                    currentAttackFrame = 0;
                }

                return; // Skip base animation
            }

            // Movement logic
            if (onGround)
                currentMove = (xSpeed == 0) ? "idle" : "run";
            else
                currentMove = (ySpeed <= 0) ? "jump" : "fall";

            Rectangle drawRect = new Rectangle((int)x - 32, (int)y, 64, 64);
            Image currentImage = idleFrames[0];

            switch (currentMove)
            {
                case "idle":
                    currentImage = idleFrames[animationCounter];
                    break;
                case "run":
                    currentImage = runFrames[animationCounter];
                    break;
                case "jump":
                    currentImage = jumpFrames[animationCounter % jumpFrames.Length];
                    break;
                case "fall":
                    currentImage = fallFrames[animationCounter % fallFrames.Length];
                    break;
            }

            if (facingRight)
                g.DrawImage(currentImage, drawRect);
            else
            {
                Image flippedImage = new Bitmap(currentImage);
                flippedImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                g.DrawImage(flippedImage, drawRect);
            }

            // Frame counter update
            if (frameCount % 3 == 0)
            {
                animationCounter++;

                if (currentMove == "idle" && animationCounter >= idleFrames.Length)
                    animationCounter = 0;
                else if (currentMove == "run" && animationCounter >= runFrames.Length)
                    animationCounter = 0;
                else if ((currentMove == "jump" || currentMove == "fall") && animationCounter >= 2)
                    animationCounter = 0;
            }
        }

        public void StartAttack(Attack attack)
        {
            if (onGround && !isAttacking)
            {
                currentAttack = attack;
                currentAttackFrame = 0;
                isAttacking = true;
                xSpeed = 0;
                ySpeed = 0;
            }
        }
    }
}
