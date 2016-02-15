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
            GameObject collisionTile;

            // X Collision
            velocity.x += acceleration.x;
            position.x += velocity.x;
            x = position.x;

            collisionTile = TMXLevel.Return().CheckCollision(this);

            if (collisionTile != null && collisionTile is CollisionTile)
            {
                CollisionTile wall = (CollisionTile)collisionTile;
                switch (wall.type)
                {
                    case "white":
                        string side;
                        if (velocity.x < 0)
                            side = "left";
                        else
                            side = "right";

                        SpawnPortal(side, wall);
                        Destroy();
                        break;

                    case "gray":
                        direction = velocity.x > 0 ? -1 : 1;
                        position.x = collisionTile.x + 16 + direction * (width / 2 + 17);
                        velocity.x *= -1;
                        break;

                    case "black":
                        Destroy();
                        break;
                }
                        
            }
            x = position.x;

            // Y Collision
            acceleration.y += _gravity;
            velocity.y += acceleration.y;
            position.y += velocity.y;
            y = position.y;

            collisionTile = TMXLevel.Return().CheckCollision(this);

            if (collisionTile != null && collisionTile is CollisionTile)
            {
                CollisionTile wall = (CollisionTile)collisionTile;
                switch (wall.type)
                {
                    case "white":
                        string side;
                        if (velocity.y < 0)
                            side = "up";
                        else
                            side = "down";

                        SpawnPortal(side, wall);
                        Destroy();
                        break;

                    case "gray":
                        direction = velocity.y > 0 ? -1 : 1;
                        position.y = collisionTile.y + 16 + direction * (width / 2 + 16);
                        velocity.y *= -1;
                        break;

                    case "black":
                        Destroy();
                        break;
                }
            }
            y = position.y;

            // Friction
            acceleration = Vec2.zero;
            velocity.Scale(0.99f);
        }

        void SpawnPortal(string side, CollisionTile wall) {
            Portal portal = new Portal(side);
            TMXLevel.Return().AddChild(portal);

            switch(side) {
                case "up":
                    portal.SetXY(wall.x + 16, wall.y + 32);
                    portal.rotation = -90;
                    break;
                case "down":
                    portal.SetXY(wall.x + 16, wall.y);
                    portal.rotation = 90;
                    break;
                case "left":
                    portal.SetXY(wall.x + 32, y + 16);
                    break;
                case "right":
                    portal.SetXY(wall.x, y + 16);
                    portal.rotation = 180;
                    break;
            }
            
        }
    }
}
