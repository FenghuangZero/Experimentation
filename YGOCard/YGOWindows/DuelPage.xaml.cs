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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using YGOShared;
using YGOWindows;


namespace YGOCardGame
{
    /// <summary>
    /// A page which displays the current status of a duel in progress.
    /// </summary>
    public sealed partial class DuelPage : Page
    {
        /// <summary>
        /// Runs a demonstration
        /// </summary>
        public void demo()
        {
            var awr = new AppwideResources();
            var gameOn = new Duel(awr.trunk);
        }

        /// <summary>
        /// Initialises the page
        /// </summary>
        public DuelPage()
        {
            this.InitializeComponent();
            demo();
            //StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            //Uri assetsFolder = new Uri(localFolder.ToString());
            //BitmapImage back = new BitmapImage();
            // image1.Source =;
        }
    }
}
