using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Core.Behavior
{
    public class ButtonBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Click += AssociatedObjectClick;
        }


        private void AssociatedObjectClick(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(AssociatedObject);
            if (window is null)
                return;

            var localPoint = AssociatedObject.IsMouseOver ? Mouse.GetPosition(AssociatedObject) : new Point(0, AssociatedObject.ActualHeight);

            var screenPoint = AssociatedObject.PointToScreen(localPoint);
            // 물리 픽셀 → 논리 픽셀 (DPI 보정)
            var source = PresentationSource.FromVisual(window);
            if (source?.CompositionTarget is not null)
                screenPoint = source.CompositionTarget.TransformFromDevice.Transform(screenPoint);

            SystemCommands.ShowSystemMenu(window, screenPoint);

        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObjectClick;

            base.OnDetaching();
        }
    }
}
