using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net;

namespace YGOShared
{
    class XmlHandler
    {
        public Card[] loadXml(Card[] trunk)
        {
            XDocument doc = XDocument.Load("YGOCardDB.xml");

            var dbName = doc.Descendants("Name");
            var dbDescription = doc.Descendants("Description");
            var dbNumber = doc.Descendants("Number");
            var dbType = doc.Descendants("Type");
            var dbAttribute = doc.Descendants("Attribute");
            var dbAttack = doc.Descendants("Attack");
            var dbDefence = doc.Descendants("Defence");
            var dbLevel = doc.Descendants("Level");

            // Load cards from XML
            for (int i = 0; i < 126; i++)
            {
                trunk[i] = new Card();
                trunk[i].Name = dbName.ElementAt(i).Value;
                trunk[i].Description = dbDescription.ElementAt(i).Value;
                trunk[i].Number = int.Parse(dbNumber.ElementAt(i).Value);
                trunk[i].Type = dbType.ElementAt(i).Value;
                trunk[i].Attribute = dbAttribute.ElementAt(i).Value;
                trunk[i].Attack = int.Parse(dbAttack.ElementAt(i).Value);
                trunk[i].Defence = int.Parse(dbDefence.ElementAt(i).Value);
                trunk[i].Level = int.Parse(dbLevel.ElementAt(i).Value);
            }
            return trunk;
        }
        public void downloadDB()
        {
            WebClient website = new WebClient();
            website.BaseAddress = ("http://www.db.yugioh-card.com/yugiohdb/");

            //http://www.db.yugioh-card.com/yugiohdb/card_search.action?ope=2&cid=NUMBER
            //This is the address to list individual cards. Replace 'NUMBER' with any int from 4007 to 12272 inclusive

            XDocument doc = new XDocument(new XElement("CardDB"));
            
            for (int i = 4007; i < 4009; i++)
            {
                string webOut = website.DownloadString("card_search.action?ope=2&cid=" + i);

                int start = webOut.IndexOf("<article");
                int finish = webOut.LastIndexOf("</article>") + "<article>".Length;
                webOut = webOut.Substring(start, finish - start);

                start = webOut.IndexOf("<h1>") + "<h1>".Length;
                finish = webOut.IndexOf("</h1>");
                string name = webOut.Substring(start, finish - start);
                name = name.Trim();

                start = webOut.IndexOf("<td valign=\"top\">");
                finish = webOut.LastIndexOf("<script");
                webOut = webOut.Substring(start, finish - start);

                Console.WriteLine(webOut);

                doc.Root.Add(new XElement("id_" + i, new XElement("Name", name),
                new XElement("Attribute", "LIGHT"),
                new XElement("Level", 8),
                new XElement("Monster_Type", "Dragon"),
                new XElement("ATK", 3000),
                new XElement("DEF", 2500),
                new XElement("Card_Text", "This legendary dragon is a powerful engine of destruction. Virtually invincible, very few have faced this awesome creature and lived to tell the tale.")));
                
            }

            doc.Save("CardDB.xml");
        }
        public XmlHandler()
        { }
    }
}
