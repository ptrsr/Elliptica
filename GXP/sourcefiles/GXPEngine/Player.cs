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
        Ball ball;

        public Player(Vec2 pPosition = null) : base("player.png", 16 , 2)
        {
            SetOrigin(width / 2, height / 2);
            position = pPosition;
            velocity = Vec2.zero;
            acceleration = Vec2.zero;

            arm = new Arm(this);
            arm.x += 10;
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
            if (Input.mouseX <= x)
                _directionX = -1;
            if (Input.mouseX >= x)
                _directionX = 1;
            if (Input.GetKeyDown(Key.W))
            {
                if (onGround)
                {
                    acceleration.Add(new Vec2(0, -20));
                }
            }
            if (Input.GetKey(Key.D))
            {
                acceleration.Add(new Vec2(0.8f, 0));
            }
            if (Input.GetMouseButtonDown(0))
            {
                if(!arm.CheckHasBall())
                arm.ShootingPortal();
                else
                arm.ShootingBall();
            }
 
            else if (Input.GetKey(Key.A))
            {
                acceleration.Add(new Vec2(-0.8f, 0));
            }

            else {
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
            if (velocity.y == 0)
            {
                if (Mathf.Abs(velocity.x) > 1.5f)
                {
                    frame = frame + velocity.x / 15 * _directionX;

                    firstframe = 0;
                    lastframe = 15;
                }
                else
                {
                    frame += 0.1f;

                    firstframe = 16;
                    lastframe = 22;
                }
                if (frame < firstframe)
                {
                    frame = lastframe;
                }
                if (frame > lastframe)
                {
                    frame = firstframe;
                }
            }
            else if (velocity.y <= -1)
                frame = 23;
            else if (velocity.y > -1 && velocity.y < 5)
                frame = 24;
            else if (velocity.y > 5)
                frame = 25;


            SetFrame((int)frame);
        }
    }
}
