using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1.Helpers
{
    public static class ComboBoxBackgroundHelper
    {


        public static Brush GetBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BackgroundProperty);
        }

        public static void SetBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(BackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for Background.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(ComboBoxBackgroundHelper), new PropertyMetadata(null, BackgroundChanged));

        private static void BackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ComboBox cb)
                return;

            cb.Loaded -= CmdLoaded;
            cb.Loaded += CmdLoaded;
            if(cb.IsLoaded)
                Apply(cb);

        }


        private static void CmdLoaded(object sender, RoutedEventArgs e)
        {
            if(sender is ComboBox cb)
                Apply(cb);

        }

        private static void Apply(ComboBox cb)
        {
            if (GetBackground(cb) is Brush brush)
                cb.SetBackground(brush);   // 기존 ComboBoxExtension 재사용
        }


    }
}
