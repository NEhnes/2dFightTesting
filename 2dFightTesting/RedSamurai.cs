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
    public class RedSamurai : Character 
    {
        // XML READ HITBOXES AND HURTBOXES
        // tons of data which takes up code space
        public RedSamurai(float _x, float _y) : base(_x, _y)
        {
            // subclass specific details below
            Name = "Red Samurai";

            Damage = 0;

            IdleFrames = new Image[8] { Properties.Resources.idle1, Properties.Resources.idle2 , Properties.Resources.idle3 ,
                                            Properties.Resources.idle4, Properties.Resources.idle5, Properties.Resources.idle6,
                                            Properties.Resources.idle7,  Properties.Resources.idle8 };
            RunFrames = new Image[8] { Properties.Resources.run1, Properties.Resources.run2 , Properties.Resources.run3 ,
                                            Properties.Resources.run4, Properties.Resources.run5, Properties.Resources.run6,
                                            Properties.Resources.run7,  Properties.Resources.run8 };
            Attack1Frames = new Image[6] { Properties.Resources.attack1_1, Properties.Resources.attack1_2 , Properties.Resources.attack1_3 ,
                                            Properties.Resources.attack1_4, Properties.Resources.attack1_5, Properties.Resources.attack1_6 };
            Attack2Frames = new Image[6] { Properties.Resources.attack2_1, Properties.Resources.attack2_2 , Properties.Resources.attack2_3 ,
                                            Properties.Resources.attack2_4, Properties.Resources.attack2_5, Properties.Resources.attack2_6 };
            Attack3Frames = new Image[3] { Properties.Resources.light_air1, Properties.Resources.light_air3, Properties.Resources.light_air4 };

            JumpFrames = new Image[2] { Properties.Resources.jump1, Properties.Resources.jump2 };
            FallFrames = new Image[2] { Properties.Resources.fall1, Properties.Resources.fall2 };
            DamagedFrames = new Image[4] { Properties.Resources.damaged1, Properties.Resources.damaged2, Properties.Resources.damaged3, Properties.Resources.damaged4 };

            Light2 = new Attack("Light2", 4, 2, 0, 0, 0,
                new List<Rectangle> { new Rectangle(50, 0, 50, 50) }, // Hitboxes
                new List<Rectangle> { new Rectangle(0, 0, 100, 100) }, // Hurtboxes
                Attack1Frames.ToList() // frames
            );
            Heavy2 = new Attack("Heavy2", 4, 2, 0, 0, 0,
                new List<Rectangle> { new Rectangle(50, 0, 50, 50) }, // Hitboxes
                new List<Rectangle> { new Rectangle(0, 0, 100, 100) }, // Hurtboxes
                Attack2Frames.ToList() // frames
            );
            LightAir = new Attack("LightAir", 1, 1, 0, 0, 0,
                new List<Rectangle> { new Rectangle(80, 10, 40, 25) }, // Hitboxes
                new List<Rectangle> { new Rectangle(0, 0, 100, 100) }, // Hurtboxes
                Attack3Frames.ToList() // frames
            );
        }

        public override Rectangle GetHurtBox()
        {
            if (currentAttack != null)
            {
                switch (currentAttack.Name)
                {
                    case "Light2":
                        if (facingRight) return new Rectangle((int)X + 40, (int)Y + 10, 30, 54); // not confirmed
                        else return new Rectangle((int)X - 8, (int)Y + 10, 30, 54);
                    case "Heavy2":
                        if (facingRight) return new Rectangle((int)X + 45, (int)Y + 15, 35, 54); // not confirmed
                        else return new Rectangle((int)X - 15, (int)Y + 15, 35, 54);
                    case "LightAir":
                        if (facingRight) return new Rectangle((int)X + 45, (int)Y + 15, 35, 54); // not confirmed
                        else return new Rectangle((int)X - 15, (int)Y + 15, 35, 54);
                }

            }

            // idle/running/jumping/falling hurtbox
            if (facingRight) return new Rectangle((int)X + 22, (int)Y + 5, 30, 59);
            else return new Rectangle((int)X + 10, (int)Y + 5, 30, 59);
        }
    }
}
