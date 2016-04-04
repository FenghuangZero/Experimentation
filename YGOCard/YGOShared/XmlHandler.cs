using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net.Http;


namespace YGOShared
{
    /// <summary>
    /// Creates an object to control the database of cards.
    /// </summary>
    class XmlHandler
    {
        
        Uri uri = new Uri("http://www.db.yugioh-card.com/yugiohdb/");
        //http://www.db.yugioh-card.com/yugiohdb/card_search.action?ope=2&cid=NUMBER
        //This is the address to list individual cards. Replace 'NUMBER' with any int from 4007 to 12272 inclusive


        /// <summary>
        /// Loads the database from a pre-existing Xml file.
        /// </summary>
        /// <param name="t">The object which will store the database while loaded.</param>
        /// <returns></returns>
        public Card[] loadXml(Card[] t)
        {
            XDocument doc = XDocument.Load("YGOCardDB.xml");

            var dbName = doc.Descendants("Name");
            var dbAttribute = doc.Descendants("Attribute");
            var dbLevel = doc.Descendants("Level");
            var dbType = doc.Descendants("Type");
            var dbAttack = doc.Descendants("Attack");
            var dbDefence = doc.Descendants("Defence");
            var dbText = doc.Descendants("Description");

            // Load cards from XML
            for (int i = 0; i < 126; i++)
            {
                t[i] = new Card();
                t[i].Name = dbName.ElementAt(i).Value;
                t[i].Attribute = dbAttribute.ElementAt(i).Value;
                t[i].Level = int.Parse(dbLevel.ElementAt(i).Value);
                t[i].MonsterType = dbType.ElementAt(i).Value;
                t[i].ATK = int.Parse(dbAttack.ElementAt(i).Value);
                t[i].DEF = int.Parse(dbDefence.ElementAt(i).Value);
                t[i].CardText = dbText.ElementAt(i).Value;
            }
            return t;
        }

        /// <summary>
        /// Reads the HTML page and extracts the header.
        /// </summary>
        /// <param name="webOut">A string representation of the HTML.</param>
        /// <returns></returns>
        public string extractName(string webOut)
        {
            var name = "";
            try
            {
                var start = webOut.IndexOf("<article");
                var finish = webOut.LastIndexOf("</article>") + "<article>".Length;
                webOut = webOut.Substring(start, finish - start);
                start = webOut.IndexOf("<h1>") + "<h1>".Length;
                finish = webOut.IndexOf("</h1>");
                name = webOut.Substring(start, finish - start);
                name = name.Trim();
            }
            catch { }
            
            return name;

        }

        /// <summary>
        /// Reads the HTML page and extracts information about a card.
        /// </summary>
        /// <param name="webOut">A string representation of the HTML</param>
        /// <param name="el">The tag that indicates the location on the page containing the required information.</param>
        /// <returns></returns>
        public string extractElement(string webOut, string el)
        {
            var start = webOut.IndexOf(el);
            var finish = 0;
            string element = "";
            try
            {
                webOut = webOut.Substring(start);
                if (webOut.IndexOf("</div>") < webOut.IndexOf("</span>"))
                {
                    start = webOut.IndexOf("</div>");
                    webOut = webOut.Substring(start + 6);
                    if (webOut.IndexOf("<div") < webOut.IndexOf("</div>"))
                        finish = webOut.IndexOf("<div");
                    else
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
            }
            catch { }
            return element;
        }

        /// <summary>
        /// Queries 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public async Task<string> getWebsiteStringAsync(Uri u)
        {
            HttpClient httpclient = new HttpClient();
            string w = await httpclient.GetStringAsync(u);
            httpclient.Dispose();
            return w;
        }

        public async Task<Card> downloadCard(int id)
        {
            Card c = new Card();
            Uri iuri = new Uri(uri + "card_search.action?ope=2&cid=" + id);
            var page = await this.getWebsiteStringAsync(iuri);

            c.Name = extractName(page);
            if (c.Name != "")
            {
                c.Attribute = extractElement(page, "<b>Attribute</b>");
                c.Icon = extractElement(page, "<b>Icon</b>");
                try
                {
                    c.Level = int.Parse(extractElement(page, "<b>Level</b>"));
                }
                catch { }
                try
                {
                    c.Rank = int.Parse(extractElement(page, "<b>Rank</b>"));
                }
                catch { }
                try
                {
                    c.PendulumScale = int.Parse(extractElement(page, "<b>Pendulum Scale</b>"));
                }
                catch { }
                c.PendulumEffect = extractElement(page, "<b>Pendulum Effect</b>");
                c.MonsterType = extractElement(page, "<b>Monster Type</b>");
                try
                {
                    c.ATK = int.Parse(extractElement(page, "<b>ATK</b>"));
                }
                catch { }
                try
                {
                    c.DEF = int.Parse(extractElement(page, "<b>DEF</b>"));
                }
                catch { }
                c.CardText = extractElement(page, "<b>Card Text</b>");
                c.ID = id;
            }
            else
            {
                c = null;
            }
            return c;
        }

        public async void downloadToArray()
        {
            Card[] trunk = new Card[12273];

            for (int i = 4007; i < 4057/*trunk.Length*/; i = i + 10)
            {
                var download1 = this.downloadCard(i);
                var download2 = this.downloadCard(i + 1);
                var download3 = this.downloadCard(i + 2);
                var download4 = this.downloadCard(i + 3);
                var download5 = this.downloadCard(i + 4);
                var download6 = this.downloadCard(i + 5);
                var download7 = this.downloadCard(i + 6);
                var download8 = this.downloadCard(i + 7);
                var download9 = this.downloadCard(i + 8);
                var download10 = this.downloadCard(i + 9);
                trunk[i] = await download1;
                trunk[i + 1] = await download2;
                trunk[i + 2] = await download3;
                trunk[i + 3] = await download4;
                trunk[i + 4] = await download5;
                trunk[i + 5] = await download6;
                trunk[i + 6] = await download7;
                trunk[i + 7] = await download8;
                trunk[i + 8] = await download9;
                trunk[i + 9] = await download10;
            }
            Console.WriteLine("Download Complete.");
            writeXml(trunk);
        }

        public void writeXml(Card[] trunk)
        {
            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.Indent = true;
            writerSettings.IndentChars = "\t";
            writerSettings.NewLineChars = "\n";

            XmlWriter writer = XmlWriter.Create("CardDB.xml", writerSettings);

            writer.WriteStartDocument();
            writer.WriteStartElement("CardDB");
            foreach (Card c in trunk)
            {
                if (c != null)
                {
                    var id = c.ID.ToString();
                    writer.WriteStartElement("Card", id);
                    writer.WriteElementString("Name", c.Name);
                    writer.WriteElementString("Attribute", c.Attribute);
                    writer.WriteElementString("Icon", c.Icon);
                    writer.WriteElementString("Level", c.Level.ToString());
                    writer.WriteElementString("Rank", c.Rank.ToString());
                    writer.WriteElementString("Pendulum_Scale", c.PendulumScale.ToString());
                    writer.WriteElementString("Pendulum_Effect", c.PendulumEffect);
                    writer.WriteElementString("Monster_Type", c.MonsterType);
                    writer.WriteElementString("ATK", c.ATK.ToString());
                    writer.WriteElementString("DEF", c.DEF.ToString());
                    writer.WriteElementString("Card_Text", c.CardText);
                    writer.WriteEndElement();

                }

            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            Console.WriteLine("Database Complete");
        }

        public Card[] readXml()
        {
            Card[] trunk = new Card[12273];
            XmlReader reader = XmlReader.Create("CardDB.xml");

            reader.MoveToContent();
            //reader.ReadToNextSibling("Card");
            Console.WriteLine(reader.LocalName);
            Console.WriteLine(reader.NamespaceURI);
            return trunk;
        }

        /// <summary>
        /// Initializes the object.
        /// </summary>
        public XmlHandler()
        {
        }
    }
}
