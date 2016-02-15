using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class PortalBall : Projectile
    {
        public PortalBall(Vec2 spawnPos) : base("Solidball", spawnPos, 10)
        {

        }

        void Update()
        {
            Step();
        }

        public override void Step()
        {
            int direction;
            GameObject tiledObject;

            // X Collision
            velocity.x += acceleration.x;
            position.x += velocity.x;
            x = position.x;

            tiledObject = TMXLevel.Return().CheckCollision(this);

            if (tiledObject != null)
            {
                direction = velocity.x > 0 ? -1 : 1;

                position.x = tiledObject.x + 16 + direction * (width / 2 + 17);
                velocity.x *= -1;
            }
            x = position.x;

            // Y Collision
            acceleration.y += _gravity;
            velocity.y += acceleration.y;
            position.y += velocity.y;
            y = position.y;

            tiledObject = TMXLevel.Return().CheckCollision(this);

            if (tiledObject != null)
            {
                direction = velocity.y > 0 ? -1 : 1;

                position.y = tiledObject.y + 16 + direction * (width / 2 + 16);

                velocity.y *= -1;
            }
            y = position.y;

            // Friction
            acceleration = Vec2.zero;
            velocity.Scale(0.99f);
        }
    }
}
