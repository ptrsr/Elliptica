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
        public Projectile PortalBall;
        public Ball ball;
        Sound throwing;
        Sound shooting;
        public bool hasBall = false;

        public Arm(Player player) : base("arm.png",2,1)
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

        public void Shoot()
        {
            if (hasBall == true)
            {
                throwing.Play();
                ball = new Ball(new Vec2(_player.x + _armVector.x + x, _player.y + _armVector.y + y));
                TMXLevel.Return().AddChild(ball);
                ball.velocity.SetXY(_armVector.Scale(0.8f));

                hasBall = false;
                SetFrame(0);

            } else if (PortalBall == null) {

                PortalBall = new PortalBall(new Vec2(_player.x + _armVector.x + x, _player.y + _armVector.y + y));
                TMXLevel.Return().AddChild(PortalBall);
                PortalBall.velocity.SetXY(_armVector.Scale(0.8f));
                shooting.Play();
            }
        }

        public void CheckBall()
        {
            if (hasBall == true)
                SetFrame(1);
            else 
                SetFrame(0);
        }
    }
}
