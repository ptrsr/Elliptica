using System;
using GXPEngine;
using System.Drawing;
using System.Xml.Serialization;
using System.Xml;


public enum StateType
{
    Menu,
    Level_Select,
    First_Level,
    Second_Level,
    Third_Level,
    Forth_Level,
    Fifth_Level,
    Sixth_Level,
    Seventh_Level,
    Eight_Level,
    Ninth_Level,
    Tenth_Level,
    Eleventh_Level,
    Twelveth_Level,

}
public class MyGame : Game
{
    Menu _menu;
    [XmlIgnore]
    Level _level;
    LoadLevel _levelSelect;
    public StateType _stateType;
    Sound music;
    Cursor cursor;
    public MyGame () : base(1024, 768, false)
	{
        ShowMouse(false);
        stateType = StateType.Menu;
        music = new Sound("music.mp3", true, true);
        music.Play();

        cursor = new Cursor();
        game.AddChild(cursor);
    }

	void Update ()
	{
        Console.Clear();
        Console.WriteLine(currentFps);
	}



    public StateType stateType
    {
        get
        {
            return _stateType;
        }
        set
        {
            _stateType = value;
            switch (_stateType)
            {
                
                case StateType.Menu:
                    Clear();
                    _menu = new Menu(this);
                    AddChild(_menu);
                    cursor = new Cursor();
                    game.AddChild(cursor);
                    break;
                case StateType.Level_Select:
                    Clear();
                    _levelSelect = new LoadLevel(this);
                    AddChild(_levelSelect);
                    cursor = new Cursor();
                    game.AddChild(cursor);
                    break;
                case StateType.First_Level:
                    Clear();
                    _level = new Level("Level1.tmx", this);
                    AddChild(_level);
                    cursor = new Cursor();
                    game.AddChild(cursor);
                    break;
                case StateType.Second_Level:
                    Clear();
                    _level = new Level("Level2.tmx", this);
                    AddChild(_level);
                    cursor = new Cursor();
                    game.AddChild(cursor);
                    break;
                case StateType.Third_Level:
                    Clear();
                    _level = new Level("Level3.tmx", this);
                    AddChild(_level);
                    cursor = new Cursor();
                    game.AddChild(cursor);
                    break;
                case StateType.Forth_Level:
                    Clear();
                    _level = new Level("Level4.tmx", this);
                    AddChild(_level);
                    cursor = new Cursor();
                    game.AddChild(cursor);
                    break;
                case StateType.Fifth_Level:
                    Clear();
                    _level = new Level("Level5.tmx", this);
                    AddChild(_level);
                    cursor = new Cursor();
                    game.AddChild(cursor);
                    break;
                case StateType.Sixth_Level:
                    Clear();
                    _level = new Level("Level6.tmx", this);
                    AddChild(_level);
                    cursor = new Cursor();
                    game.AddChild(cursor);
                    break;
                case StateType.Seventh_Level:
                    Clear();
                    _level = new Level("Level7.tmx", this);
                    AddChild(_level);
                    cursor = new Cursor();
                    game.AddChild(cursor);
                    break;
                case StateType.Eight_Level:
                    Clear();
                    _level = new Level("Level8.tmx", this);
                    AddChild(_level);
                    cursor = new Cursor();
                    game.AddChild(cursor);
                    break;
                case StateType.Ninth_Level:
                    Clear();
                    _level = new Level("Level9.tmx", this);
                    AddChild(_level);
                    cursor = new Cursor();
                    game.AddChild(cursor);
                    break;
                case StateType.Tenth_Level:
                    Clear();
                    _level = new Level("Level10.tmx", this);
                    AddChild(_level);
                    cursor = new Cursor();
                    game.AddChild(cursor);
                    break;
                case StateType.Eleventh_Level:
                    Clear();
                    _level = new Level("Level11.tmx", this);
                    AddChild(_level);
                    cursor = new Cursor();
                    game.AddChild(cursor);
                    break;
                case StateType.Twelveth_Level:
                    Clear();
                    _level = new Level("Level12.tmx", this);
                    AddChild(_level);
                    cursor = new Cursor();
                    game.AddChild(cursor);
                    break;
            }
        }

    }

    private void Clear()
    {
        if(_menu != null)
        {
            _menu.Destroy();
            _menu = null;
        }
        if (_levelSelect != null)
        {
            _levelSelect.Destroy();
            _levelSelect = null;
        }
        if(_level != null)
        {
            _level.Destroy();
            _level = null;
        }
    }
   
    static void Main() {
		new MyGame().Start();
	}
}

