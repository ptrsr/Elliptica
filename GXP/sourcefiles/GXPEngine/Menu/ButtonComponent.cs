using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GXPEngine.Core;
using GXPEngine;

using SRect = System.Drawing.Rectangle;

namespace GXPEngine
{
	public class ButtonComponent : GameObject
	{
		//stuff that most buttons will need
		private int _width = 0;
		private int _height = 0;

		private Vector2 _point;
		private System.Drawing.Rectangle _rect = new System.Drawing.Rectangle();

		private bool _mouseDown = false;

		public delegate void ClickEvent(ButtonComponent target);
		public event ClickEvent OnClick;

		private Color _baseColor = Color.Black;

		private Canvas _offButton = null;
		private Canvas _overButton = null;

		public Object data;

		public ButtonComponent (int width, int height, Color baseColor, string label, Object pData = null):base()
		{
			_width = width;
			_height = height;
			_baseColor = baseColor;

			_offButton = drawButton (_width, _height, baseColor);
			drawText (_offButton, label);
			AddChild (_offButton);

			_overButton = drawButton (_width, _height, ControlPaint.LightLight (baseColor));
			drawText (_overButton, label);
			AddChild (_overButton);

			_rect.Width = width;
			_rect.Height = height;

			data = pData;

			Game.main.Add (this);
		}

		private Canvas drawButton (int width, int height, Color color) {
			Canvas button = new Canvas (width, height);

			button.graphics.SmoothingMode = SmoothingMode.AntiAlias;

			GraphicsPath gp = RoundedRectangle.Create (0, 0, width, height, 8);
			LinearGradientBrush brush = new LinearGradientBrush (
				new SRect (0,0, width, height), 
				color, 
				ControlPaint.LightLight (color), 
				LinearGradientMode.Vertical
			);
			button.graphics.FillPath(brush, gp);
			gp.Dispose();
			brush.Dispose ();


			gp = RoundedRectangle.Create (2, 2, width-4, height/2, 8);
			brush = new LinearGradientBrush (
				new SRect (2,2, width-4, (height/2)+1), 
				Color.FromArgb (0x80, 0xff, 0xff, 0xff), 
				Color.FromArgb (0x0, 0xff, 0xff, 0xff), 
				LinearGradientMode.Vertical
			);
			button.graphics.FillPath(brush, gp);
			gp.Dispose();
			brush.Dispose ();

			return button;
		}

		private void drawText (Canvas canvas, string text) {
			Font font = new Font ("Arial", 16);
			StringFormat sf = new StringFormat ();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;
			canvas.graphics.DrawString (text, font, Brushes.White, _width / 2, _height / 2, sf);
		}

		public int width {
			get {
				return _width;
			}
		}

		public int height {
			get {
				return _height;
			}
		}

		public float alpha {
			get { return _offButton.alpha; }
			set { _offButton.alpha = _overButton.alpha = value; }
		}


		void Update() {
			//no listeners? Skip check
			if (OnClick == null)
				return;

			//otherwise get rectangle in global space, this assumes no rotation, that takes more work :)
			_point = TransformPoint (0, 0);
			_rect.X = (int)_point.x;
			_rect.Y = (int)_point.y; 
			_point = TransformPoint (_width, _height);
			_rect.Width = (int)_point.x - _rect.X;
			_rect.Height = (int)_point.y - _rect.Y;

			//are we hitting the button?
			bool hit = _rect.Contains (Input.mouseX, Input.mouseY);
			//update over skin
			_overButton.visible = hit;

			//if we are hitting 
			if (hit) {
				//check if mouse is down, if so register
				if (Input.GetMouseButton (0)) {
					_mouseDown = true;
				//otherwise check if it WAS down, if so, trigger the click
				} else if (_mouseDown) {
					_mouseDown = false;
					OnClick (this);
				}
			} else {
				//if we are not hitting, just register the mouse down/up
				_mouseDown = Input.GetMouseButtonDown (0);
			}


		}



	}
}

