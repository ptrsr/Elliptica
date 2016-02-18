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
        private Portal portalIn = null;
        private Portal portalOut = null;
        private bool _inPortal = false;
        private float _horSlice = 0;
        private float _verSlice = 0;

        private float distance;

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
            CheckPortal();
            Movements();
            UpdateAnimation();
            Step();
        }

        private void Movements()
        {
            if (Input.GetKey(Key.W) && onGround)
            {
                acceleration.Add(new Vec2(0, -15));
            }

            if (Input.GetKey(Key.D))
            {
                acceleration.Add(new Vec2(0.8f, 0));
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
            // X Collision
            velocity.x += acceleration.x;
            position.x += velocity.x;
            x = position.x;

            tiledObject = TMXLevel.Return().CheckCollision(this);

            if (tiledObject != null)
            {
                if (tiledObject is Door)
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
            velocity.y = Utils.Clamp(velocity.y + acceleration.y,-30,30);
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
            if (position.y > game.height || position.y < 0 || position.x < 0 || position.x > game.width)
                Die();
        
            // Friction
            acceleration = Vec2.zero;

            if (onGround)
            {
                velocity.x *= 0.90f;
                velocity.y *= 0.99f;
            }
            else
            {
                velocity.x *= 0.95f;
                velocity.y *= 0.995f;
            }
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
            if (other is Trigger)
            {
                Trigger trigger = (Trigger)other;
                int frame = trigger.GetFrame();
                if (frame == 2)
                {
                    arm.hasBall = true;
                    trigger.triggerAnim.SetFrame(0);
                    Turret turret = TMXLevel.Return().GetTurret();
                    turret.DestroyLine();
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
        public void HorSlice(float slice)
        {
            _horSlice = slice;
            SetHorSlice(slice);
            setUVs();
            SetOrigin(width / 2, height);
        }
        public void VerSLice(float slice)
        {
            _verSlice = slice;
            SetVerSlice(slice);
            setUVs();
            SetOrigin(width / 2, height);
        }

        private void CheckPortal()
        {
            Portal green = arm._greenPortal;
            Portal purple = arm._purplePortal;

            if (green != null && purple != null)
            {
                if (DistanceTo(green) > DistanceTo(purple))
                {
                    portalIn = purple;
                    portalOut = green;
                }
                else {
                    portalIn = green;
                    portalOut = purple;
                }

                distance = position.Clone().Substract(new Vec2(0, height / 2)).Substract(portalIn._Pos).Lenght();
                Console.WriteLine(distance);

                if (distance < 45)
                {
                    Vec2 playerPos = position;
                    string side = portalIn._side;
                    if (side == "up")
                        playerPos.Substract(new Vec2(0, height - 60));

                    Vec2 Dif = playerPos.Clone().Substract(portalIn._botPos.Clone());
                    Vec2 lineNormal = portalIn._botPos.Clone().Substract(portalIn._topPos.Clone()).Normal();
                    distance = -Dif.Dot(lineNormal);

                    if (distance < 5 && HitTest(portalIn)) 
                        Teleport(portalIn, portalOut);
                }
            }
        }

        private void Teleport(Portal In, Portal Out)
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
                    position = Out._Pos.Clone().Add(new Vec2(0, height / 2 + 30));
                    velocity.y -= velocity.y;
                }
                else if (sideOut == down)
                {
                    position = Out._Pos.Clone().Substract(new Vec2(0, height / 2));
                }
                else if (sideOut == left)
                {
                    position = Out._topPos.Clone().Add(new Vec2(width / 2, 0));
                    velocity.x = -velocity.y;
                    velocity.y = 0;
                }
                else if (sideOut == right)
                {
                    position = Out._topPos.Clone().Substract(new Vec2(width / 2, 0));
                    velocity.x = velocity.y;
                    velocity.y = 0;
                }
            }
            else if (sideIn == down)
            {
                if (sideOut == up)
                {
                    position = Out._Pos.Clone().Add(new Vec2(0, height / 2 + 40));
                }
                else if (sideOut == down)
                {
                    position = Out._Pos.Clone();
                    velocity.y = -velocity.y;
                }
                else if (sideOut == left)
                {
                    position = Out._topPos.Clone().Add(new Vec2(width / 2, 0));
                    velocity.x = velocity.y;
                    velocity.y = 0;
                }
                else if (sideOut == right)
                {
                    position = Out._topPos.Clone();
                    velocity.x = -velocity.y;
                    velocity.y = 0;
                }
            }
            else if (sideIn == left)
            {
                if (sideOut == up)
                {
                    position = Out._Pos.Clone().Add(new Vec2(0, height / 2 + 30));
                    velocity.y = -velocity.x;
                    velocity.x = 0;
                }
                else if (sideOut == down)
                {
                    position = Out._Pos.Clone().Substract(new Vec2(0, height / 2));
                    velocity.y = velocity.x;
                    velocity.x = 0;
                }
                else if (sideOut == left)
                {
                    position = Out._topPos.Clone().Add(new Vec2(width / 2 + 10));
                    velocity.x = -velocity.x;
                }
                else if (sideOut == right)
                {
                    position = Out._topPos.Clone().Substract(new Vec2(width / 2));
                }
            }
            else if (sideIn == right)
            {
                if (sideOut == up)
                {
                    position = Out._Pos.Clone().Add(new Vec2(0, height / 2 + 30));
                    velocity.y = velocity.x;
                    velocity.x = 0;
                }
                else if (sideOut == down)
                {
                    position = Out._Pos.Clone().Substract(new Vec2(0, height / 2));
                    velocity.y = -velocity.x;
                    velocity.x = 0;
                }
                else if (sideOut == left)
                {
                    position = Out._topPos.Clone().Add(new Vec2(width / 2, 0));
                }
                else if (sideOut == right)
                {
                    position = Out._topPos.Clone().Substract(new Vec2(width / 2, 0));
                    velocity.x = -velocity.x;
                }
            }


        }

        public bool CheckState()
        {
            return isLevelCompleted;
        }
        public bool CheckDeath()
        {
            return dead;
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
