using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Cabel : AnimSprite
    {
        public Cabel(int frame) : base("tilesheet.png", 9, 11)
        {
            SetFrame(frame);
        }

        void Update()
        {
            ChangeColor();
        }

        private void ChangeColor()
        {
            Trigger trigger = TMXLevel.Return().GetTrigger();
            Trigger trigger2 = TMXLevel.Return().GetRedTrigger();
            Ball ball = TMXLevel.Return().GetBall();
            if (trigger != null)
            {
                if (trigger.triggerAnim.currentFrame == 1)
                    color = 0xFF66FFFF;
            }

            if (trigger2 != null)
            {
                if (trigger2.triggerAnim.currentFrame == 2)
                    color = 0xFFFF3333;
            }
            if (trigger != null & trigger2 != null)
            if (trigger.triggerAnim.currentFrame == 0 & trigger2.triggerAnim.currentFrame == 0)
                SetColor(255, 255, 255);
            if (trigger == null && ball == null)
                color = 0xFF66FFFF;
        }
    }
}
