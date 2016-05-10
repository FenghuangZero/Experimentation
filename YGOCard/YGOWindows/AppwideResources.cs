using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGOShared;

namespace YGOWindows
{
    class AppwideResources
    {
        private static DBHandler cardDB = new DBHandler();
        private List<Card> Trunk = new List<Card>(cardDB.loadXml());
        public List<Card> trunk { get { return Trunk; } }

        public AppwideResources()
        { }
    }
}
