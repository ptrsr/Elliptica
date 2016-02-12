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
        public Projectiles projectile;
        public Ball ball;
        private bool hasBall = false;

        public Arm(Player player) : base("arm.png",2,1)
        {
            SetOrigin(0, height / 2);
            _player = player;
            SetFrame(0);
        }

        void Update()
        {
            _armVector = new Vec2(Input.mouseX - _player.x, Input.mouseY - _player.y).Normalize().Multiply(this.height * 1.5f);
            RotateArm();
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

        public void ShootingPortal()
        {
            if(projectile == null)
            {
                projectile = new Projectiles();
                game.AddChild(projectile);
                projectile.x = _player.x + _armVector.x;
                projectile.y = _player.y + _armVector.y;
                projectile.rotation = Vec2.Rad2Deg(Mathf.Atan2(_armVector.y, _armVector.x));
            }
        }
        public void ShootingBall()
        {
            if (ball == null)
            {
                ball = new Ball();
                game.AddChild(ball);
                ball.position.x = _player.x;
                ball.position.y = _player.y;
                ball.velocity.SetXY(_armVector.Scale(0.8f));
                ball.rotation = this.rotation;
                hasBall = false;
                SetFrame(0);
            }
        }
        public void BallArm()
        {
            SetFrame(1);
            hasBall = true;
        }

        public bool CheckHasBall()
        {
            return hasBall;
        }

        public Ball GetBall()
        {
            return ball;
        }
        


    }
}
