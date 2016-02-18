using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine 
{


    public class Player : AnimSprite
    {


        public Vec2 _position;
        public Vec2 _velocity;
        public Vec2 _acceleration;

        private float _gravity = 1;

        private bool onGround = false;
        public bool dead = false;
        private float frame = 0.0f;
        private float firstframe = 0.0f;
        private float lastframe = 0.0f;

        protected string state = null;

        private bool isLevelCompleted = false;

        public Arm arm;

        public Player() : base("player.png", 16 , 2)
        {
            SetOrigin(width / 2, height);

            position = Vec2.zero;
            velocity = Vec2.zero;
            acceleration = Vec2.zero;

            arm = new Arm(this);
            arm.SetXY(10, -60);

            AddChild(arm);

            x = position.x;
            y = position.y;
        }


        void Update()
        {
            Movements();
            UpdateAnimation();
            Step();
        }

        private void Movements()
        {
            if (Input.GetKey(Key.W) && onGround)
            {
                acceleration.Add(new Vec2(0, -40));
            }

            if (Input.GetKey(Key.D))
            {
                acceleration.Add(new Vec2(0.8f, 0));
            }

            if (Input.GetMouseButtonDown(0))
            {
                arm.Shoot();
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
            GameObject tiledObject;

            // X Collision
            velocity.x += acceleration.x;
            position.x += velocity.x;
            x = position.x;

            tiledObject = TMXLevel.Return().CheckCollision(this);

            if (tiledObject != null)
            {   if (tiledObject is Door)
                {
                    Door door = (Door)tiledObject;
                    if (door.IsDoorOpen())
                    {
                        isLevelCompleted = true;
                    }
                }
                direction = velocity.x > 0 ? -1 : 1;

                position.x = tiledObject.x + 16 + direction * (width / 2 + 17);
                velocity.x = 0;
            }
            x = position.x;

            // Y Collision
            acceleration.y += _gravity;
            velocity.y += acceleration.y;
            position.y += velocity.y;
            y = position.y;

            tiledObject = TMXLevel.Return().CheckCollision(this);

            if (tiledObject != null)
            {
                direction = velocity.y < 0 ? -1 : 1;

                if (direction == 1)
                {
                    position.y = tiledObject.y;
                    onGround = true;
                }

                if (direction == -1)
                    position.y = tiledObject.y + height + 32;

                velocity.y = 0;
            }
            y = position.y;

            // Friction
            acceleration = Vec2.zero;
            velocity.x *= 0.90f;
            velocity.y *= 0.99f;
        }

        public void OnCollision(GameObject other)
        {
            if (other is Ball)
            {
                Ball ball = (Ball)other;
                if (ball.timer > 10)
                {
                    arm.hasBall = true;
                    other.Destroy();
                }
            }
        }


        public void Die()
        {
            dead = true;

            if (dead == true)
                Destroy();
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

        public bool CheckState()
        {
            return isLevelCompleted;
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
