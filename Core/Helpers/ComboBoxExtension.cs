using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Core.Helpers
{
    public static class ComboBoxExtension
    {
        public static void SetBackground(this ComboBox comboBox, Brush brush)
        {
            if (comboBox.Template.FindName("toggleButton", comboBox) is not ToggleButton toggleButton)
                return;
            if (toggleButton.Template.FindName("templateRoot", toggleButton) is not Border border)
                return;

            border.Background = brush;


            if (comboBox.Template.FindName("PART_EditableTextBox", comboBox) is not TextBox tb)
                return;
            var parent = (Border)tb.Parent;
            parent.Background = brush;

        }

    }
}
