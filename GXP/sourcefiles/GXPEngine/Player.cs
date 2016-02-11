using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine 
{


    class Player : AnimSprite
    {


        private Vec2 _position;
        private Vec2 _velocity;
        private Vec2 _acceleration;
        private Vec2 _gravity = new Vec2(0, 1);
        private bool onGround = false;
        private float frame = 0.0f;
        private float firstframe = 0.0f;
        private float lastframe = 0.0f;
        private int _directionX = 1;
        public Arm arm;

        public Player(Vec2 pPosition = null) : base("player.png", 16 , 2)
        {
            SetOrigin(width / 2, height / 2);
            position = pPosition;
            velocity = Vec2.zero;
            acceleration = Vec2.zero;

            arm = new Arm(this);
            AddChild(arm);
            x = position.x;
            y = position.y;
        }


        void Update()
        {
            UpdateAnimation();
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
                if (onGround)
                {
                    acceleration.Add(new Vec2(0, -20));
                    Console.WriteLine(true);
                }
            }
            if (Input.GetKey(Key.D))
            {
                acceleration.Add(new Vec2(1, 0));
                firstframe = 0;
                lastframe = 15;
                _directionX = 1;
            }
            if (Input.GetMouseButtonDown(0))
            {
                arm.ShootingPortal();
                arm.ShootingBall();
            }
            else if (Input.GetKey(Key.A))
            {
                acceleration.Add(new Vec2(-1, 0));
                firstframe = 0;
                lastframe = 15;
                _directionX = -1;
            }

            else {
                firstframe = 16;
                lastframe = 22;
                onGround = false;
            }
            scaleX = _directionX;
        
        onGround = false;
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

       public void SetOnGound()
        {
            onGround = true;
        }

        public void PickUpBall()
        {
            arm.BallArm();
        }
        void UpdateAnimation()
        {
            frame = frame + 0.2f;
            if (frame >= lastframe + 1);
                frame = firstframe;
            if (frame < firstframe)
                frame = lastframe;
            SetFrame((int)frame);
        }
    }
}
