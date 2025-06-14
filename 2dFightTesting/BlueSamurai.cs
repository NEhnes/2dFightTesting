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
    public class BlueSamurai : Character
    {
        // XML READ HITBOXES AND HURTBOXES
        // tons of data which takes up code space
        public BlueSamurai(float _x, float _y) : base(_x, _y)
        {
            // subclass specific details below
            Damage = 0;

            IdleFrames = new Image[4] { Properties.Resources.blue_idle1, Properties.Resources.blue_idle2, Properties.Resources.blue_idle3,
                                            Properties.Resources.blue_idle4 };
            RunFrames = new Image[8] { Properties.Resources.blue_run1, Properties.Resources.blue_run2 , Properties.Resources.blue_run3 ,
                                            Properties.Resources.blue_run4, Properties.Resources.blue_run5, Properties.Resources.blue_run6,
                                            Properties.Resources.blue_run7,  Properties.Resources.blue_run8 };
            Attack1Frames = new Image[4] { Properties.Resources.blue_attack1_1, Properties.Resources.blue_attack1_2 , Properties.Resources.blue_attack1_3 ,
                                            Properties.Resources.blue_attack1_4 };
            Attack2Frames = new Image[4] { Properties.Resources.blue_attack2_1, Properties.Resources.blue_attack2_2 , Properties.Resources.blue_attack2_3 ,
                                            Properties.Resources.blue_attack2_4 };
            Attack3Frames = new Image[3] { Properties.Resources.light_air1, Properties.Resources.light_air3, Properties.Resources.light_air4 };

            JumpFrames = new Image[2] { Properties.Resources.blue_jump1, Properties.Resources.blue_jump2 };
            FallFrames = new Image[2] { Properties.Resources.blue_fall1, Properties.Resources.blue_fall2 };
            DamagedFrames = new Image[4] { Properties.Resources.blue_damaged1, Properties.Resources.blue_damaged2, Properties.Resources.blue_damaged3, Properties.Resources.blue_damaged4 };

            Light2 = new Attack(2, 1, 0, 0, 0,
                new List<Rectangle> { new Rectangle(50, 0, 50, 50) }, // Hitboxes
                new List<Rectangle> { new Rectangle(0, 0, 100, 100) }, // Hurtboxes
                Attack1Frames.ToList() // frames
            );
            Heavy2 = new Attack(2, 1, 0, 0, 0,
                new List<Rectangle> { new Rectangle(50, 0, 50, 50) }, // Hitboxes
                new List<Rectangle> { new Rectangle(0, 0, 100, 100) }, // Hurtboxes
                Attack2Frames.ToList() // frames
            );
            LightAir = new Attack(1, 2, 0, 0, 0,
                new List<Rectangle> { new Rectangle(50, 0, 50, 50) }, // Hitboxes
                new List<Rectangle> { new Rectangle(0, 0, 100, 100) }, // Hurtboxes
                Attack3Frames.ToList() // frames
            );
        }
    }
}
