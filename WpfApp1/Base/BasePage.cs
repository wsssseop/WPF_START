using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WpfApp1.Animation;

namespace WpfApp1.Base
{
    public class BaseUserControl : UserControl
    {
        public ViewAnimation ViewLoadAnimation { get; set; } = ViewAnimation.SlideAndFadeInFromRight;
        public ViewAnimation ViewUnloadAnimation { get; set; } = ViewAnimation.SlideAndFadeOutToLeft;

        public float SlideSeconds { get; set; } = 0.8f;



        public BaseUserControl()
        {
            if(ViewLoadAnimation != ViewAnimation.None)
                this.Visibility = Visibility.Collapsed;


            this.Loaded += BasePageLoaded;
        }

        private async void BasePageLoaded(object sender, RoutedEventArgs e)
        {

            await AnimateIn();

        }

        private async Task AnimateIn()
        {
            if (ViewLoadAnimation == ViewAnimation.None)
                return;

            switch (ViewLoadAnimation)
            {
                case ViewAnimation.SlideAndFadeInFromRight:
                    var sb = new Storyboard();

                    double width = (VisualTreeHelper.GetParent(this) as FrameworkElement)?.ActualWidth
                                   ?? Window.GetWindow(this)?.ActualWidth
                                   ?? 0;


                    var slideAnimation = new ThicknessAnimation
                    {
                        Duration = new Duration(TimeSpan.FromSeconds(SlideSeconds)),
                        From = new Thickness(width, 0, -width, 0),
                        To = new Thickness(0),
                        DecelerationRatio = 0.9f

                    };
                    Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
                    sb.Children.Add(slideAnimation);

                    sb.Begin(this);

                    this.Visibility = Visibility.Visible;

                    await Task.Delay((int)(SlideSeconds * 1000));

                    break;


            }

        }
    }
}
