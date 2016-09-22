using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;
using System.Globalization;

namespace xxe
{
	
	public partial class Default : System.Web.UI.Page
	{
		//NO XXE
		public void button1Clicked (object sender, EventArgs args)
		{
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load("xxe_clean.xml");
			// получим корневой элемент
			XmlElement xRoot = xDoc.DocumentElement;
			// обход всех узлов в корневом элементе
			foreach(XmlNode xnode in xRoot)
			{
				// получаем атрибут vuln
				if(xnode.Attributes.Count>0)
				{
					XmlNode attr = xnode.Attributes.GetNamedItem("name");
					if (attr!=null)
						button1.Text = attr.Value;
				}
			}
		}

		//XXE 
		public void button2Clicked (object sender, EventArgs args)
		{
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load("xxe.xml");
			// получим корневой элемент
			XmlElement xRoot = xDoc.DocumentElement;
			// обход всех узлов в корневом элементе
			foreach(XmlNode xnode in xRoot)
			{
				// получаем атрибут vuln
				if(xnode.Attributes.Count>0)
				{
					XmlNode attr = xnode.Attributes.GetNamedItem("name");
					if (attr!=null)
						button2.Text = attr.Value;
				}
			}
		}

		//XXE via Reader
		public void button3Clicked (object sender, EventArgs args)
		{
			String vulnstr = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n<vulners>\n\t<vuln name=\"xss\">XSS</vuln>\n\t<vuln name=\"xxe\">XXE</vuln>\n</vulners>";
			MemoryStream stream = new MemoryStream(System.Text.Encoding.Default.GetBytes(vulnstr));
			XmlReaderSettings settings = new XmlReaderSettings();

			settings.DtdProcessing = DtdProcessing.Prohibit;
			settings.MaxCharactersFromEntities = 6000;
			XmlReader reader = XmlReader.Create(stream, settings);

			XmlDocument xDoc = new XmlDocument();
			try{
				xDoc.Load(reader);
			}
			catch (System.Exception ex){
				button3.Text = ex.ToString();
			}
			// получим корневой элемент
			XmlElement xRoot = xDoc.DocumentElement;
			// обход всех узлов в корневом элементе
			foreach(XmlNode xnode in xRoot)
			{
				// получаем атрибут vuln
				if(xnode.Attributes.Count>0)
				{
					XmlNode attr = xnode.Attributes.GetNamedItem("name");
					if (attr!=null)
						button3.Text = attr.Value;
				}
			}
		}
	}
}

