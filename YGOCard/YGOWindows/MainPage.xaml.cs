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
using YGOShared;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace YGOWindows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<Card> trunk = new List<Card>();
        DBHandler db = new DBHandler();
        Double downloadProgress = new Double();
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            DBDownloadProgress.Visibility = Visibility.Visible;
            var download = db.downloadtoList(trunk, 4000, 4400);
            for (Double i = 0; i < 100; i = downloadProgress)
            {
                updateProgressBar();
                await System.Threading.Tasks.Task.Delay(100);
            }

            trunk = await download;

            db.writeXml(trunk);
        }

        private void updateProgressBar()
        {
            downloadProgress = db.progress();
            DBDownloadProgress.Value = downloadProgress;
        }
        
    }
}
