using System;
using System.Collections.Generic;
using System.Text;

namespace YGOShared
{
    /// <summary>
    /// Provides platform specific behaviour for displaying messages to the user.
    /// </summary>
    class Debug
    {
        /// <summary>
        /// Displays a string.
        /// </summary>
        /// <param name="s">The string to be displayed.</param>
        public static async void WriteLine(string s)
        {
#if CONSOLE
            Console.WriteLine(s);
#elif WINDOWS_UWP
            var messageDialog = new Windows.UI.Popups.MessageDialog(s);
            await messageDialog.ShowAsync();
#endif
        }

        /// <summary>
        /// Displays a string. Replaces "{0}" in the string with a "toString()" of Object a.
        /// </summary>
        /// <param name="s">The string to be displayed.</param>
        /// <param name="a">An object whose "toString" result will display in place of "{0}" in the string.</param>
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

        /// <summary>
        /// Displays a string. Replaces "{0}" in the string with a "toString()" of Object a, and likewise for incrementing numbers up to "{1}".
        /// </summary>
        /// <param name="s">The string to be displayed.</param>
        /// <param name="a">An object whose "toString" result will display in place of "{0}" in the string.</param>
        /// <param name="b">An object whose "toString" result will display in place of "{1}" in the string.</param>
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

        /// <summary>
        /// Displays a string. Replaces "{0}" in the string with a "toString()" of Object a, and likewise for incrementing numbers up to "{2}".
        /// </summary>
        /// <param name="s">The string to be displayed.</param>
        /// <param name="a">An object whose "toString" result will display in place of "{0}" in the string.</param>
        /// <param name="b">An object whose "toString" result will display in place of "{1}" in the string.</param>
        /// <param name="c">An object whose "toString" result will display in place of "{2}" in the string.</param>
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

        /// <summary>
        /// Displays a string. Replaces "{0}" in the string with a "toString()" of Object a, and likewise for incrementing numbers up to "{3}".
        /// </summary>
        /// <param name="s">The string to be displayed.</param>
        /// <param name="a">An object whose "toString" result will display in place of "{0}" in the string.</param>
        /// <param name="b">An object whose "toString" result will display in place of "{1}" in the string.</param>
        /// <param name="c">An object whose "toString" result will display in place of "{2}" in the string.</param>
        /// <param name="d">An object whose "toString" result will display in place of "{3}" in the string.</param>
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

        /// <summary>
        /// Initializes the class.
        /// </summary>
        public Debug()
        { }
    }
}
