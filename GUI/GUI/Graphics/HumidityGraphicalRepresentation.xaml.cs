using System.Windows;
using DormitorySensor;

namespace GUI
{
    /// <summary>
    /// Interaction logic for HumidityGraphicalRepresentation.xaml
    /// </summary>
    public partial class HumidityGraphicalRepresentation : Window
    {
        public HumidityGraphicalRepresentation(Sensor sensor)
        {
            InitializeComponent();
            marker.DataContext = sensor;
        }
    }
}
