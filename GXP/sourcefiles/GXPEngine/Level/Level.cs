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
                    if (tiles[i] == 8)
                    {
                        player.position.y = anims.y - player.height / 2;
                        player.velocity.y = 0;
                        player.SetOnGound();
                    }
                    if (tiles[i] == 4)
                    {
                        player.position.x = anims.x + player.width / 2 + 32;
                        player.velocity.x = 0;
                    }
                    if (tiles[i] == 6)
                    {
                        player.position.x = anims.x - player.width / 2;
                        player.velocity.x = 0;
                    }
                }
                if (player.arm.projectile != null)
                {
                    if (anims.HitTest(player.arm.projectile))
                    {
                        if(tiles[i] == 8 || tiles[i] == 4 || tiles[i] == 6 || tiles[i] == 2)
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
                        if (tiles[i] == 8)
                        {
                            if(ball.position.y + ball.radius > anims.y)
                            {
                                ball.position.y = anims.y - ball.radius;
                                ball.velocity.y *= -0.5f;
                            }
                        }
                    }
                }
                if(player.arm.ball != null)
                {
                    if (player.arm.ball.HitTest(anims))
                    {
                        if (tiles[i] == 8 )
                        {
                            if (player.arm.ball.position.y + player.arm.ball.radius > anims.y)
                            {
                                player.arm.ball.position.y = anims.y - player.arm.ball.radius;
                                player.arm.ball.velocity.y *= -1;
                            }
                        }
                        if(tiles[i] == 6 )
                        {
                            if (player.arm.ball.position.x + player.arm.ball.radius > anims.x)
                            {
                                player.arm.ball.position.x = anims.x - player.arm.ball.radius;
                                player.arm.ball.velocity.x *= -1;
                            }
                        }
                        if (tiles[i] == 4)
                        {
                            if (player.arm.ball.position.x - player.arm.ball.radius < anims.x)
                            {
                                player.arm.ball.position.x = anims.x + player.arm.ball.radius;
                                player.arm.ball.velocity.x *= -1;
                            }
                        }
                        if (tiles[i] == 2)
                        {
                            if (player.arm.ball.position.y - player.arm.ball.radius < anims.y)
                            {
                                player.arm.ball.position.y = anims.y + player.arm.ball.radius;
                                player.arm.ball.velocity.y *= -1;
                            }
                        }
                    }
                }


            }
        }


    }
}
