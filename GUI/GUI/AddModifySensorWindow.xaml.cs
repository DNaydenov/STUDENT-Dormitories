using DormitorySensor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace GUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddModifySensorWindow : Window
    {
        Sensor sensor = null;

        public AddModifySensorWindow(string name, Sensor sensor = null)
        {
            InitializeComponent();
            Title = name;
            ActivateButton(name);
            if (sensor != null)
            {
                this.sensor = sensor;
            }
            

        }

        private void ActivateButton(string name)
        {
            if (confirmAdd.Name.Contains(name))
            {
                confirmAdd.Visibility = Visibility.Visible;
            }
            else if (confirmModify.Name.Contains(name))
            {
                confirmModify.Visibility = Visibility.Visible;
            }
            else
            {
                Debug.Assert(true, "invalid button");
            }
        }

        private void ConfirmAdd_Click(object sender, RoutedEventArgs e)
        {
            //call API and get value 
            SensorList.AddSensor(txtName.Text, txtDescription.Text, SensorType.noiseSensor, 0, 0, new Tuple<double, double>(Double.Parse(Name6.Text), Double.Parse(Name7.Text)));

           
            //ClearUserInput();

        }

        private void ClearAddData_Click(object sender, RoutedEventArgs e)
        {
            ClearUserInput();
        }



        private void ClearUserInput()
        {
            Name0.Clear();
            txtName.Clear();
            txtDescription.Clear();
            txtType.Clear();
            txtLatitude.Clear();
            Name5.Clear();
            Name6.Clear();
            Name7.Clear();
            Name9.Clear();
        }

        private void ConfirmModify_Click(object sender, RoutedEventArgs e)
        {
            SensorList.Modify(sensor.Id, txtName.Text, txtDescription.Text, SensorType.noiseSensor, 0, 0,
                new Tuple<double, double>(Double.Parse(Name6.Text), Double.Parse(Name7.Text)));

        }

    }
}
