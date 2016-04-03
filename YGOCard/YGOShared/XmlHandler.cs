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

        HttpClient website = new HttpClient();
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
            XDocument doc = XDocument.Load("YGOCardDB1.xml");

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
                t[i].Type = dbType.ElementAt(i).Value;
                t[i].Attack = int.Parse(dbAttack.ElementAt(i).Value);
                t[i].Defence = int.Parse(dbDefence.ElementAt(i).Value);
                t[i].Text = dbText.ElementAt(i).Value;
            }
            return t;
        }

        /// <summary>
        /// Downloads a web page that details a card at the given index number.
        /// </summary>
        public async void downloadDB()
        {
            XDocument doc = new XDocument(new XElement("CardDB"));
            
            for (int i = 4007; i < 4060; i++)
            {
                var name = "";
                Uri iUri = new Uri(uri + "card_search.action?ope=2&cid=" + i);
                var download = this.getWebsiteStringAsync(iUri);

                var webOut = await download;

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
                catch (ArgumentOutOfRangeException)
                {
                    //When a webpage displays no card, skip.
                    continue;
                }
                //this.addElements(webOut, name, i);
            }

            //doc.Save("CardDB.xml");
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
                    {
                        finish = webOut.IndexOf("<div");
                    }
                    else
                    {
                        finish = webOut.IndexOf("</div>");
                    }
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
        /// An asyncronous task to download the string of a website.
        /// </summary>
        /// <param name="u">The Uri of the website.</param>
        /// <returns></returns>
        public async Task<string> getWebsiteStringAsync(Uri u)
        {
            string w = await website.GetStringAsync(u);
            return w;
        }

        /*public void addElements(string webOut, string name, int i)
        {
            var attribute = "";
            var icon = "";
            var level = "";
            var rank = "";
            var monsterType = "";
            var attack = "";
            var defence = "";
            var cardText = "";

            attribute = extractElement(webOut, "<b>Attribute</b>");
            icon = extractElement(webOut, "<b>Icon</b>");
            level = extractElement(webOut, "<b>Level</b>");
            rank = extractElement(webOut, "<b>Rank</b>");
            monsterType = extractElement(webOut, "<b>Monster Type</b>");
            attack = extractElement(webOut, "<b>ATK</b>");
            attack = extractElement(webOut, "<b>ATK</b>");
            defence = extractElement(webOut, "<b>DEF</b>");
            cardText = extractElement(webOut, "<b>Card Text</b>");

            doc.Root.Add(new XElement("id_" + i, new XElement("Name", name),
                new XElement("Icon", icon),
                new XElement("Attribute", attribute),
                new XElement("Level", level),
                new XElement("Rank", rank),
                new XElement("Monster_Type", monsterType),
                new XElement("ATK", attack),
                new XElement("DEF", defence),
                new XElement("Card_Text", cardText)));
        }*/

        /// <summary>
        /// Initializes the object.
        /// </summary>
        public XmlHandler()
        {
            website.BaseAddress = uri;
        }
    }
}
