using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{	

	public MyGame () : base(1024, 768, false)
	{
        Level level = new Level("level2.tmx");
        AddChild(level);
	}

	void Update ()
	{
	}

	static void Main() {
		new MyGame().Start();
	}
}

