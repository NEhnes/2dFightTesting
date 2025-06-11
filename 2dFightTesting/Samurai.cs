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
    public class Samurai : Character
    {
        // XML READ HITBOXES AND HURTBOXES
        // tons of data which takes up code space
        public Samurai(float _x, float _y) : base(_x, _y)
        {
            // subclass specific details below
            Health = 100;
            IdleFrames = new Image[8] { Properties.Resources.idle1, Properties.Resources.idle2 , Properties.Resources.idle3 ,
                                            Properties.Resources.idle4, Properties.Resources.idle5, Properties.Resources.idle6,
                                            Properties.Resources.idle7,  Properties.Resources.idle8 };
            RunFrames = new Image[8] { Properties.Resources.run1, Properties.Resources.run2 , Properties.Resources.run3 ,
                                            Properties.Resources.run4, Properties.Resources.run5, Properties.Resources.run6,
                                            Properties.Resources.run7,  Properties.Resources.run8 };
            Attack1Frames = new Image[6] { Properties.Resources.attack1_1, Properties.Resources.attack1_2 , Properties.Resources.attack1_3 ,
                                            Properties.Resources.attack1_4, Properties.Resources.attack1_5, Properties.Resources.attack1_5 };
            Attack2Frames = new Image[6] { Properties.Resources.attack2_1, Properties.Resources.attack2_2 , Properties.Resources.attack2_3 ,
                                            Properties.Resources.attack2_4, Properties.Resources.attack2_5, Properties.Resources.attack2_6 };
            JumpFrames = new Image[2] { Properties.Resources.jump1, Properties.Resources.jump2 };
            FallFrames = new Image[2] { Properties.Resources.fall1, Properties.Resources.fall2 };

            // init attacks - testing for now
            Light2 = new Attack(4, 2, 0, 0, 0,
                new List<Rectangle> { new Rectangle(50, 0, 50, 50) }, // Hitboxes
                new List<Rectangle> { new Rectangle(0, 0, 100, 100) }, // Hurtboxes
                Attack1Frames.ToList() // frames
            );
        }
    }
}
