using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{	

	public MyGame () : base(800, 600, false)
	{
        Level level = new Level("untitled.tmx");
        AddChild(level);
	}

	void Update ()
	{
		
	}

	static void Main() {
		new MyGame().Start();
	}
}

