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
        private float playerOldX;
        private float playerOldY;
        public Level(string filename) : base()
        {
            InterpretLayer(filename);
            InterpretObjectGroup(filename);


            player = new Player();
            player.position.x = 400;
            player.position.y = 600 - 32;
            AddChild(player);

        }

        void Update()
        {

            CheckCollision();
            playerOldX = player.position.x;
            playerOldY = player.position.y;
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
                            Console.WriteLine("CREATING PORTALSSSs");
                        }
                    }
                }
            }
        }


    }
}
