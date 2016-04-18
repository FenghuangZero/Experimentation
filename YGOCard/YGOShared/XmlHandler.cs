using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net.Http;
#if WINDOWS_UWP
using Windows.Storage;
using Windows.ApplicationModel;
#endif


namespace YGOShared
{
    /// <summary>
    /// Creates an object to control the database of cards.
    /// </summary>
    class XmlHandler
    {
        
        Uri uri = new Uri("http://www.db.yugioh-card.com/yugiohdb/");
        // http://www.db.yugioh-card.com/yugiohdb/card_search.action?ope=2&cid=NUMBER
        // This is the address to list individual cards. Replace 'NUMBER' with any int from 4007 to 12272 inclusive
        // http://www.db.yugioh-card.com/yugiohdb/card_search.action?ope=1&sess=1&pid=NUMBER&rp=99999
        // This is the address for a card list. Replace the 'NUMBER' with an 8 digit id.
        // Starter decks use the id format 

#if WINDOWS_UWP
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
#endif
      
        /// <summary>
        /// Loads the database from a pre-existing Xml file.
        /// </summary>
        /// <param name="t">The object which will store the database while loaded.</param>
        /// <returns></returns>
        public async Task<Card[]> loadXml(Card[] t)
        {
#if WINDOWS_UWP
            StorageFile db = await localFolder.GetFileAsync("CardDB.xml");
            XDocument doc = XDocument.Load(await db.OpenStreamForReadAsync());
#elif CONSOLE
            var doc = XDocument.Load("CardDB.xml");
#endif

            var dbCard = doc.Descendants("Card");
            var dbName = doc.Descendants("Name");
            var dbAttribute = doc.Descendants("Attribute");
            var dbLevel = doc.Descendants("Level");
            var dbRank = doc.Descendants("Rank");
            var dbPenScale = doc.Descendants("Pendulum_Scale");
            var dbPenEffect = doc.Descendants("Pendulum_Effect");
            var dbMonType = doc.Descendants("Monster_Type");
            var dbAttack = doc.Descendants("ATK");
            var dbDefence = doc.Descendants("DEF");
            var dbText = doc.Descendants("Card_Text");
            
            // Load cards from XML
            for (var i = 0; i < dbCard.Count(); i++)
            {
                var index = int.Parse(dbCard.ElementAt(i).FirstAttribute.Value);
                t[index] = new Card();
                t[index].ID = index;
                t[index].Name = dbName.ElementAt(i).Value;
                t[index].Attribute = dbAttribute.ElementAt(i).Value;
                t[index].Level = int.Parse(dbLevel.ElementAt(i).Value);
                t[index].Rank = int.Parse(dbRank.ElementAt(i).Value);
                t[index].PendulumScale = int.Parse(dbPenScale.ElementAt(i).Value);
                t[index].PendulumEffect = dbPenEffect.ElementAt(i).Value;
                t[index].MonsterType = dbMonType.ElementAt(i).Value;
                t[index].ATK = int.Parse(dbAttack.ElementAt(i).Value);
                t[index].DEF = int.Parse(dbDefence.ElementAt(i).Value);
                t[index].CardText = dbText.ElementAt(i).Value;
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
        /// Queries website.
        /// </summary>
        /// <param name="u">Uri of the website.</param>
        /// <returns></returns>
        public async Task<string> getWebsiteStringAsync(Uri u)
        {
            var httpclient = new HttpClient();
            string w = await httpclient.GetStringAsync(u);
            httpclient.Dispose();
            return w;
        }

        /// <summary>
        /// Asyncronously downloads a single webpage of a card and extracts the elements into a Card object.
        /// </summary>
        /// <param name="id">Identification number of the card.</param>
        /// <returns></returns>
        public async Task<Card> downloadCard(int id)
        {
            var c = new Card();
            var iuri = new Uri(uri + "card_search.action?ope=2&cid=" + id);
            var page = await getWebsiteStringAsync(iuri);

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

        /// <summary>
        /// Begins a series of download tasks to put cards into an array.
        /// </summary>
        public async void downloadToArray()
        {
            var trunk = new Card[12273];

            for (var i = 4007; i < 4057/*trunk.Length*/; i = i + 10)
            {
                var download1 = downloadCard(i);
                var download2 = downloadCard(i + 1);
                var download3 = downloadCard(i + 2);
                var download4 = downloadCard(i + 3);
                var download5 = downloadCard(i + 4);
                var download6 = downloadCard(i + 5);
                var download7 = downloadCard(i + 6);
                var download8 = downloadCard(i + 7);
                var download9 = downloadCard(i + 8);
                var download10 = downloadCard(i + 9);
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
            Debug.WriteLine("Download Complete.");
            writeXml(trunk);
        }

        /// <summary>
        /// Writes a Card array into an XML file.
        /// </summary>
        /// <param name="trunk">Array to be written.</param>
        public async void writeXml(Card[] trunk)
        {
#if WINDOWS_UWP
            StorageFile db = await localFolder.CreateFileAsync("CardDB.xml", CreationCollisionOption.ReplaceExisting);
            var database = await db.OpenStreamForWriteAsync();
#elif CONSOLE
            var database = new FileStream("CardDB.xml", FileMode.OpenOrCreate);
#endif
            var writerSettings = new XmlWriterSettings();
            writerSettings.Indent = true;
            writerSettings.IndentChars = "\t";
            writerSettings.NewLineChars = "\n";

            var writer = XmlWriter.Create(database, writerSettings);

            writer.WriteStartDocument();
            writer.WriteStartElement("CardDB");
            foreach (var c in trunk)
            {
                if (c != null)
                {
                    writer.WriteStartElement("Card");
                    writer.WriteAttributeString("id", c.ID.ToString());
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
            Debug.WriteLine("Database Complete");
        }

        public async void downloadCardList(int id)
        {
            var iuri = new Uri(uri + "card_search.action?ope=1&sess=1&pid=" + id + "&rp=99999");
            var page = await getWebsiteStringAsync(iuri);
            var start = page.IndexOf("#card_image_0_1");
            var findLength = page.IndexOf("Total of");
            var findName = page.IndexOf("<li class=\"oneline\">");
            var finish = page.IndexOf("$('.list_style ul li')");

            var listLength = page.Substring(findLength + 8);
            listLength = listLength.Substring(0, listLength.IndexOf("cards"));
            listLength = listLength.Trim();

            var listName = page.Substring(findName + 20);
            listName = listName.Substring(0, listName.IndexOf("</li>"));
            page = page.Substring(start, finish);

            var cardArray = new int[int.Parse(listLength)];

            for (var i = 0; i < cardArray.Length; i++)
            {

                start = page.IndexOf("#card_image_"+ i + "_1");
                page = page.Substring(start);
                cardArray[i] = extractCardFromList(page);
            }
            writeRecipie(cardArray, listName);
        }

        public int extractCardFromList(string webOut)
        {
            var start = webOut.IndexOf("cid=");
            var finish = webOut.IndexOf("&ciid=1");
            webOut = webOut.Substring(start + 4, finish - start - 4);
            return int.Parse(webOut);
        }

        public void writeRecipie(int[] cards, string name)
        {
            name = name + ".txt";
            var recipie = new FileStream(name, FileMode.Create);
            var writer = new StreamWriter(recipie);
            foreach (var i in cards)
            {
                
                writer.Write(i);
                writer.Write(',');
            }
            writer.Flush();
        }

        /// <summary>
        /// Initializes the object.
        /// </summary>
        public XmlHandler()
        {
        }
    }
}
