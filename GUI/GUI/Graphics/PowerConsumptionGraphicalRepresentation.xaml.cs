using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
