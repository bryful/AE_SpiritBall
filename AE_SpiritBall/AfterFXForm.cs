using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

		[Category("SB")]
		public int SelectedIndex
		{
			get { return (int)m_afterFXs.SelectedIndex; }
			set
			{
				m_afterFXs.SelectedIndex = value;

				if(m_aeicons.Count > 0)
				{
					m_aeicons[m_afterFXs.SelectedIndex].Focus();
				}
			}
		}

		private Color m_SelectedColorNone = Color.FromArgb(128, 128, 200);
		[Category("SB_Color")]
		public Color SelectedColorNone
		{
			get { return m_SelectedColorNone; }
			set
			{
				m_SelectedColorNone = value;
				if (m_aeicons.Count > 0)
				{
					for (int i = 0; i < m_aeicons.Count; i++)
					{
						m_aeicons[i].SelectedColorNone = m_SelectedColorNone;
					}
				}
				this.Invalidate();
			}
		}

		private Color m_SelectedColor = Color.FromArgb(200, 200, 255);
		[Category("SB_Color")]
		public Color SelectedColor
		{
			get { return m_SelectedColor; }
			set
			{
				m_SelectedColor = value;
				if (m_aeicons.Count > 0)
				{
					for (int i = 0; i < m_aeicons.Count; i++)
					{
						m_aeicons[i].SelectedColor = m_SelectedColor;
					}
				}
				this.Invalidate();
			}
		}
		private Color m_SelectedColorHi = Color.FromArgb(235, 235, 255);

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
			set
			{
				m_TextColor = value;
				if (m_aeicons.Count > 0)
				{
					for (int i = 0; i < m_aeicons.Count; i++)
					{
						m_aeicons[i].TextColor = m_TextColor;
					}
				}
				this.Invalidate();
			}
		}
		private Color m_TextColorNone = Color.FromArgb(22, 22, 25);
		[Category("SB_Color")]
		public Color TextColorNone
		{
			get { return m_TextColorNone; }
			set
			{
				m_TextColorNone = value;
				if (m_aeicons.Count > 0)
				{
					for (int i = 0; i < m_aeicons.Count; i++)
					{
						m_aeicons[i].TextColorNone = m_TextColorNone;
					}
				}
				this.Invalidate();
			}
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
		private ContextMenuStrip contextMenuStrip1;
		private IContainer components;
		private ToolStripMenuItem openAepFileMenu;
		private ToolStripMenuItem clearAepPathMenu;
		private ToolStripMenuItem quitMenu;

		[Category("SB_Color")]
		public Font TextFont
		{
			get { return this.m_TextFont; }
			set
			{
				this.m_TextFont = value;
				if (m_aeicons.Count > 0)
				{
					for (int i = 0; i < m_aeicons.Count; i++)
					{
						m_aeicons[i].Font = m_TextFont;
					}
				}
				this.Invalidate();
			}
		}
		private List<AEIcon> m_aeicons = new List<AEIcon>();
		public AfterFXForm()
		{
			m_TextFont = base.Font;
			DoubleBuffered = true;
			InitializeComponent();

			if (m_afterFXs.Count > 0)
			{
				m_afterFXs.SelectedIndex = m_afterFXs.Count - 1;

				for (int i = 0; i < m_afterFXs.Count; i++)
				{
					AEIcon ai = new AEIcon();
					ai.Text = m_afterFXs.Caption(i);
					ai.Index = i;
					ai.Size = new Size(m_ItemWidth - 4, m_ItemHeight - 4);
					ai.Location = new Point(m_ItemWidth * i + 2, 2 + m_BarHeight);
					ai.Aep += (sender, e) =>
					{
						m_afterFXs.aepPath = e.Aep;
						SelectedIndex = e.Index;
						this.Invalidate();
						Exec();
					};
					ai.MouseClick += (sender, e) =>
					{
						AEIcon rr = (AEIcon)sender;
						m_afterFXs.SelectedIndex = rr.Index;
						Exec();
					};
					ai.KeyEnter += (sender, e) =>
					{
						AEIcon rr = (AEIcon)sender;
						m_afterFXs.SelectedIndex = rr.Index;
						Exec();

					};
					m_aeicons.Add(ai);
					this.Controls.Add(ai);
				}

			}
			ChkSize();


			quitMenu.Click += (sender, e) =>
			{
				Application.Exit();
			};
			openAepFileMenu.Click += (sender, e) =>
			{
				OpenAepDialog();
			};
			clearAepPathMenu.Click += (sender, e) =>
			{
				m_afterFXs.aepPath = "";
				SetAepPath("");
				this.Invalidate();
			};
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string[] cmds = System.Environment.GetCommandLineArgs();
			if (cmds.Length > 1)
			{
				for (int i = 1; i < cmds.Length; i++)
				{
					if (SetAepPath(cmds[i]) == true)
					{
						break;
					}
				}
			}
			this.KeyPreview = true;
			SelectedIndex = m_afterFXs.Count-1;	
		}
		public bool SetAepPath(string p)
		{
			bool ret = false;
			this.Text = "AE元気玉";
			if (File.Exists(p) == false) return ret;
			m_afterFXs.aepPath = p;
			if (m_afterFXs.aepPath != "")
			{
				string[] pp = p.Split('\\');
				string cap = "";
				if (pp.Length > 0)
				{
					cap = pp[pp.Length - 1];
					if (cap.Length > 1)
					{
						cap = pp[pp.Length - 2] + "//" + cap;
					}
					if (cap.Length > 2)
					{
						cap = pp[pp.Length - 3] + "//" + cap;
					}
				}
				this.Text = cap;
				m_aeppath = p;
				this.Invalidate();
				ret = true;
			}
			return ret;
		}
		private void ChkSize()
		{
			int cou = m_afterFXs.Count;
			if (cou <= 0) cou = 1;
			this.ClientSize = new Size(
				m_ItemWidth * cou,
				m_ItemHeight + m_BarHeight);
			int w = m_BarHeight / 2;
			m_CloseBtnRect = new Rectangle(this.Width - w - 4, w / 2, w, w);
			if (m_aeicons.Count > 0)
			{
				for (int i = 0; i < m_aeicons.Count; i++)
				{
					m_aeicons[i].Size = new Size(m_ItemWidth - 4, m_ItemHeight - 4);
					m_aeicons[i].Location = new Point(m_ItemWidth * i + 2, 2 + m_BarHeight);
				}
			}
		}
		protected override void OnResize(EventArgs e)
		{
			this.ClientSize = new Size(m_ItemWidth * m_afterFXs.Count, m_ItemHeight + m_BarHeight);
			base.OnResize(e);
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
				g.FillRectangle(sb, this.ClientRectangle);
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


			}
		}
		// **************************************************************

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (m_md)
			{
				int dx = this.Left + e.X - m_mdp.X;
				int dy = this.Top + e.Y - m_mdp.Y;
				this.Location = new Point(m_mdloc.X + dx, m_mdloc.Y + dy);
			}
			base.OnMouseMove(e);
		}
		private Point m_mdp = new Point(0, 0);
		private Point m_mdloc = new Point(0, 0);

		private bool m_md = false;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Y < m_BarHeight)
			{
				if ((e.X >= m_CloseBtnRect.Left) && (e.X < m_CloseBtnRect.Right))
				{
					Application.Exit();
					return;
				}
				m_md = true;
				m_mdp = new Point(this.Left + e.X, this.Top + e.Y);
				m_mdloc = this.Location;
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_md)
			{
				m_md = false;
			}
			base.OnMouseUp(e);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openAepFileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.quitMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.clearAepPathMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openAepFileMenu,
            this.clearAepPathMenu,
            this.quitMenu});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
			// 
			// openAepFileMenu
			// 
			this.openAepFileMenu.Name = "openAepFileMenu";
			this.openAepFileMenu.Size = new System.Drawing.Size(180, 22);
			this.openAepFileMenu.Text = "Open AepFile";
			// 
			// quitMenu
			// 
			this.quitMenu.Name = "quitMenu";
			this.quitMenu.Size = new System.Drawing.Size(180, 22);
			this.quitMenu.Text = "Quit";
			// 
			// clearAepPathMenu
			// 
			this.clearAepPathMenu.Name = "clearAepPathMenu";
			this.clearAepPathMenu.Size = new System.Drawing.Size(180, 22);
			this.clearAepPathMenu.Text = "Clear AepPath";
			// 
			// AfterFXForm
			// 
			this.ClientSize = new System.Drawing.Size(507, 104);
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Name = "AfterFXForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		public void Exec()
		{
			if (m_afterFXs.aepPath == "")
			{
				if (OpenAepDialog() == false)
				{
					return;
				}
			}
			if (m_afterFXs.CanAfterFX)
			{
				if (ShowAnswer())
				{
					if (m_afterFXs.Run())
					{
						Application.Exit();
					}
				}
			}
		}
		private string m_aeppath = string.Empty;
		public bool OpenAepDialog()
		{
			bool ret = false;
			bool tm = this.TopMost;
			this.TopMost = false;
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.Filter = "*.aep|*.aep|*.*|*.*";
				dlg.DefaultExt = ".aep";
				if (Directory.Exists(m_aeppath))
				{
					dlg.InitialDirectory = Path.GetDirectoryName(m_aeppath);
				}
				else
				{

				}
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					m_afterFXs.aepPath = dlg.FileName;
					if (m_afterFXs.aepPath != "")
					{
						ret = SetAepPath(dlg.FileName);
					}
				}
				this.TopMost = tm;
			}
			return ret;
		}
		public bool ShowAnswer()
		{
			bool ret = false;
			bool tm = this.TopMost;
			this.TopMost = false;
			using (AnswerOK dlg = new AnswerOK())
			{
				dlg.AepText = m_afterFXs.aepPath;
				dlg.VersionText = m_afterFXs.AfterFX.Caption;
				dlg.TopMost = true;
				dlg.IsSound = m_afterFXs.IsSound;
				dlg.IsMFR = m_afterFXs.IsMFR;
				dlg.MFRPER = m_afterFXs.MFRPer;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					m_afterFXs.IsSound = dlg.IsSound;
					m_afterFXs.IsMFR = dlg.IsMFR;
					m_afterFXs.MFRPer = dlg.MFRPER;
					ret = true;

				}
			}
			this.TopMost = tm;
			return ret;
		}
	}
}
