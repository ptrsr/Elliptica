using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Trigger : Sprite
    {
        public AnimSprite triggerAnim;

        public Trigger() : base("hitbox.png")
        {
            alpha = 0.0f;
            triggerAnim = new AnimSprite("trigger.png", 3, 1);

            triggerAnim.x -= 25;
            triggerAnim.y -= 16;
            AddChild(triggerAnim);
        }
        public int GetFrame()
        {
            return triggerAnim.currentFrame;
        }
    }
}
