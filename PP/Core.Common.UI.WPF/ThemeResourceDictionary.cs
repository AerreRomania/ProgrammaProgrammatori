using System.Windows;

namespace Core.Common.UI.WPF
{
    public class ThemeResourceDictionary : ResourceDictionary
    {
        public ThemeResourceDictionary()
        {
            MergedDictionaries.Add(Theme.ResourceDictionary);
        }
    }
}
