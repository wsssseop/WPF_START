using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Core.Behavior
{
    public partial class WindowBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += AssociatedObjectMouseLeftButtonDown;
        }

        private void AssociatedObjectMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                AssociatedObject.DragMove();
            }

        }


        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= AssociatedObjectMouseLeftButtonDown;
            base.OnDetaching();
        }
    }
}
