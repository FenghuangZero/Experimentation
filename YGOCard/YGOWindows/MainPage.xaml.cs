using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Net;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YGOShared;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace YGOWindows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Card[] trunk = new Card[12273];
        XmlHandler cardDB = new XmlHandler();
        
        /// <summary>
        /// Initializes the page.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        private async void campaign_Click(object sender, RoutedEventArgs e)
        {
            await cardDB.loadXml(trunk);
            var gameOn = new Duel(trunk);
        }


        private void deckEditor_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DeckBuilder));
        }

        private void multiplayer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void options_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
