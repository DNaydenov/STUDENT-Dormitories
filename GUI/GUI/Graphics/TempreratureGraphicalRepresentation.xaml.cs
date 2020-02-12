using DormitorySensor;
using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for TempreratureGraphicalRepresentation.xaml
    /// </summary>
    public partial class TempreratureGraphicalRepresentation : Window
    {
        public TempreratureGraphicalRepresentation(Sensor sensor)
        {
            InitializeComponent();
            linearBar.DataContext = sensor;
        }
    }
}
