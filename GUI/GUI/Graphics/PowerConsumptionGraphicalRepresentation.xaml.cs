using System.Windows;
using DormitorySensor;

namespace GUI
{
    /// <summary>
    /// Interaction logic for PowerConsumptionGraphicalRepresentation.xaml
    /// </summary>
    public partial class PowerConsumptionGraphicalRepresentation : Window
    {
        public PowerConsumptionGraphicalRepresentation(Sensor sensor)
        {
            InitializeComponent();
            needle.DataContext = sensor;
        }
    }
}
