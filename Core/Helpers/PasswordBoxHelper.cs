using System.Windows;
using System.Windows.Controls;

namespace Core.Helpers
{
    public static class PasswordBoxHelper
    {
        // true로 설정하면 PasswordChanged를 구독해서 HasPassword를 갱신
        public static readonly DependencyProperty MonitorPasswordProperty =
            DependencyProperty.RegisterAttached("MonitorPassword", typeof(bool), typeof(PasswordBoxHelper),
                new PropertyMetadata(false, OnMonitorPasswordChanged));

        public static bool GetMonitorPassword(DependencyObject d) => (bool)d.GetValue(MonitorPasswordProperty);
        public static void SetMonitorPassword(DependencyObject d, bool value) => d.SetValue(MonitorPasswordProperty, value);

        // 템플릿 Trigger에서 사용
        public static readonly DependencyProperty HasPasswordProperty =
            DependencyProperty.RegisterAttached("HasPassword", typeof(bool), typeof(PasswordBoxHelper),
                new PropertyMetadata(false));

        public static bool GetHasPassword(DependencyObject d) => (bool)d.GetValue(HasPasswordProperty);
        public static void SetHasPassword(DependencyObject d, bool value) => d.SetValue(HasPasswordProperty, value);

        private static void OnMonitorPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not PasswordBox passwordBox)
                return;

            passwordBox.PasswordChanged -= OnPasswordChanged;

            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += OnPasswordChanged;
                SetHasPassword(passwordBox, passwordBox.Password.Length > 0);
            }
        }

        private static void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
                SetHasPassword(passwordBox, passwordBox.Password.Length > 0);
        }
    }
}
