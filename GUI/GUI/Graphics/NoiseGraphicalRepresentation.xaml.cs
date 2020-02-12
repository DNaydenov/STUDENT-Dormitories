using DormitorySensor;
using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for GraphicalRepresentationWindow.xaml
    /// </summary>
    public partial class NoiseGraphicalRepresentation : Window
    {
        public NoiseGraphicalRepresentation(Sensor sensor)
        {
            InitializeComponent();
            needle.DataContext = sensor;
        }
    }
}
