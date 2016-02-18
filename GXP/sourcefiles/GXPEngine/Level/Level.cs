using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace GXPEngine
{
    public class Level : TMXLevel
    {
        MyGame _game;
        [XmlIgnore]
        EndScreen endScreen;
        private int timer;
        private int delay = 300;
        Sound backgroundsound;
        SoundChannel backGroundSoundOn;
        private bool isGameOver = false;
        public Level(string filename,MyGame myGame) : base()
        {
            _game = myGame;
            InterpretLayer(filename);
            InterpretObjectGroup(filename);
            endScreen = new EndScreen(bigTimer, _game.stateType);
            backgroundsound = new Sound("humming.wav", true, true);

            backGroundSoundOn = backgroundsound.Play();

            if (_game.stateType == StateType.Menu)
            {
                turret.DestroyLine();
            }
        }

        public Level()
        {

        }

        void Update()
        {
            if (door != null)
            {
                if (trigger == null)
                {
                    door.ActivateDoor();
                    if (door.open == false)
                    {
                        if (door.DistanceTo(player.position.x, player.position.y) < 200)
                        {
                            door.UpdateAnimation(door.DistanceTo(player.position.x, player.position.y));
                        }
                    }
                    else door.SetFrame(door.frameCount);
                }
                if (trigger != null)
                    if (trigger.triggerAnim.currentFrame == 1)
                    {
                        door.ActivateDoor();
                        if (door.DistanceTo(player.position.x, player.position.y) < 200)
                        {
                            door.UpdateAnimation(door.DistanceTo(player.position.x, player.position.y));
                        }
                    }
            }
            if (player != null)
                if (player.CheckState())
                {
                    if (_game.stateType == StateType.Menu)
                    {
                        game.Destroy();
                    }
                        bigTimer.StopTimer();
                        AddChild(endScreen);
                        if (timer >= delay || Input.GetKeyDown(Key.ENTER) || Input.GetKeyDown(Key.W) || Input.GetKeyDown(Key.S) || Input.GetKeyDown(Key.D) || Input.GetKeyDown(Key.A))
                        {
                            _game.stateType = _game.stateType + 1;
                        }
                        timer++;
                }
            if (player != null)
                if (player.CheckDeath())
                {
                    if (!isGameOver)
                    {
                        GameOver gameOver = new GameOver(_game);
                        AddChild(gameOver);
                        isGameOver = true;
                    }
                }
            if (Input.GetKey(Key.BACKSPACE))
            {
                if(player != null)
                player.Die();
            }
        }
         
        



    }
}
