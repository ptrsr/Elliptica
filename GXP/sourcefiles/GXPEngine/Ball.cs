using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    public class Ball : Sprite
    {
        public readonly int radius;
        private Vec2 _position;
        private Vec2 _velocity;
        private Vec2 _acceleration;
        private Vec2 _gravity = new Vec2(0,1);
        

        public Ball(Vec2 pPosition = null) : base("Solidball.png")
        {
            radius = 8;
            SetOrigin(radius * 2, radius * 2);
            position = pPosition;
            velocity = Vec2.zero;
            acceleration = Vec2.zero;
            SetOrigin(radius, radius);
            x = position.x;
            y = position.y;

            _acceleration.Add(_gravity);
        }
        void Update()
        {
            Step();
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


        public void Step()
        {
            _velocity.Add(_acceleration);
            _position.Add(_velocity);

            x = _position.x;
            y = _position.y;
        }
    }
}
