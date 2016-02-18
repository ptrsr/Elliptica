using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Arm : AnimSprite
    {
        private Player _player;
        private Vec2 _armVector;
        public Projectile portalBall;
        public Ball ball;
        private float _frame;
        private bool shot = false;
        Sound throwing;
        Sound shooting;
        public bool hasBall = false;
        private bool reset;

        string _colour;

        private Charge charge = new Charge();

        public float _timer = 0;

        public Portal _purplePortal = null;
        public Portal _greenPortal = null;

        public Arm(Player player) : base("arm.png",5,1)
        {
            SetOrigin(0, height / 2);
            _player = player;
            SetFrame(0);

            throwing = new Sound("throw.wav");
            shooting = new Sound("shoot.wav");

            AddChild(charge);
            charge.SetXY(40, -18);
        }

        void Update()
        {
            Charge();

            _armVector = new Vec2(Input.mouseX - (_player.x + x), Input.mouseY - (_player.y + y)).Normalize().Multiply(this.height * 1f);
            RotateArm();
            CheckBall();
            if (shot)
            {
                UpdateAnimation();
            }
        }

        void RotateArm ()
        {
            float angleInRadians;
            if (_player.scaleX == 1)
            {
                angleInRadians = Mathf.Atan2(_armVector.y, _armVector.x);
                rotation = Vec2.Rad2Deg(angleInRadians);
            }
            if (_player.scaleX == -1)
            {
                angleInRadians = -Mathf.Atan2(_armVector.y, _armVector.x);
                rotation = Vec2.Rad2Deg(angleInRadians) + 180;
            }
        }

        public void Charge()
        {
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
                reset = true;

            charge.alpha = 0;
            if (reset == true && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
            {
                if (Input.GetMouseButton(0))
                    _colour = "Purple";
                else
                    _colour = "Green";

                charge.alpha = 1;
                _timer += 0.2f;

                charge.SetFrame((int)_timer);
            }


            if (Input.GetMouseButtonUp(0) && _timer > 0)
            {
                Shoot("Purple", _timer);
                _timer = 0;
            }
            else if (Input.GetMouseButtonUp(1) && _timer > 0)
            {
                Shoot("Green", _timer);
                _timer = 0;
            }
            else if (_timer > 8)
            {
                Shoot(_colour, _timer);
                _timer = 0;
                reset = false;
            }


        }

        public void Shoot(string color, float strength)
        {
            if (hasBall == true)
            {
                ball = new Ball(new Vec2(_player.x + _armVector.x + x, _player.y + _armVector.y + y));
                TMXLevel.Return().AddChild(ball);
                ball.velocity.SetXY(_armVector.Scale(0.8f));

                hasBall = false;
                SetFrame(0);

            }
            else {

                if (portalBall == null)
                {
                    portalBall = new PortalBall(new Vec2(_player.x + _armVector.x + x, _player.y + _armVector.y + y), color, this);
                    TMXLevel.Return().AddChild(portalBall);
                    portalBall.velocity.SetXY(_armVector.Scale(strength).Scale(0.2f));
                }
            }
        }
        void UpdateAnimation()
        {
            _frame = _frame + 0.5f;
            if (_frame >= frameCount - 1)
                _frame = 0;
            SetFrame((int)_frame);
        }

        public void CheckBall()
        {
            if (hasBall == true)
                SetFrame(4);
            else 
                SetFrame(0);
        }
    }
}
