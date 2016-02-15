using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    public class Ball : Projectile
    {
        public int timer;

        public Ball(Vec2 spawnPos) : base("Solidball", spawnPos, 8)
        {

        }
        void Update()
        {
            timer++;
            Step();
        }
    }
}
