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
            
            for (int i = 4007; i < 4060; i++)
            {
                var name = "";
                var attribute = "";
                var level = "";
                var monsterType = "";
                var attack = "";
                var defence = "";
                var cardText = "";
                var webOut = website.DownloadString("card_search.action?ope=2&cid=" + i);

                var start = webOut.IndexOf("<article");
                var finish = webOut.LastIndexOf("</article>") + "<article>".Length;
                webOut = webOut.Substring(start, finish - start);
                start = webOut.IndexOf("<h1>") + "<h1>".Length;
                finish = webOut.IndexOf("</h1>");
                try
                {
                    name = webOut.Substring(start, finish - start);
                    name = name.Trim();
                }
                catch { }

                try
                {
                    attribute = extractElement(webOut, "<b>Attribute</b>");
                }

                catch { }

                try
                {
                    level = extractElement(webOut, "<b>Level</b>");
                }

                catch { }

                try
                {
                    monsterType = extractElement(webOut, "<b>Monster Type</b>");
                }
                catch { }

                try
                {
                    attack = extractElement(webOut, "<b>ATK</b>");
                }
                catch{ }

                try
                {
                    attack = extractElement(webOut, "<b>ATK</b>");
                }
                catch { }

                try
                {
                    defence = extractElement(webOut, "<b>DEF</b>");
                }
                catch { }

                try
                {
                    cardText = extractElement(webOut, "<b>Card Text</b>");
                }
                catch { }                
                
                doc.Root.Add(new XElement("id_" + i, new XElement("Name", name),
                new XElement("Attribute", attribute),
                new XElement("Level", level),
                new XElement("Monster_Type", monsterType),
                new XElement("ATK", attack),
                new XElement("DEF", defence),
                new XElement("Card_Text", cardText)));
                
            }

            doc.Save("CardDB.xml");
        }

        public string extractElement(string webOut, string el)
        {
            var start = webOut.IndexOf(el);
            var finish = 0;
            string element = "";

            webOut = webOut.Substring(start);
            if(webOut.IndexOf("</div>") < webOut.IndexOf("</span>"))
            {
                start = webOut.IndexOf("</div>");
                webOut = webOut.Substring(start + 6);
                finish = webOut.IndexOf("</div>");
                element = webOut.Substring(0, finish);
                element = element.Trim();
            }
            else
            {
                start = webOut.IndexOf("</span>");
                webOut = webOut.Substring(start + 7);
                if (webOut.IndexOf("<span class=\"item_box_value\">") < webOut.IndexOf("</div>"))
                {
                    start = webOut.IndexOf("<span class=\"item_box_value\">");
                    webOut = webOut.Substring(start + 28);
                    finish = webOut.IndexOf("</span>");
                    element = webOut.Substring(start, finish - 11);
                    element = element.Trim();
                }
                else
                {
                    finish = webOut.IndexOf("</div>");
                    element = webOut.Substring(0, finish);
                    element = element.Trim();
                }
            }
            return element;
        }
        public XmlHandler()
        { }
    }
}
