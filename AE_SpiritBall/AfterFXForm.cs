using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_SpiritBall
{
	public class AfterFXForm : Form
	{
		private AfterFXs m_afterFXs = new AfterFXs();
		private int m_ItemWidth = 72;
		[Category("SB")]
		public int ItemWidth
		{
			get { return m_ItemWidth; }
			set
			{
				m_ItemWidth = value;
				ChkSize();
				this.Invalidate();
			}
		}
		private int m_ItemHeight = 24;
		[Category("SB")]
		public int ItemHeight
		{
			get { return m_ItemHeight; }
			set
			{
				m_ItemHeight = value;
				ChkSize();
				this.Invalidate();
			}
		}
		private int m_BarHeight = 32;
		[Category("SB")]
		public int BarHeight
		{
			get { return m_BarHeight; }
			set
			{
				m_BarHeight = value;
				ChkSize();
				this.Invalidate();
			}
		}

		private Rectangle m_CloseBtnRect = new Rectangle();
		public Rectangle CloseBtnRect
		{
			get { return m_CloseBtnRect; }
		}

		private int m_SelectedIndex = -1;
		[Category("SB")]
		public int SelectedIndex
		{
			get { return (int)m_SelectedIndex; }
		}

		private Color m_NormalColor = Color.FromArgb(128,128,200);
		[Category("SB_Color")]
		public Color NormalColor
		{
			get { return m_NormalColor; }
			set { m_NormalColor = value; this.Invalidate(); }
		}

		private Color m_SelectedColor = Color.FromArgb(200, 200, 255);
		[Category("SB_Color")]
		public Color SelectedColor
		{
			get { return m_SelectedColor; }
			set { m_SelectedColor = value; this.Invalidate(); }
		}
		private Color m_SelectedColorHi = Color.FromArgb(235, 235, 255);
		[Category("SB_Color")]
		public Color SelectedColorHi
		{
			get { return m_SelectedColorHi; }
			set { m_SelectedColorHi = value;}
		}
		private Color m_BarColor = Color.FromArgb(64, 64, 64);
		[Category("SB_Color")]
		public Color BarColor
		{
			get { return m_BarColor; }
			set { m_BarColor = value; this.Invalidate(); }
		}
		private Color m_CloseBtnColor = Color.FromArgb(220, 220, 255);
		[Category("SB_Color")]
		public Color CloseBtnColor
		{
			get { return m_CloseBtnColor; }
			set { m_CloseBtnColor = value; this.Invalidate(); }
		}
		private Color m_TextColor = Color.FromArgb(220, 220, 255);
		[Category("SB_Color")]
		public Color TextColor
		{
			get { return m_TextColor; }
			set { m_TextColor = value; this.Invalidate(); }
		}
		[Category("SB_Color")]
		public new Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; this.Invalidate(); }
		}
		[Category("SB_Color")]
		public new Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; this.Invalidate(); }
		}
		private Font m_TextFont;
		[Category("SB_Color")]
		public Font TextFont
		{
			get { return this.m_TextFont; }
			set
			{
				this.m_TextFont = value;
				this.Invalidate();
			}
		}
		public AfterFXForm()
		{
			m_TextFont = base.Font;
			DoubleBuffered = true;

			if (m_afterFXs.Count > 0)
			{
				m_SelectedIndex = m_afterFXs.Count - 1;
			}
			ChkSize();
		}
		private void ChkSize()
		{
			this.ClientSize = new Size(
				m_ItemWidth * m_afterFXs.Count, 
				m_ItemHeight+m_BarHeight);
			int w = m_BarHeight / 2;
			m_CloseBtnRect = new Rectangle(this.Width - w - 4, w / 2, w, w);
		}
		protected override void OnResize(EventArgs e)
		{
			this.ClientSize = new Size(m_ItemWidth * m_afterFXs.Count, m_ItemHeight+m_BarHeight);
			base.OnResize(e);
		}
		private void DrawItem(Graphics g, SolidBrush sb, Pen p, int idx,StringFormat sf)
		{
			sb.Color = m_NormalColor;
			if(idx == m_SelectedIndex) 
			{
				if (m_md2)
				{
					sb.Color = m_SelectedColorHi;

				}
				else
				{
					sb.Color = m_SelectedColor;
				}
			}
			Rectangle rct = new Rectangle(
				idx * m_ItemWidth + 2, 
				2 + m_BarHeight, 
				m_ItemWidth - 4, m_ItemHeight - 4);
			g.FillRectangle(sb, rct);
			sb.Color =m_TextColor;
			g.DrawString(m_afterFXs.Caption(idx), m_TextFont, sb,rct, sf);
			rct = new Rectangle(rct.Left, rct.Top, rct.Width - 1, rct.Height - 1);
			p.Color = ForeColor;
			g.DrawRectangle(p, rct);

		}
		// **************************************************************
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			using (SolidBrush sb = new SolidBrush(ForeColor))
			using (Pen p = new Pen(ForeColor))
			using (StringFormat sf = new StringFormat())
			{
				sb.Color = BackColor;
				g.FillRectangle(sb,this.ClientRectangle);
				Rectangle rct = new Rectangle(0, 0, this.Width, m_BarHeight);
				sb.Color = m_BarColor;
				g.FillRectangle(sb, rct);
				int ww = m_BarHeight / 2;
				sb.Color = m_CloseBtnColor;
				g.FillRectangle(sb, m_CloseBtnRect);
				rct = new Rectangle(4, 0, this.Width - m_CloseBtnRect.Width - 8, m_BarHeight);
				sb.Color = m_TextColor;
				sf.Alignment = StringAlignment.Near;
				sf.LineAlignment = StringAlignment.Center;
				g.DrawString(base.Text, this.Font, sb, rct, sf);


				if (m_afterFXs.Count > 0)
				{
					sf.Alignment = StringAlignment.Center;
					sf.LineAlignment = StringAlignment.Center;
					for (int i = 0; i < m_afterFXs.Count; i++)
					{
						DrawItem(g,sb,p,i,sf);

					}
				}

			}
		}
		// **************************************************************
		public void ShowAnswer()
		{
			bool tm = this.TopMost;
			this.TopMost = false;
			using(AnswerOK dlg = new AnswerOK())
			{
				dlg.InfoText = "aaaaa.aep";
				dlg.TopMost = true;
				if(dlg.ShowDialog()== DialogResult.OK)
				{

				}
			}
			this.TopMost = tm;
		}


		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.Invalidate();
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if(m_md)
			{
				int dx = this.Left + e.X - m_mdp.X;
				int dy = this.Top + e.Y - m_mdp.Y;
				this.Location = new Point(m_mdloc.X+ dx, m_mdloc.Y + dy);
			}
			else
			{
				int idx = e.X / m_ItemWidth;
				if (m_SelectedIndex != idx)
				{
					m_SelectedIndex = idx;
					this.Invalidate();

				}
			}
			base.OnMouseMove(e);
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			this.Invalidate();
			base.OnMouseLeave(e);
		}
		private Point m_mdp = new Point(0,0);
		private Point m_mdloc = new Point(0, 0);

		private bool m_md = false;
		private bool m_md2 = false;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Y < m_BarHeight)
			{
				if((e.X>=m_CloseBtnRect.Left)&&(e.X<m_CloseBtnRect.Right))
				{
					Application.Exit();
					return;
				}
				m_md = true;
				m_mdp = new Point(this.Left + e.X, this.Top + e.Y);
				m_mdloc = this.Location;
			}
			else
			{
				int idx = e.X / m_ItemWidth;
				if ((idx>=0)&&(idx<m_afterFXs.Count))
				{
					m_SelectedIndex = idx;
					m_md2 = true;
					this.Invalidate();
					ShowAnswer();
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if(m_md)
			{
				m_md = false;
			}
			if(m_md2)
			{
				m_md2 = false;
				this.Invalidate();
			}
			base.OnMouseUp(e);
		}
	}
}
