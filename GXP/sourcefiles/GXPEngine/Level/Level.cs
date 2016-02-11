using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace GXPEngine
{
    class Level : TMXLevel
    {
        private Player player;
        Ball ball;
        private float playerOldX;
        private float playerOldY;
        private List<Ball> ballz = new List<Ball>();
        public Level(string filename) : base()
        {
            InterpretLayer(filename);
            InterpretObjectGroup(filename);


            player = new Player();
            player.position.x = 400;
            player.position.y = 600 - 32;
            AddChild(player);


            ballz.Add(ball = new Ball());
            ball.position.x = 700;
            ball.position.y = 400;
            AddChild(ball);

        }

        void Update()
        {

            CheckCollision();
            CheckBallCollision();
            playerOldX = player.position.x;
            playerOldY = player.position.y;
            
        }
        
        private void CheckBallCollision()
        {
            if (ball != null)
            {

                if (!player.arm.CheckHasBall())
                {
                    if ((ball.position.x - player.position.x) < 3)
                    {
                        ball.Destroy();
                        ball = null;
                        player.PickUpBall();
                        Console.WriteLine(true);
                    }
                }
            }
        }

        private void CheckCollision()
        {
            for (int i = background.Count - 1; i >= 0; i--)
            {
                AnimSprite anims = background[i];
                if (anims.HitTest(player))
                {
                    if (tiles[i] == 2)
                    {
                        player.position.y = playerOldY;
                        player.velocity.y = 0;
                        player.SetOnGound();
                    }
                    if (tiles[i] == 12)
                    {
                        player.position.x = playerOldX;
                        player.velocity.x = 0;
                    }
                    if (tiles[i] == 10)
                    {
                        player.position.x = playerOldX;
                        player.velocity.x = 0;
                    }
                }
                if (player.arm.projectile != null)
                {
                    if (anims.HitTest(player.arm.projectile))
                    {
                        if(tiles[i] == 12)
                        {
                            player.arm.projectile.Destroy();
                            player.arm.projectile = null;
                        }
                    }
                }
                if (ball != null)
                {
                    if (ball.HitTest(anims))
                    {
                        if (tiles[i] == 2)
                            if (ball.position.y + ball.radius > anims.y)
                            {

                                ball.position.y = anims.y - ball.radius;
                                ball.velocity.y *= -0.50f;
                            }

                    }
                }
            }
        }


    }
}
