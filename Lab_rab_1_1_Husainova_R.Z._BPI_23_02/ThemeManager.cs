using System;
using System.Windows;

namespace Lab_rab_1_1_Husainova_R.Z._BPI_23_02
{
    public static class ThemeManager
    {
        public static void ApplyTheme(string themeName)
        {
            var dictionaries = Application.Current.Resources.MergedDictionaries;
            dictionaries.Clear();

            try
            {
                dictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri("Themes/CommonStyle.xaml", UriKind.Relative)
                });

                var uri = new Uri($"Themes/{themeName}.xaml", UriKind.Relative);
                dictionaries.Add(new ResourceDictionary { Source = uri });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить тему: {themeName}\n{ex.Message}", "Ошибка темы",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}