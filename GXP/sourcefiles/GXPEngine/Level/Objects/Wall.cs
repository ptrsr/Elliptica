using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Wall : AnimSprite
    {
        public Wall(int frame) : base("tilesheet.png",9,5)
        {
            SetFrame(frame);
        }
    }
}
