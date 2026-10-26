using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1.Behavior
{
    public partial class WindowBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Closing += AssociatedObjectClosing;
            AssociatedObject.Closed += AssociatedObjectClosed;
        }

        private void AssociatedObjectClosing(object? sender, CancelEventArgs e)
        {

        }

        private void AssociatedObjectClosed(object? sender, EventArgs e)
        {

        }

        protected override void OnDetaching()
        {
            AssociatedObject.Closing -= AssociatedObjectClosing;
            AssociatedObject.Closed -= AssociatedObjectClosed;
            base.OnDetaching();
        }
    }
}
