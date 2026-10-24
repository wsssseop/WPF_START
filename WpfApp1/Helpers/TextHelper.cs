using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp1.Helpers
{
    public class TextHelper
    {


        public static bool GetUseOnPropertyChanged(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseOnPropertyChangedProperty);
        }

        public static void SetUseOnPropertyChanged(DependencyObject obj, bool value)
        {
            obj.SetValue(UseOnPropertyChangedProperty, value);
        }

        // Using a DependencyProperty as the backing store for UseOnPropertyChanged.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseOnPropertyChangedProperty =
            DependencyProperty.RegisterAttached("UseOnPropertyChanged", typeof(bool), typeof(TextHelper), new PropertyMetadata(false, OnUseOnPropertyChanged));

        private static void OnUseOnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // null 및 type검사, 캐스팅, 할당 한번에 다 함.
            if(d is not TextBox tb)
                return;

            if (e.NewValue is bool)
            {
                tb.TextChanged += TextBox_TextChanged;
            }
            else
            {
                tb.TextChanged -= TextBox_TextChanged;
            }
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox tb)
            {
                return;
            }

            BindingExpression bindingExpression = tb.GetBindingExpression(TextBox.TextProperty);
            bindingExpression.UpdateSource();


        }





    }
}
