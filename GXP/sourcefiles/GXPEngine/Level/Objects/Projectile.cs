using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    public abstract class Projectile : AnimationSprite
    {
        public int radius;
        public Vec2 _position;
        public Vec2 _velocity;
        public Vec2 _acceleration;


        private float frame = 0.0f;
        private float firstframe = 0.0f;
        private float lastframe = 0.0f;

        public float _gravity = 1;

        public Projectile(string filename, Vec2 spawnPos, int radius, int cols = 1, int rows = 1) : base(filename + ".png", cols, rows)
        {
            SetXY(spawnPos.x, spawnPos.y);

            velocity = Vec2.zero;
            acceleration = Vec2.zero;
            position = spawnPos;

            SetOrigin(width / 2, width / 2);


        }

        void Update()
        {
        }

        public virtual void Step()
        {
            int direction;
            GameObject tileObject;

            // X Collision
            velocity.x += acceleration.x;
            position.x += velocity.x;
            x = position.x;

            tileObject = TMXLevel.Return().CheckCollision(this);

            if (tileObject != null)
            {
                direction = velocity.x > 0 ? -1 : 1;

                position.x = tileObject.x + 16 + direction * (width / 2 + 17);
                velocity.x *= -1;
            }
            x = position.x;

            // Y Collision
            acceleration.y += _gravity;
            velocity.y += acceleration.y;
            position.y += velocity.y;
            y = position.y;

            tileObject = TMXLevel.Return().CheckCollision(this);

            if (tileObject != null)
            {
                direction = velocity.y > 0 ? -1 : 1;

                position.y = tileObject.y + 16 + direction * (width / 2 + 16);

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
