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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace YGOCardGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DuelPage : Page
    {
        Card[] trunk = new Card[12273];
        DBHandler cardDB = new DBHandler();
        public async void demo()
        {

            await cardDB.loadXml(trunk);
            var gameOn = new Duel(trunk);
        }
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
