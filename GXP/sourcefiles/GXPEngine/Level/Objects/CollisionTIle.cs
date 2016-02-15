using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class CollisionTile : AnimSprite
    {
        public string type;
        public CollisionTile(int frame) : base("tilesheet.png",9,11)
        {
            SetFrame(frame);
            if (frame < 9)
                type = "black";
            else if (frame >= 9 && frame < 18)
                type = "white";
            else if (frame >= 18 && frame < 27)
                type = "gray";

            if (frame <= 27)
            {
                TMXLevel.Return().collisionSprites.Add(this);
            }
        }
    }
}
