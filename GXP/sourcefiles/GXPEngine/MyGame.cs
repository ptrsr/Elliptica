using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{
    //Sound backGroundSound;
	public MyGame () : base(1024, 768, false)
	{
        //backGroundSound = new Sound("background.wav");
        Level level = new Level("level1.tmx");
        AddChild(level);
	}

	void Update ()
	{
	}

	static void Main() {
		new MyGame().Start();
	}
}

