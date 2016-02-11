using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Arm : AnimSprite
    {
        private Player _player;
        private Vec2 _armVector;
        public Projectiles projectile;
        private Ball ball;
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
            float angleInRadians = Mathf.Atan2(_armVector.y, _armVector.x);
            rotation = Vec2.Rad2Deg(angleInRadians);
        }

        public void ShootingPortal()
        {
            if(projectile == null && !hasBall)
            {
                projectile = new Projectiles();
                game.AddChild(projectile);
                projectile.x = _player.x + _armVector.x;
                projectile.y = _player.y + _armVector.y;
                projectile.rotation = this.rotation;
            }
        }
        public void ShootingBall()
        {
            if (ball == null && hasBall)
            {
                ball = new Ball();
                game.AddChild(ball);
                ball.position.x = _player.x;
                ball.position.y = _player.y;
                ball.acceleration.x = _armVector.x * 0.1f;
                ball.rotation = this.rotation;
                Console.WriteLine("ballx : " + ball.position.x + "  bally : " + ball.position.y);
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


    }
}
