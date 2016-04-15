using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace YGOShared
{
    class DeckBuilder
    {
        private int?[][] Recipie = new int?[3][]
        {
            new int?[60],
            new int?[15],
            new int?[15]
        };
        
        public void addCardtoDeck(Card c)
        {
            int i = Array.IndexOf(Recipie[0], null);
            Recipie[0][i] = c.ID;
        }

        public void addCardtoExtra(Card c)
        {
            int i = Array.IndexOf(Recipie[1], null);
            Recipie[1][i] = c.ID;
        }

        public void addCardtoSide(Card c)
        {
            int i = Array.IndexOf(Recipie[2], null);
            Recipie[1][i] = c.ID;
        }

        public void removeCardfromDeck(Card c)
        {
            int i = Array.IndexOf(Recipie[0], c.ID);
            Recipie[0][i] = null;
        }

        public void removeCardfromExtra(Card c)
        {
            int i = Array.IndexOf(Recipie[1], c.ID);
            Recipie[0][i] = null;
        }

        public void removeCardfromSide(Card c)
        {
            int i = Array.IndexOf(Recipie[2], c.ID);
            Recipie[0][i] = null;
        }

        public void emptyRecipie()
        {
            Array.Clear(Recipie[0], 0, Recipie[0].Length);
            Array.Clear(Recipie[1], 0, Recipie[1].Length);
            Array.Clear(Recipie[2], 0, Recipie[1].Length);
        }

        public void loadKaiba()
        {
            Recipie[0][0] = 4007;
            Recipie[0][1] = 4009;
            Recipie[0][2] = 4011;
            Recipie[0][3] = 4029;
            Recipie[0][4] = 4032;
            Recipie[0][5] = 4037;
            Recipie[0][6] = 4039;
            Recipie[0][7] = 4050;
            Recipie[0][8] = 4072;
            Recipie[0][9] = 4086;
            Recipie[0][10] = 4096;
            Recipie[0][11] = 4097;
            Recipie[0][12] = 4200;
            Recipie[0][13] = 4239;
            Recipie[0][14] = 4247;
            Recipie[0][15] = 4254;
            Recipie[0][16] = 4273;
            Recipie[0][17] = 4287;
            Recipie[0][18] = 4309;
            Recipie[0][19] = 4330;
            Recipie[0][20] = 4342;
            Recipie[0][21] = 4352;
            Recipie[0][22] = 4383;
            Recipie[0][23] = 4384;
            Recipie[0][24] = 4385;
            Recipie[0][25] = 4388;
            Recipie[0][26] = 4460;
            Recipie[0][27] = 4472;
            Recipie[0][28] = 4508;
            Recipie[0][29] = 4603;
            Recipie[0][30] = 4835;
            Recipie[0][31] = 4839;
            Recipie[0][32] = 4843;
            Recipie[0][33] = 4842;
            Recipie[0][34] = 4848;
            Recipie[0][35] = 4849;
            Recipie[0][36] = 4852;
            Recipie[0][37] = 4865;
            Recipie[0][38] = 4747;
            Recipie[0][39] = 4810;
            Recipie[0][40] = 4172;
            Recipie[0][41] = 4230;
            Recipie[0][42] = 4339;
            Recipie[0][43] = 4547;
            Recipie[0][44] = 4695;
            Recipie[0][45] = 4838;
            Recipie[0][46] = 4850;
            Recipie[0][47] = 4851;
            saveRecipie("kaiba");
            var recipie = new FileStream("Kaiba.txt", FileMode.Open);
            var reader = new StreamReader(recipie);
            
            var read = (reader.ReadLine()).Split(',');
            int[] buffer = new int[read.Length];
            for(var i = 0; i < buffer.Length; i++)
            {
                buffer[i] = int.Parse(read[i]);
            }
        }

        public void saveRecipie(string name)
        {
            name = name + ".txt";
            var recipie = new FileStream(name, FileMode.Create);
            var writer = new StreamWriter(recipie);

            foreach (var i in Recipie[0])
            {
                if (i != null)
                {
                    writer.Write(i.Value);
                    writer.Write(',');
                }
            }
            writer.WriteLine();
            foreach (var i in Recipie[1])
            {
                if (i != null)
                {
                    writer.Write(i.Value);
                    writer.Write(',');
                }
            }
            writer.WriteLine();
            foreach (var i in Recipie[2])
            {
                if (i != null)
                {
                    writer.Write(i.Value);
                    writer.Write(',');
                }
            }
            writer.Flush();
        }

        public void loadDeck(Player p, Card[] t)
        {
            for (var i = 0; i < 60; i++)
            {
                if (Recipie[0][i] != null)
                    p.Deck[i] = t[Recipie[0][i].Value];
            }
            for (var i = 0; i < 15; i++)
            {
                if (Recipie[1][i] != null)
                    p.ExtraDeck[i] = t[Recipie[1][i].Value];
            }
            for (var i = 0; i < 15; i++)
            {
                if (Recipie[2][i] != null)
                    p.SideDeck[i] = t[Recipie[2][i].Value];
            }
        }

        public DeckBuilder()
        { }
    }
}
