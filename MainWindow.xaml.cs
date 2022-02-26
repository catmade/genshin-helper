using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace genshin_helper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ClickArea clickArea;

        public MainWindow()
        {
            this.clickArea = new ClickArea
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            clickArea.Show();

            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            clickArea.Close();
            base.OnClosing(e);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            clickArea.MousePassThrough = (bool) ((CheckBox) sender).IsChecked;
        }

        private void SliderOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = ((Slider) sender);
            clickArea.Opacity = slider.Value / slider.Maximum;
        }

        private void SliderRadius_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = (Slider) sender;
            clickArea.Radius = (int) slider.Value;
        }
    }
}