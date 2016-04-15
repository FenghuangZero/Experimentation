using System;
using System.Collections.Generic;
using System.Text;

namespace YGOShared
{
    class Debug
    {
        public static async void WriteLine(string s)
        {
#if CONSOLE
            Console.WriteLine(s);
#elif WINDOWS_UWP
            var messageDialog = new Windows.UI.Popups.MessageDialog(s);
            await messageDialog.ShowAsync();
#endif
        }

        public static async void WriteLine(string s, Object a)
        {
#if CONSOLE
            Console.WriteLine(s, a);
#elif WINDOWS_UWP
            s = s.Replace("{0}", a.ToString());
            var messageDialog = new Windows.UI.Popups.MessageDialog(s);
            await messageDialog.ShowAsync();
#endif
        }

        public static async void WriteLine(string s, Object a, Object b)
        {
#if CONSOLE
            Console.WriteLine(s, a, b);
#elif WINDOWS_UWP
            s = s.Replace("{0}", a.ToString());
            s = s.Replace("{1}", b.ToString());
            var messageDialog = new Windows.UI.Popups.MessageDialog(s);
            await messageDialog.ShowAsync();
#endif
        }

        public static async void WriteLine(string s, Object a, Object b, Object c)
        {
#if CONSOLE
            Console.WriteLine(s, a, b, c);
#elif WINDOWS_UWP
            s = s.Replace("{0}", a.ToString());
            s = s.Replace("{1}", b.ToString());
            s = s.Replace("{2}", c.ToString());
            var messageDialog = new Windows.UI.Popups.MessageDialog(s);
            await messageDialog.ShowAsync();
#endif
        }

        public static async void WriteLine(string s, Object a, Object b, Object c, Object d)
        {
#if CONSOLE
            Console.WriteLine(s, a, b, c, d);
#elif WINDOWS_UWP
            s = s.Replace("{0}", a.ToString());
            s = s.Replace("{1}", b.ToString());
            s = s.Replace("{2}", c.ToString());
            s = s.Replace("{3}", d.ToString());
            var messageDialog = new Windows.UI.Popups.MessageDialog(s);
            await messageDialog.ShowAsync();
#endif
        }
        public Debug()
        { }
    }
}
