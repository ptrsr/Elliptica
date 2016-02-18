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
        Trigger trigger;

        Sound triggersound;
        public Ball(Vec2 spawnPos) : base("Solidball", spawnPos, 8)
        {
            triggersound = new Sound("trigger activate.wav");
        }
        void Update()
        {
            timer++;
            Step();
            CheckCollisionsTrigger();
        }

    private void CheckCollisionsTrigger()
        {
            
            trigger = TMXLevel.Return().CheckTriggerCollisions(this);
            if(trigger != null)
            {
                triggersound.Play();
                this.Destroy();
                trigger.triggerAnim.SetFrame(1);
            }
        }
    }
}
