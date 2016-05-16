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
using YGOWindows;


namespace YGOCardGame
{
    /// <summary>
    /// The main menu page of the App.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Initializes the page.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }
        
        private void campaign_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DuelPage));
        }


        private void deckEditor_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DeckManager));
        }

        private void multiplayer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void options_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
