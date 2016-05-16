using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGOShared;

namespace YGOWindows
{
    public class AppwideResources
    {
        private static DBHandler cardDB = new DBHandler();
        private List<Card> Trunk = new List<Card>();
        public List<Card> trunk { get { return Trunk; } }

        public async void loadTrunkAsync()
        {
            Trunk = await cardDB.loadXmlAsync();
        }
        public AppwideResources()
        {}
    }
}
