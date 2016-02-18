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

        public Portal _purplePortal = null;
        public Portal _greenPortal = null;

        public Arm(Player player) : base("arm.png",5,1)
        {
            SetOrigin(0, height / 2);
            _player = player;
            SetFrame(0);
            throwing = new Sound("throw.wav");
            shooting = new Sound("shoot.wav");
        }

        void Update()
        {
            _armVector = new Vec2(Input.mouseX - (_player.x + x), Input.mouseY - (_player.y + y)).Normalize().Multiply(this.height * 1.5f);
            RotateArm();
            CheckBall();
            if (shot)
            {
                UpdateAnimation();
            }
            if(Input.GetKeyDown(Key.R))
            {
                if (_greenPortal != null)
                {
                    _greenPortal.Destroy();
                    _greenPortal = null;
                }

                if (_purplePortal != null)
                {
                    _purplePortal.Destroy();
                    _purplePortal = null;
                }
            }

            if (_greenPortal == null && _purplePortal == null)
            {
                Cursor.SetCursorFrame(0);
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

        public void Shoot(string color)
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
                    portalBall.velocity.SetXY(_armVector.Scale(0.8f));
                    shot = true;
                }
            }
        }
        void UpdateAnimation()
        {
            _frame = _frame + 0.3f;
            if (_frame >= frameCount - 1)
            {
                _frame = 0;
                shot = false;
            }
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
