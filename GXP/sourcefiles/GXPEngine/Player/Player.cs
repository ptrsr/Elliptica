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


        public Arm arm;

        private Portal portalIn = null;
        private Portal portalOut = null;

        private float _gravity = 1;

        private float _horSlice = 0;
        private float _verSlice = 0;

        private bool onGround = false;

        private float frame = 0;

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
                acceleration.Add(new Vec2(0, -30));
            }

            if (Input.GetKey(Key.D))
            {
                acceleration.Add(new Vec2(0.8f, 0));
            }

            if (Input.GetMouseButtonDown(0))
            {
                arm.Shoot("Purple");
            }

            if (Input.GetMouseButtonDown(1))
            {
                arm.Shoot("Green");
            }

            else if (Input.GetKey(Key.A))
            {
                acceleration.Add(new Vec2(-0.8f, 0));
            }

            else {
                onGround = false;
            }
            float scale = 1 - Mathf.Abs(_horSlice);
            scaleX = position.x > Input.mouseX ? -scale : scale;
            scaleY = 1 - Mathf.Abs(_verSlice);
        
        onGround = false;
        }

        public void Step()
        {
            int direction;
            GameObject tiledObject;

            if (CheckInPortal() == false) 
            {

            // X Collision
            velocity.x += acceleration.x;
            position.x += velocity.x;
            x = position.x;

            tiledObject = TMXLevel.Return().CheckCollision(this);

            
                if (tiledObject != null)
                {
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
            }

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

        void UpdateAnimation()
        {
            float firstframe = 0.0f;
            float lastframe = 0.0f;

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

        public bool CheckInPortal()
        {
            Portal green = arm._greenPortal;
            Portal purple = arm._purplePortal;
            portalIn = null;
            portalOut = null;

            float distance = 1000;

            if (green != null && purple != null)
            {
                if (DistanceTo(green) > DistanceTo(purple))
                {
                    portalIn = purple;
                    portalOut = green;
                }
                else
                {
                    portalIn = green;
                    portalOut = purple;
                }

                distance = DistanceTo(portalIn);
            }
            else
            {
                portalIn = null;
                portalOut = null;

                return false;
            }

            if (distance < 64)
            {
                Vec2 playerPos = position.Clone();

                if (portalIn._side == "up") { playerPos.Substract(new Vec2(0, height / 4 + 40)); }
                if (portalIn._side == "down") { playerPos.Substract(new Vec2(0, height / 4)); }


                Vec2 Dif = playerPos.Substract(portalIn._botPos.Clone());
                Vec2 lineNormal = portalIn._botPos.Clone().Substract(portalIn._topPos.Clone()).Normal();
                distance = -Dif.Dot(lineNormal);

                velocity.Add(acceleration);
                position.Add(velocity);

                if (portalIn._side == "up" || portalIn._side == "down")
                {
                    position.y += _gravity;
                    x = Utils.Clamp(position.x, portalIn._topPos.x, portalIn._botPos.x);
                    y = position.y;

                }
                else
                {
                    x = position.x;
                    y = Utils.Clamp(position.y, portalIn._botPos.y, portalIn._topPos.y);
                }


                if (distance < 0)
                {
                    Teleport(portalIn, portalOut);
                }
                return true;
            }
            return false;
        }

        void Teleport(Portal In, Portal Out)
        {
            string sideIn = In._side;
            string sideOut = Out._side;

            string up = "up";
            string down = "down";
            string left = "left";
            string right = "right";

            if (sideIn == up)
            {
                if (sideOut == up)
                {
                    position = Out._Pos.Add( new Vec2(0,height/2));
                    velocity.y -= velocity.y;
                }
                else if (sideOut == down)
                {
                    position = Out._Pos.Substract(new Vec2(0,height/2));
                }
                else if (sideOut == left)
                {
                    position = Out._botPos;
                    velocity.x = velocity.y;
                    velocity.y = 0;
                }
                else if (sideOut == right)
                {
                    position = Out._botPos;
                    velocity.x = -velocity.y;
                    velocity.y = 0;
                }
            }
            else if (sideIn == down)
            {
                if (sideOut == up)
                {
                    position = Out._Pos.Add(new Vec2(0, height / 2));
                }
                else if (sideOut == down)
                {
                    position = Out._Pos.Substract(new Vec2(0, height / 2));
                    velocity.y -= velocity.y;
                }
                else if (sideOut == left)
                {
                    position = Out._botPos;
                    velocity.x = -velocity.y;
                    velocity.y = 0;
                }
                else if (sideOut == right)
                {
                    position = Out._botPos;
                    velocity.x = velocity.y;
                    velocity.y = 0;
                }
            }
            else if (sideIn == left)
            {
                if (sideOut == up)
                {
                    position = Out._Pos.Add(new Vec2(0, height / 2));
                    velocity.y = -velocity.x;
                    velocity.x = 0;
                }
                else if (sideOut == down)
                {
                    position = Out._Pos.Substract(new Vec2(0, height / 2));
                    velocity.y = velocity.x;
                    velocity.x = 0;
                }
                else if (sideOut == left)
                {
                    position = Out._botPos;
                    velocity.x = -velocity.x;
                }
                else if (sideOut == right)
                {
                    position = Out._botPos;
                }
            }
            else if (sideIn == right)
            {
                if (sideOut == up)
                {
                    position = Out._Pos.Add(new Vec2(0, height / 2));
                    velocity.y = velocity.x;
                    velocity.x = 0;
                }
                else if (sideOut == down)
                {
                    position = Out._Pos.Substract(new Vec2(0, height / 2));
                    velocity.y = -velocity.x;
                    velocity.x = 0;
                }
                else if (sideOut == left)
                {
                    position = Out._botPos;
                    velocity.x = -velocity.x;
                }
                else if (sideOut == right)
                {
                    position = Out._botPos;
                }
            }
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
