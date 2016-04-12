using System;
using System.Collections.Generic;
using System.Text;

namespace YGOShared
{
    class Debug
    {
        public static void WriteLine(string s)
        {
#if CONSOLE
            Console.WriteLine(s);
#elif WINDOWS_UWP
            var messageDialog = new Windows.UI.Popups.MessageDialog(s);
            messageDialog.ShowAsync();
#endif
        }

        public static void WriteLine(string s, Object a)
        {
#if CONSOLE
            Console.WriteLine(s, a);
#elif WINDOWS_UWP
            // var messageDialog = new Windows.UI.Popups.MessageDialog(s, a);
            // messageDialog.ShowAsync();
#endif
        }

        public static void WriteLine(string s, Object a, Object b)
        {
#if CONSOLE
            Console.WriteLine(s, a, b);
#elif WINDOWS_UWP
            // var messageDialog = new Windows.UI.Popups.MessageDialog(s, a, b);
            // messageDialog.ShowAsync();
#endif
        }

        public static void WriteLine(string s, Object a, Object b, Object c)
        {
#if CONSOLE
            Console.WriteLine(s, a, b, c);
#elif WINDOWS_UWP
            // var messageDialog = new Windows.UI.Popups.MessageDialog(s, a, b, c);
            // messageDialog.ShowAsync();
#endif
        }

        public static void WriteLine(string s, Object a, Object b, Object c, Object d)
        {
#if CONSOLE
            Console.WriteLine(s, a, b, c, d);
#elif WINDOWS_UWP
            // var messageDialog = new Windows.UI.Popups.MessageDialog(s, a, b, c, d);
            // messageDialog.ShowAsync();
#endif
        }
        public Debug()
        { }
    }
}
