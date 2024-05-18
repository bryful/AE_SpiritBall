using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_SpiritBall
{
	public class AEIcon :Control
	{
		public delegate void AepEventHandler(object sender, AepEventArgs e);

		//イベントデリゲートの宣言
		public event AepEventHandler Aep;

		protected virtual void OnAep(AepEventArgs e)
		{
			Aep?.Invoke(this, e);
		}
		public event EventHandler KeyEnter;
		protected virtual void OnKeyEnter(EventArgs e)
		{
			KeyEnter?.Invoke(this, e);
		}
		
		[DllImport("user32.dll")]
		private static extern IntPtr SetFocus(IntPtr hWnd);


		public int Index { get; set; } = -1;

		/*
		private bool m_IsSelected = false;
		[Category("AEIcon")]
		public bool IsSelected
		{
			get { return m_IsSelected; }
			set
			{
				m_IsSelected = value;
				this.Invalidate();
			}
		}
		*/
		public Color m_SelectedColor =Color.FromArgb(150, 150, 200);
		[Category("AEIcon")]
		public Color SelectedColor
		{
			get { return m_SelectedColor; }
			set
			{
				m_SelectedColor = value;
				this.Invalidate();
			}
		}
		public Color m_SelectedColorNone = Color.FromArgb(100, 100, 150);
		[Category("AEIcon")]
		public Color SelectedColorNone
		{
			get { return m_SelectedColorNone; }
			set
			{
				m_SelectedColorNone = value;
				this.Invalidate();
			}
		}
		public Color m_LineColor = Color.FromArgb(10, 10, 15);
		[Category("AEIcon")]
		public Color LineColor
		{
			get { return m_LineColor; }
			set
			{
				m_LineColor = value;
				this.Invalidate();
			}
		}
		public Color m_FocusLineColor = Color.FromArgb(200, 200, 255);
		[Category("AEIcon")]
		public Color FocusLineColor
		{
			get { return m_FocusLineColor; }
			set
			{
				m_FocusLineColor = value;
				this.Invalidate();
			}
		}
		public Color m_TextColor = Color.FromArgb(220, 220, 255);
		[Category("AEIcon")]
		public Color TextColor
		{
			get { return m_TextColor; }
			set
			{
				m_TextColor = value;
				this.Invalidate();
			}
		}
		public Color m_TextColorNone = Color.FromArgb(22, 22, 25);
		[Category("AEIcon")]
		public Color TextColorNone
		{
			get { return m_TextColorNone; }
			set
			{
				m_TextColorNone = value;
				this.Invalidate();
			}
		}
		public AEIcon()
		{
			DoubleBuffered = true;
			AllowDrop = true;
			this.SetStyle(
			ControlStyles.Selectable |
			ControlStyles.UserMouse |
			ControlStyles.DoubleBuffer |
			ControlStyles.UserPaint |
			ControlStyles.AllPaintingInWmPaint |
			ControlStyles.SupportsTransparentBackColor,
			true);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			using(SolidBrush sb = new SolidBrush(m_SelectedColorNone))
			using (Pen p = new Pen(m_LineColor))
			using (StringFormat sf = new StringFormat())
			{
				Graphics g = e.Graphics;
				Rectangle rct = this.ClientRectangle;
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;
				sb.Color = m_TextColor;
				if (this.Focused)
				{
					g.Clear(m_SelectedColor);
					sb.Color = m_TextColor;
					p.Color = m_FocusLineColor;
				}
				else
				{
					g.Clear(m_SelectedColorNone);
					sb.Color = m_TextColorNone;
					p.Color = m_LineColor;
				}
				g.DrawString(this.Text, this.Font, sb, rct, sf);
				rct = new Rectangle(rct.Left, rct.Top, rct.Width - 1, rct.Height - 1);
				g.DrawRectangle(p,rct);
			}
		}
		
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.Invalidate();
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.Invalidate();
		}
		
		protected override void OnMouseEnter(EventArgs e)
		{
			this.Focus();
			this.Invalidate();
			base.OnMouseEnter(e);
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			this.Invalidate();
			base.OnMouseLeave(e);
		}
		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			drgevent.Effect = DragDropEffects.All;
			this.Focus();
			this.Invalidate();
			OnKeyEnter(new EventArgs());
			base.OnDragEnter(drgevent);
		}
		protected override void OnDragLeave(EventArgs e)
		{
			this.Invalidate();
			base.OnDragLeave(e);
		}
		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
			{
				foreach (var filePath in (string[])drgevent.Data.GetData(DataFormats.FileDrop))
				{
					if(File.Exists(filePath))
					{
						string e = Path.GetExtension(filePath).ToLower();
						if(e == ".aep")
						{
							OnAep(new AepEventArgs(filePath,Index));
							break;
						}
					}

				}
			}
			base.OnDragDrop(drgevent);
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			Debug.WriteLine(e.KeyCode.ToString());
			if(e.KeyCode == Keys.Enter)
			{
				OnKeyEnter(new EventArgs());
			}
			base.OnKeyDown(e);
		}
	}
	public class AepEventArgs : EventArgs
	{
		public string Aep;
		public int Index;
		public AepEventArgs(string s,int idx)
		{
			Aep = s;
			Index = idx;
		}	
	}
}
