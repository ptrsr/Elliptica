using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class Buttons : AnimSprite
    {
        public float _frame;

        private bool _mouseDown = false;
        private bool _animated = false;
        private bool printed = false;
        private string _text;
        private Canvas _canvas;

        public delegate void ClickEvent(Buttons target);
        public event ClickEvent OnClick;

        private bool _mouseOn;

        public Buttons(string filename,int frame, int rows, int cols,string text = "" , bool animated = false,bool mouseOn= false) : base(filename, rows, cols)
        {
            SetOrigin(width / 2, height / 2);
            _frame = frame;
            _animated = animated;
            _text = text;
            SetFrame(frame);

            _mouseOn = mouseOn;

            _canvas = new Canvas(200, 200);
            AddChild(_canvas);
            _canvas.SetXY(-50, -60);

        }

        void Update()
        {
            if (OnClick == null)
                return;

            if (HitTestPoint(Input.mouseX, Input.mouseY))
            {
                if (_animated)
                {
                    UpdateAnimation();
                    _mouseOn = true;

                    if (_frame > 5)
                    {
                        Font font = new Font("Tubular Hollow", 50);
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        _canvas.graphics.DrawString(_text, font, Brushes.Black, 50, 50, sf);
                        printed = true;
                    }

                }
                else {

                    SetFrame((int)_frame + 1);
                }
                if (Input.GetMouseButtonDown(0))
                    _mouseDown = true;
                else if (_mouseDown)
                {
                    _mouseDown = false;
                    OnClick(this);
                }
            }
            else
            {
                if (_animated)
                {
                    _mouseOn = false;
                    SetFrame(0);
                    _frame = 0;
                }
                else SetFrame((int)_frame);
            }

            if (_frame < 5)
            {
                if (printed == true)
                {
                    _canvas.graphics.Clear(Color.Transparent);
                    printed = false;
                }

            }

        }

        void UpdateAnimation()
        {
            _frame = _frame + 0.5f;
            if (_frame >= frameCount - 1)
                _frame = 6;
            SetFrame((int)_frame);
        }
        public bool GetMouse()
        {
            return _mouseOn;
        }
    }
}
