using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    public abstract class Projectile : Sprite
    {
        public int radius;
        public Vec2 _position;
        public Vec2 _velocity;
        public Vec2 _acceleration;

        public float _gravity = 1;

        public Projectile(string filename, Vec2 spawnPos, int radius) : base(filename + ".png")
        {
            SetXY(spawnPos.x, spawnPos.y);

            velocity = Vec2.zero;
            acceleration = Vec2.zero;
            position = spawnPos;

            SetOrigin(width / 2, width / 2);

        }

        public virtual void Step()
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

        public Vec2 position
        {
            set
            {
                _position = value ?? Vec2.zero;
            }
            get
            {
                return _position;
            }
        }

        public Vec2 velocity
        {
            set
            {
                _velocity = value ?? Vec2.zero;
            }
            get
            {
                return _velocity;
            }
        }

        public Vec2 acceleration
        {
            set
            {
                _acceleration = value ?? Vec2.zero;
            }
            get
            {
                return _acceleration;
            }
        }
    }
}
