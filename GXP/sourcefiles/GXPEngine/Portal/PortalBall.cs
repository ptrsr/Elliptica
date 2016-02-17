using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class PortalBall : Projectile
    {
        private string _color;
        private float _frame;
        Arm _arm;

        public PortalBall(Vec2 spawnPos, string color, Arm arm) : base("Portal Ball " + color, spawnPos, 10, 4)
        {
            _arm = arm;
            _color = color;
        }

        void Update()
        {
            Step();
            UpdateAnimation();
        }

        public override void Step()
        {
            bool spawned = false;
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
                        spawned = true;
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

            if (spawned == false && collisionTile != null && collisionTile is CollisionTile)
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
                        _arm.portalBall = null;
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
            _arm.portalBall = null;
            Portal portal = null;

            switch (side)
            {
                case "up":
                    portal = new Portal(side, _color, wall.x + 16, wall.y + 64);
                    break;
                case "down":
                    portal = new Portal(side, _color, wall.x + 16, wall.y - 32);
                    break;
                case "left":
                    portal = new Portal(side, _color, wall.x + 64, y + 16);
                    break;
                case "right":
                    portal = new Portal(side, _color, wall.x - 32, y + 16);
                    break;
            }

            
            TMXLevel.Return().AddChild(portal);

            if (_color == "Green")
            {
                if (_arm._greenPortal != null)
                    _arm._greenPortal.Remove();

                _arm._greenPortal = portal;
            }

            if (_color == "Purple")
            {
                if  (_arm._purplePortal != null)
                    _arm._purplePortal.Remove();
                _arm._purplePortal = portal;
            }
        }
        void UpdateAnimation()
        {
            _frame += 0.4f;

            if (_frame > frameCount)
            {
                _frame = 0;
            }
            SetFrame((int)_frame);
        }
    }
}
