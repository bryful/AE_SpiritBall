using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_SpiritBall
{
	public class AfterFX
	{
		private string m_Name = "";
		private string m_Directory = "";
		private string m_Caption = "";

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
		}

	}

	public class AfterFXs
	{
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
	}
}
