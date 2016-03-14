using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YGOCardGame
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
        public XmlHandler()
        { }
    }
}
