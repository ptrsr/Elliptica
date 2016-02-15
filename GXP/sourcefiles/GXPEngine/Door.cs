﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Door : AnimationSprite
    {
        private float frame;
        public Door() : base("door.png", 17, 1)
        {
            SetFrame(0);
        }

        public void ActivateDoor()
        {
            SetFrame(1);
        }

        public int DistanceTo(float x, float y)
        {
            float dx = x - this.x;
            float dy = y - this.y;
            return (int)Math.Sqrt(dx * dx + dy * dy);
        }
        public void UpdateAnimation(int distance)
        {
            if (distance > 200)
                frame -= 1;
            else frame += 1;
            SetFrame((int)frame);
        }
    }
}