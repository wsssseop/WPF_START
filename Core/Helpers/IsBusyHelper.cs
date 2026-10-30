using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Core.Helpers
{
    public class IsBusyHelper
    {


        public static bool GetValue(DependencyObject obj)
        {
            return (bool)obj.GetValue(ValueProperty);
        }

        public static void SetValue(DependencyObject obj, bool value)
        {
            obj.SetValue(ValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value", typeof(bool), typeof(IsBusyHelper), new PropertyMetadata(false));


    }
}
