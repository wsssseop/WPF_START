using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Helpers;

namespace WpfApp1.Behavior
{
    public class PasswordPlaceholderBehavior : Behavior<PasswordBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PasswordChanged += AssociatedObjectPasswordChanged;
            UpdateHasPassword();
        }

        private void AssociatedObjectPasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdateHasPassword();
        }

        // 템플릿 Trigger가 볼 수 있도록 PasswordBox에 HasPassword 설정
        private void UpdateHasPassword()
        {
            PasswordBoxHelper.SetHasPassword(AssociatedObject, AssociatedObject.Password.Length > 0);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged -= AssociatedObjectPasswordChanged;

            base.OnDetaching();
        }
    }
}
