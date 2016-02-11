using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Projectiles : Sprite
    {
        public Projectiles() : base("checkers.png")
        {
            SetOrigin(width / 2, height / 2);
        }

        void Update ()
        {
            Move(15, 0);
        }



    }
}
