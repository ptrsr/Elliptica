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
            Wall wall;

            // X Collision
            velocity.x += acceleration.x;
            position.x += velocity.x;
            x = position.x;

            wall = TMXLevel.Return().CheckCollision(this);

            if (wall != null)
            {
                direction = velocity.x > 0 ? -1 : 1;

                position.x = wall.x + 16 + direction * (width / 2 + 17);
                velocity.x *= -1;
            }
            x = position.x;

            // Y Collision
            acceleration.y += _gravity;
            velocity.y += acceleration.y;
            position.y += velocity.y;
            y = position.y;

            wall = TMXLevel.Return().CheckCollision(this);

            if (wall != null)
            {
                direction = velocity.y > 0 ? -1 : 1;

                position.y = wall.y + 16 + direction * (width / 2 + 16);

                velocity.y *= -1;
            }
            y = position.y;

            // Friction
            acceleration = Vec2.zero;
            velocity.Scale(0.99f);
        }
    }
}
