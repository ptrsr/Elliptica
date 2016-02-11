using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{	

	public MyGame () : base(1024, 768, false)
	{
        Level level = new Level("level1.tmx");
        AddChild(level);
	}

	void Update ()
	{
        Console.WriteLine(currentFps);
	}

	static void Main() {
		new MyGame().Start();
	}
}

