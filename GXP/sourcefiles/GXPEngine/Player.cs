using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine 
{


    public class Player : AnimSprite
    {


        private Vec2 _position;
        private Vec2 _velocity;
        private Vec2 _acceleration;
        private TMXLevel _lvl;

        private float _gravity = 1;

        private bool onGround = false;
        private float frame = 0.0f;
        private float firstframe = 0.0f;
        private float lastframe = 0.0f;

        protected string state = null;

        public Arm arm;

        public Player(TMXLevel lvl, Vec2 pPosition) : base("player.png", 16 , 2)
        {
            _lvl = lvl;

            SetOrigin(width / 2, height);
            position = pPosition;
            velocity = Vec2.zero;
            acceleration = Vec2.zero;

            arm = new Arm(this);
            arm.x += 10;
            arm.y -= 60;
            AddChild(arm);

            x = position.x;
            y = position.y;
        }


        void Update()
        {
            Movements();
            UpdateAnimation();
            Step();

            acceleration = Vec2.zero;
            velocity.x *= 0.90f;
            velocity.y *= 0.90f;
        }

        private void Movements()
        {
            if (Input.GetKey(Key.W) && onGround)
            {
                acceleration.Add(new Vec2(0, -20));
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
            scaleX = position.x > Input.mouseX ? -1 : 1;
        
        onGround = false;
        }

        public void Step()
        {
            int direction;
            Wall wall;

            // X Collision
            velocity.x += acceleration.x;
            position.x += velocity.x;
            x = position.x;

            wall = _lvl.CheckCollision();

            if (wall != null)
            {
                direction = velocity.x > 0 ? -1 : 1;

                position.x = wall.x + 16 + direction * (width / 2 + 17);
                velocity.x = 0;
            }
            x = position.x;

            // Y Collision
            acceleration.y += _gravity;
            velocity.y += acceleration.y;
            position.y += velocity.y;
            y = position.y;

            wall = _lvl.CheckCollision();

            if (wall != null)
            {
                direction = velocity.y < 0 ? -1 : 1;

                if (direction == 1)
                    position.y = wall.y;

                if (direction == -1)
                    position.y = wall.y + height + 32;

                velocity.y = 0;
                onGround = true;
            }
            y = position.y;
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
                    frame += (velocity.x / 15) * scaleX;

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
