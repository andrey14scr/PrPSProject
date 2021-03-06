using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PrPS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		private static List<CultureInfo> _languages = new List<CultureInfo>();

        public static List<CultureInfo> Languages
        {
            get { return _languages; }
        }

        public App()
        {
            App.LanguageChanged += App_LanguageChanged;

            _languages.Clear();
            _languages.Add(new CultureInfo("en-US"));
            _languages.Add(new CultureInfo("ru-RU"));
        }

		public static event EventHandler LanguageChanged;

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU":
                        dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Resources/lang.xaml", UriKind.Relative);
                        break;
                }

                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                    where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                    select d).First();
                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }

                LanguageChanged(Application.Current, new EventArgs());
            }
        }

        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            Language = PrPS.Properties.Settings.Default.DefaultLanguage;
        }

        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            PrPS.Properties.Settings.Default.DefaultLanguage = Language;
            PrPS.Properties.Settings.Default.Save();
        }
    }
}
