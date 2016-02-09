using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine 
{


    class Player : Sprite
    {


        private Vec2 _position;
        private Vec2 _velocity;
        private Vec2 _acceleration;
        private Vec2 _gravity = new Vec2(0, 1);
        private bool checkGround = false;
        public Player(Vec2 pPosition = null) : base("colors.png")
        {
            SetOrigin(width / 2, height / 2);
            position = pPosition;
            velocity = Vec2.zero;
            acceleration = Vec2.zero;

            x = position.x;
            y = position.y;
        }


        void Update()
        {
            ApplyBorders();
            Movements();
            Step();
            acceleration = Vec2.zero;
            velocity.x *= 0.90f;
            velocity.y *= 0.90f;
        }

        private void Movements()
        {
            if (Input.GetKeyDown(Key.W))
            {
                if(checkGround)
                acceleration.Add(new Vec2(0,-20));
            }
            if (Input.GetKey(Key.D))
            {
                acceleration.Add(new Vec2(1, 0));
            }
            if (Input.GetKey(Key.A))
            {
                acceleration.Add(new Vec2(-1,0));
            }
        }


        private void ApplyBorders()// applying borders to game
        {
            checkGround = false;
            if (position.x < 64) position.x = 64;
            if (position.y < 64) position.y = 64;
            if (position.x > 1024 - 64) position.x = 1024 - 64;
            if (position.y > 768 - 64) { position.y = 768 - 64; checkGround = true; }
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
            _acceleration.Add(_gravity);
            _velocity.Add(_acceleration);
            _position.Add(_velocity);

            x = _position.x;
            y = _position.y;
        }
    }
}
