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
        Card[] trunk = new Card[130];
        XmlHandler cardDB = new XmlHandler();
        
        
        /// <summary>
        /// Initializes the page.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            localSettings.Values["exampleSetting"] = "Hello Windows";
            Object value = localSettings.Values["exampleSetting"];
            ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue();
            composite["intVal"] = 1;
            composite["strVal"] = "string";

            localSettings.Values["exampleCompositeSetting"] = composite;
        }

        private void downDB_Click(object sender, RoutedEventArgs e)
        {
            cardDB.downloadToArray();
        }
    }
}
