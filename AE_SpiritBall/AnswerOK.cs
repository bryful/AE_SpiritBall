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
	public class AnswerOK : Form
	{
		private Button btnCancel;
		private TextBox tbAep;
		private CheckBox cbSound;
		private CheckBox cbMFR;
		private NumericUpDown numMFR;
		private TextBox tbVersion;
		private Button btnOK;

		public bool IsSound
		{
			get { return cbSound.Checked; }
			set
			{
				cbSound.Checked = value;
			}
		}
		public bool IsMFR
		{
			get { return cbMFR.Checked; }
			set
			{
				cbMFR.Checked = value;
			}
		}
		public int MFRPER
		{
			get { return (int)numMFR.Value; }
			set
			{
				numMFR.Value = (decimal)value;
			}
		}

		public string AepText
		{
			get { return tbAep.Text; }
			set
			{
				if(tbAep!=null)
					tbAep.Text = value;
			}
		}
		public string VersionText
		{
			get { return tbVersion.Text; }
			set
			{
				if (tbVersion != null)
					tbVersion.Text = value;
			}
		}
		public AnswerOK()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.tbAep = new System.Windows.Forms.TextBox();
			this.cbSound = new System.Windows.Forms.CheckBox();
			this.cbMFR = new System.Windows.Forms.CheckBox();
			this.numMFR = new System.Windows.Forms.NumericUpDown();
			this.tbVersion = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.numMFR)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Location = new System.Drawing.Point(373, 102);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 32);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.Location = new System.Drawing.Point(479, 102);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(100, 32);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// tbAep
			// 
			this.tbAep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(50)))));
			this.tbAep.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbAep.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.tbAep.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.tbAep.Location = new System.Drawing.Point(25, 39);
			this.tbAep.Multiline = true;
			this.tbAep.Name = "tbAep";
			this.tbAep.ReadOnly = true;
			this.tbAep.Size = new System.Drawing.Size(554, 57);
			this.tbAep.TabIndex = 2;
			this.tbAep.Text = "aaaaaa";
			// 
			// cbSound
			// 
			this.cbSound.AutoSize = true;
			this.cbSound.Checked = true;
			this.cbSound.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbSound.Location = new System.Drawing.Point(25, 111);
			this.cbSound.Name = "cbSound";
			this.cbSound.Size = new System.Drawing.Size(55, 16);
			this.cbSound.TabIndex = 3;
			this.cbSound.Text = "Sound";
			this.cbSound.UseVisualStyleBackColor = true;
			// 
			// cbMFR
			// 
			this.cbMFR.AutoSize = true;
			this.cbMFR.Checked = true;
			this.cbMFR.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbMFR.Location = new System.Drawing.Point(111, 111);
			this.cbMFR.Name = "cbMFR";
			this.cbMFR.Size = new System.Drawing.Size(117, 16);
			this.cbMFR.TabIndex = 4;
			this.cbMFR.Text = "MultiFrameRender";
			this.cbMFR.UseVisualStyleBackColor = true;
			// 
			// numMFR
			// 
			this.numMFR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(50)))));
			this.numMFR.Cursor = System.Windows.Forms.Cursors.AppStarting;
			this.numMFR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.numMFR.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numMFR.Location = new System.Drawing.Point(234, 108);
			this.numMFR.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.numMFR.Name = "numMFR";
			this.numMFR.Size = new System.Drawing.Size(53, 19);
			this.numMFR.TabIndex = 5;
			this.numMFR.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
			// 
			// tbVersion
			// 
			this.tbVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(50)))));
			this.tbVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbVersion.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.tbVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.tbVersion.Location = new System.Drawing.Point(25, 12);
			this.tbVersion.Multiline = true;
			this.tbVersion.Name = "tbVersion";
			this.tbVersion.ReadOnly = true;
			this.tbVersion.Size = new System.Drawing.Size(554, 29);
			this.tbVersion.TabIndex = 6;
			this.tbVersion.Text = "aaaaaa";
			// 
			// AnswerOK
			// 
			this.AcceptButton = this.btnOK;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(50)))));
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(596, 146);
			this.Controls.Add(this.tbVersion);
			this.Controls.Add(this.numMFR);
			this.Controls.Add(this.cbMFR);
			this.Controls.Add(this.cbSound);
			this.Controls.Add(this.tbAep);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AnswerOK";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			((System.ComponentModel.ISupportInitialize)(this.numMFR)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			using(Pen p = new Pen(ForeColor, 1))
			{
				e.Graphics.DrawRectangle(p,new Rectangle(0, 0, Width-1, Height-1));
			}
		}
	}
}
