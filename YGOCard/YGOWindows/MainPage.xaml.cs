using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YGOCardGame;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace YGOWindows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Card[] trunk = new Card[130];
        Player player1 = new Player("Player 1");
        Player player2 = new Player("Player 2");
        XmlHandler cardDB = new XmlHandler();

        public MainPage()
        {
            this.InitializeComponent();           

            cardDB.loadXml(trunk);

            player2.Deck[0] = trunk[1];
            player1.Deck[0] = trunk[5];

            /*
            //Mock Duel
            player1.draw();
            player1.summon(player1.Hand, 0);
            player1.attackDirectly(player1.MonsterZone, 0, player2);
            player2.draw();
            player2.summon(player2.Hand, 0);
            player2.attackMonster(player2.MonsterZone, 0, player1, player1.MonsterZone, 0);
            //Console.WriteLine("Player 1 has drawn Exodia. Player 1 Wins.");
            */

            // Keep the console window open in debug mode.
            //Console.WriteLine("Press any key to exit.");
            //Console.ReadKey();
        }

        private void deckButton_Click(object sender, RoutedEventArgs e)
        {
            player1.draw();
            p1hand1textBlock.Text = player1.Hand[0].Name;
        }
    }
}
