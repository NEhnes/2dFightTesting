using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2dFightTesting
{
    public class Attack
    {
        // need to add x change after attack (ex. attack1 ends to the right)
        public string Name;
        public int StartupFrames;
        public int ActiveFrames;
        public int Damage;
        public int TotalFrames => StartupFrames + ActiveFrames;

        public int HitstunFrames;
        public int HitstopFrames;

        public List<Rectangle> Hitboxes;
        public List<Rectangle> Hurtboxes;
        public List<Image> Frames;

        public Attack(String _name, int _startup, int _active, int _damage, int _hitstun, int _hitstop, List<Rectangle> _hitboxes, List<Rectangle> _hurtboxes, List<Image> _frames)
        {
            Name = _name;
            StartupFrames = _startup;
            ActiveFrames = _active;
            Damage = _damage;
            HitstunFrames = _hitstun;
            HitstopFrames = _hitstop;
            Hitboxes = _hitboxes;
            Hurtboxes = _hurtboxes;
            Frames = _frames;
        }
    }
}