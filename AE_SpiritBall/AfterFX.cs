using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AE_SpiritBall
{
	public class AfterFX
	{

		private string m_Name = "";
		private string m_Directory = "";
		private string m_Caption = "";
		private int m_Version = 0;
		public int Version { get { return m_Version; } }
		public string FullPath { get {  return Path.Combine(m_Directory, m_Name); } }
		public string Directory { get { return m_Directory; } }
		public string Caption { get { return m_Caption; } }
		public string aerender { get { return Path.Combine(m_Directory, "aerender.exe"); } }
		public AfterFX()
		{
		}
		public AfterFX(string s)
		{
			SplitPath(s);
		}
		private void SplitPath(string s)
		{
			m_Name = "";
			m_Directory = "";
			m_Caption = "";
			if (s == "") return;
			m_Directory =Path.GetDirectoryName(s);
			m_Name = Path.GetFileName(s);
			if(m_Directory!="")
			{
				string ss = Path.GetFileName(Path.GetDirectoryName(m_Directory));
				ss = ss.Replace("Adobe After Effects", "").Trim();
				if(ss.IndexOf("CC ")==0)
				{
					ss = ss.Substring(3);
				}
				m_Caption = ss;
			}
			if(m_Caption=="")
			{
				m_Version = 12;
			}
			else
			{
				int v = 0;
				if (int.TryParse(m_Caption, out v))
				{
					switch(v)
					{
						case 2013:
							m_Version = 12;
							break;
						case 2014:
						case 2015:
						case 2016:
							m_Version = 13; 
							break;
						case 2017:
							m_Version = 14;
							break;
						case 2018:
							m_Version = 15;
							break;
						case 2019:
							m_Version = 16;
							break;
						case 2020:
							m_Version = 17;
							break;
						case 2021:
							m_Version = 18;
							break;
						case 2022:
							m_Version = 22;
							break;
						case 2023:
							m_Version = 23;
							break;
						case 2024:
							m_Version = 24;
							break;
						default:
							m_Version = v - 2000;
							break;

					}
				}
				else
				{
					if (m_Caption.IndexOf("CS6")==0)
					{
						m_Version = 11;
					}else if (m_Caption.IndexOf("CS5") == 0)
					{
						m_Version = 10;
					}
					else
					{
						m_Version = 3;
					}

				}
			}
		}

	}

	public class AfterFXs
	{
		private int m_SelectedIndex = -1;
		public int SelectedIndex
		{
			get { return m_SelectedIndex; }
			set
			{
                if (Count>0)
                {
					m_SelectedIndex = value;
					m_SelectedIndex %= Count;
					if (m_SelectedIndex < 0) m_SelectedIndex += Count;
				}
			}
		}
		public bool CanAfterFX
		{
			get { return ((m_SelectedIndex >= 0) && (m_SelectedIndex < Count)); }
		}
		public void SelectedIndexAdd(int v=1)
		{
			if(Count == 0) return;
			SelectedIndex = SelectedIndex + v;
		}
		public AfterFX AfterFX
		{
			get
			{
				AfterFX ret = null;
				if((m_SelectedIndex >= 0) && (m_SelectedIndex < Count))
				{
					ret = m_AfterFXList[m_SelectedIndex];
				}
				return ret;
			}
		}
		private string m_aepPath = "";
		public string aepPath
		{
			get { return m_aepPath; }
			set
			{
				string e = Path.GetExtension(value).ToLower();

				if ((e == ".aep")&& (File.Exists(value)))
				{
					m_aepPath = value;
				}
				else
				{
					m_aepPath = "";
				}
			}
		}
		private List<AfterFX> m_AfterFXList = new List<AfterFX>();
		public int Count { get { return m_AfterFXList.Count; } }
		public AfterFXs()
		{
			Listup();
		}
		public void Listup()
		{
			m_AfterFXList.Clear();
			string p = @"C:\Program Files\Adobe";

			string [] lst = Directory.GetDirectories(p);
			foreach (string s in lst)
			{
				string s1 = Path.GetFileName(s);
				if (s1.IndexOf("Adobe After Effects")==0)
				{
					string p2 = Path.Combine(p, s)+ "\\Support Files\\AfterFX.exe";
					if (File.Exists(p2))
					{
						m_AfterFXList.Add(new AfterFX(p2));
					}
				}
			}
			m_AfterFXList.Sort((a, b) => string.Compare(a.Caption , b.Caption));

		}

		public string Caption(int idx)
		{
			string ret = "";
			if((idx>=0)&&(idx<m_AfterFXList.Count))
			{
				ret = m_AfterFXList[idx].Caption;
			}
			return ret;
		}
		public string[] Captions
		{
			get
			{
				List<string> list = new List<string>();
				if (m_AfterFXList.Count>0) 
				{
					foreach(var a in m_AfterFXList)
					{
						list.Add(a.Caption);
					}
				}
				return list.ToArray();
			}
		}

		public bool IsSound { get; set; }=true;
		public bool IsMFR { get; set; } = true;
		public int MFRPer { get; set; } = 50;


		public string aerenderCmd()
		{
			string ret = "";
			if (AfterFX != null) 
			{

				ret = "-project \"{aepPath}\" {sound} {mfr}\"";
				ret = ret.Replace("{aepPath}", aepPath);
				if(IsSound)
				{
					ret = ret.Replace("{sound}", " -sound ON");

				}
				if((IsMFR) &&(AfterFX.Version>=22))
				{
					ret = ret.Replace("{mfr}", $" -mfr ON {MFRPer}");

				}
			}

			return ret;
		}
		public bool Run()
		{
			bool ret = false;
			if (AfterFX == null) return ret;
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = AfterFX.aerender,
				Arguments = aerenderCmd(),
				UseShellExecute = false,
				//RedirectStandardOutput = true,
				//RedirectStandardError = true,
				CreateNoWindow = false
			};
			try
			{
				using (Process process = Process.Start(startInfo))
				{
					// プロセスが開始されたことを確認するために少し待つ
					System.Threading.Thread.Sleep(1000);
					ret = true;

				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				ret = false;
			}
			return ret;
		}
	}
}
