using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{
    Menu _menu;
    Level _level;
    string _state = "";

	public MyGame () : base(1024, 768, false)
	{
        SetState("menu");
	}

	void Update ()
	{
	}

    public void SetState(string state)
    {
        if (state == _state)
            return;
        stopState();
        _state = state;
        startState();
    }

    public string GetState()
    {
        return _state;
    }

    void startState()
    {
        switch (_state)
        {
            case "menu":
                _menu = new Menu(this);
                AddChild(_menu);
                break;
            case "level1":
                _level = new Level("level2.tmx");
                AddChild(_level);
                break;
        }
    }

    void stopState()
    {
        switch (_state)
        {
            case "menu":
                _menu.Destroy();
                _menu = null;
                break;
            case "level1":
                _level.Destroy();
                _level = null;
                break;
        }
    }

    static void Main() {
		new MyGame().Start();
	}
}

