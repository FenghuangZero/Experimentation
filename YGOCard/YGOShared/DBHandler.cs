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
    class DBHandler
    {
        
        static Uri uri = new Uri("http://www.db.yugioh-card.com/yugiohdb/");
        // http://www.db.yugioh-card.com/yugiohdb/card_search.action?ope=2&cid=NUMBER
        // This is the address to list individual cards. Replace 'NUMBER' with any int from 4007 to 12272 inclusive
        // http://www.db.yugioh-card.com/yugiohdb/card_search.action?ope=1&sess=1&pid=NUMBER&rp=99999
        // This is the address for a card list. Replace the 'NUMBER' with an 8 digit id.
        // Starter decks use the id format 1330{series}00{set number}
        int openHttpClients;
            
#if WINDOWS_UWP
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
#endif
      
        /// <summary>
        /// Loads the database from a pre-existing Xml file.
        /// </summary>
        /// <param name="t">The object which will store the database while loaded.</param>
        /// <returns></returns>
        /*public async Task<List<Card>> loadXmlAsync(List<Card> t)
        {
#if WINDOWS_UWP
            StorageFile db = await localFolder.GetFileAsync("CardDB.xml");
            XDocument doc = XDocument.Load(await db.OpenStreamForReadAsync());
#elif CONSOLE
            var doc = XDocument.Load("CardDB.xml");
#endif
            var dbCardDB = doc.Elements();

            foreach (var dbc in dbCardDB.Elements())
            {
                var i = int.Parse(dbc.FirstAttribute.Value);
                Card c = new Card();
                c.ID = i;
                c.Name = dbc.Descendants("Name").FirstOrDefault().Value;
                c.Attribute = dbc.Descendants("Attribute").FirstOrDefault().Value;
                c.Level = int.Parse(dbc.Descendants("Level").FirstOrDefault().Value);
                c.Rank = int.Parse(dbc.Descendants("Rank").FirstOrDefault().Value);
                c.PendulumScale = int.Parse(dbc.Descendants("Pendulum_Scale").FirstOrDefault().Value);
                c.PendulumEffect = dbc.Descendants("Pendulum_Effect").FirstOrDefault().Value;
                c.MonsterType = dbc.Descendants("Monster_Type").FirstOrDefault().Value;
                c.CardType = dbc.Descendants("Card_Type").FirstOrDefault().Value;
                c.ATK = int.Parse(dbc.Descendants("ATK").FirstOrDefault().Value);
                c.DEF = int.Parse(dbc.Descendants("DEF").FirstOrDefault().Value);
                c.CardText = dbc.Descendants("Card_Text").FirstOrDefault().Value;
                t.Add(c);
            }
            return t;
        }*/

        public List<Card> loadXml()
        {
            var t = new List<Card>();
            var doc = XDocument.Load("CardDB.xml");
            var dbCardDB = doc.Elements();

            foreach (var dbc in dbCardDB.Elements())
            {
                var i = int.Parse(dbc.FirstAttribute.Value);
                Card c = new Card();
                c.ID = i;
                c.Name = dbc.Descendants("Name").FirstOrDefault().Value;
                c.Attribute = dbc.Descendants("Attribute").FirstOrDefault().Value;
                c.Level = int.Parse(dbc.Descendants("Level").FirstOrDefault().Value);
                c.Rank = int.Parse(dbc.Descendants("Rank").FirstOrDefault().Value);
                c.PendulumScale = int.Parse(dbc.Descendants("Pendulum_Scale").FirstOrDefault().Value);
                c.PendulumEffect = dbc.Descendants("Pendulum_Effect").FirstOrDefault().Value;
                c.MonsterType = dbc.Descendants("Monster_Type").FirstOrDefault().Value;
                c.CardType = dbc.Descendants("Card_Type").FirstOrDefault().Value;
                c.ATK = int.Parse(dbc.Descendants("ATK").FirstOrDefault().Value);
                c.DEF = int.Parse(dbc.Descendants("DEF").FirstOrDefault().Value);
                c.CardText = dbc.Descendants("Card_Text").FirstOrDefault().Value;
                t.Add(c);
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
                    if (webOut.IndexOf("</div>") < webOut.IndexOf("<span"))
                    {
                        finish = webOut.IndexOf("</div>");
                        element = webOut.Substring(0, finish);
                        element = element.Trim();
                    }
                    else if (webOut.IndexOf("<span class=\"item_box_value\">") < webOut.IndexOf("</div>"))
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

                    if (el == "<b>Card Type</b>")
                    {
                        element = element.Replace("<!--", "");
                        element = element.Replace("-->", "");
                        element = element.Trim();
                        element = new string(element.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
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
            while (openHttpClients > 50)
            {
                await Task.Delay(50);
            }
            var httpclient = new HttpClient();
            openHttpClients++;
            httpclient.Timeout = TimeSpan.FromSeconds(20);
            var w = "";
            var id = u.Query;
            try
            {
                w = await httpclient.GetStringAsync(u);
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine(id + " failed to download. Retrying.");
                await Task.Delay(50);
                try
                {
                    w = await httpclient.GetStringAsync(u);
                }
                catch (TaskCanceledException e2)
                {
                    Debug.WriteLine(id + " failed to download. Retrying a second Time.");
                    await Task.Delay(50);
                    try
                    {
                        w = await httpclient.GetStringAsync(u);
                    }
                    catch (TaskCanceledException e3)
                    {
                        Debug.WriteLine(id + " failed to download. Skipping");
                    }
                }
            } 
            
            httpclient.Dispose();
            openHttpClients--;
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
            if (page != "")
                c.ID = id;
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
                c.CardType = extractElement(page, "<b>Card Type</b>");
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
            }
            return c;
        }

        /// <summary>
        /// Writes a Card list into an XML file.
        /// </summary>
        /// <param name="trunk">Array to be written.</param>
        public async void writeXml(List<Card> trunk)
        {
#if WINDOWS_UWP
            StorageFile db = await localFolder.CreateFileAsync("CardDB.xml", CreationCollisionOption.ReplaceExisting);
            var database = await db.OpenStreamForWriteAsync();
#elif CONSOLE
            var database = new FileStream("CardDB.xml", FileMode.Create);
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
                    writer.WriteElementString("Card_Type", c.CardType);
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


        public async Task<List<Card>> downloadtoList(List<Card> t, int s, int e)
        {
            var c = new Card();
            var l = new List<Task<Card>>();
            var dummy = new List<int>();
            dummy = await loadDummyCardList();

            for (var i = s; i <= e; i++)
            {
                if (t.Exists(x => x.ID == i) != true && dummy.Exists(x => x == i) != true)
                {
                    l.Add(downloadCard(i));
                    await Task.Delay(50);
                }                    
            }

            foreach (var i in l)
            {
                c = await i;
                if (c.Name != "" && c.Name != null)
                    t.Add(c);
                else
                {
                    if (c.ID != 0)
                        dummy.Add(c.ID);
                }
                    
            }
            addToDummyCardList(dummy);
            return t;
        }

        private async void addToDummyCardList(List<int> dummy)
        {
            var name = "DummyCardList.txt";
            var recipie = new FileStream(name, FileMode.Open);
            var writer = new StreamWriter(recipie);
            dummy.Sort();
            foreach (var i in dummy)
            {
                await writer.WriteAsync(i.ToString());
                await writer.WriteAsync(',');
            }
            writer.Flush();
            writer.Close();
            recipie.Close();
        }

        private async Task<List<int>> loadDummyCardList()
        {
            var list = new FileStream("DummyCardList.txt", FileMode.Open);
            var reader = new StreamReader(list);
            var read = (await reader.ReadToEndAsync()).Split(',');
            var dummy = new List<int>();
            foreach (var s in read)
                if (s != "" && s != null)
                    dummy.Add(int.Parse(s));
            reader.Close();
            list.Close();
            return dummy;

        }

        /// <summary>
        /// Initializes the object.
        /// </summary>
        public DBHandler()
        {
            openHttpClients = 0;
        }
    }
}
