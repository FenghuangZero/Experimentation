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


namespace YGOCardGame
{
    /// <summary>
    /// A page on which the player can build a deck.
    /// </summary>
    public sealed partial class DeckManager : Page
    {
        /// <summary>
        /// Initialises the page.
        /// </summary>
        public DeckManager()
        {
            this.InitializeComponent();
        }
    }
}
