using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{	

	public MyGame () : base(1024, 768, false)
	{
        Level level = new Level("level1.tmx");
        AddChild(level);
        Player player = new Player();
        player.position.x = 400;
        player.position.y = 600 - 32;
        AddChild(player);
	}

	void Update ()
	{
		
	}

	static void Main() {
		new MyGame().Start();
	}
}

