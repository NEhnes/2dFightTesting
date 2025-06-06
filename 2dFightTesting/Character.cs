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
        int xSpeed = 0;
        const int absSpeed = 10;
        float ySpeed = 0;
        bool onGround = false;

        int floorY = 250;
        int animationCounter = 0;

        bool stunned = false;

        public String currentMove = "idle"; // idle, attack1, attack2, jump, etc.

        Image[] idleFrames = new Image[8] { Properties.Resources.idle1, Properties.Resources.idle2 , Properties.Resources.idle3 ,
                                            Properties.Resources.idle4, Properties.Resources.idle5, Properties.Resources.idle6,
                                            Properties.Resources.idle7,  Properties.Resources.idle8 };

        Image[] runFrames = new Image[8] { Properties.Resources.run1, Properties.Resources.run2 , Properties.Resources.run3 ,
                                            Properties.Resources.run4, Properties.Resources.run5, Properties.Resources.run6,
                                            Properties.Resources.run7,  Properties.Resources.run8 };

        Image[] attack1Frames = new Image[6] { Properties.Resources.attack1_1, Properties.Resources.attack1_2 , Properties.Resources.attack1_3 ,
                                            Properties.Resources.attack1_4, Properties.Resources.attack1_5, Properties.Resources.attack1_6 };

        


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

            x += xSpeed;
            y += ySpeed;
        }

        public void Jump()
        {
            ySpeed = -35;
            onGround = false;
        }

        public void GetNextFrame(Graphics g, int frameCount)
        {
            if(xSpeed != 0 && onGround) currentMove = "run";
            if (xSpeed == 0 && currentMove == "run") currentMove = "idle";

            Rectangle rect = new Rectangle(0, 0, 1, 1);
            Image currentImage = null;

            switch (currentMove)
            {
                case "idle":
                    rect = new Rectangle((int)x - 32, (int)y, 64, 64);
                    currentImage = idleFrames[animationCounter]; // default frame
                    break;
                case "run":
                    rect = new Rectangle((int)x - 32, (int)y, 64, 64);
                    currentImage = runFrames[animationCounter]; // default frame
                    break;
                case "attack1":
                    rect = new Rectangle((int)x - 80, (int)y - 18, 200, 78);
                    currentImage = attack1Frames[animationCounter]; // attack frame
                    break;
            }

            //Rectangle rect = new Rectangle((int)x - 32, (int)y, 64, 64);
            //Image currentImage = idleFrames[animationCounter];

            g.DrawImage(currentImage, rect); //character frame

            if (frameCount % 3 == 0) // change frame every 5 ticks
            {
                animationCounter++;

                switch (currentMove)
                {
                    case "idle":
                        if (animationCounter >= idleFrames.Length)
                        {
                            animationCounter = 0; // reset to first frame
                        }
                        break;
                    case "run":
                        if (animationCounter >= runFrames.Length)
                        {
                            animationCounter = 0; // reset to first frame
                        }
                        break;
                    case "attack1":
                        if (animationCounter >= attack1Frames.Length)
                        {
                            animationCounter = 0; // reset to first frame
                            x += 25;
                            currentMove = "idle"; // reset to idle after attack
                        }
                        break;
                }
            }
        }

        public void SetMove(string move)
        {
            if (currentMove ==  "idle" && !stunned)
            {
                if (move == "attack1")
                {
                    if (onGround)
                    {
                        currentMove = move; // set new move
                    }
                }
                else
                {
                    currentMove = move; // set new move
                }
                

                xSpeed = 0; // reset speeds when changing moves
                ySpeed = 0;

                animationCounter = 0; // reset animation frame counter
            }
        }
    }
}
