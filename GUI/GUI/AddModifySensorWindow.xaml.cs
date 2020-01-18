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
            if (btnConfirmAdd.Name.Contains(name))
            {
                btnConfirmAdd.Visibility = Visibility.Visible;
            }
            else if (btnConfirmModify.Name.Contains(name))
            {
                btnConfirmModify.Visibility = Visibility.Visible;
            }
            else
            {
                Debug.Assert(true, "invalid button");
            }
        }

        private async void ConfirmAdd_Click(object sender, RoutedEventArgs e)
        {
            //call API and get value 
            var guid = Guid.NewGuid();
            var type = GetSensorType(CBoxType.SelectedIndex);
            var sensorValue = await SensorProcessor.LoadSensorInfo(guid.ToString(),type.ToString() );
            SensorList.AddSensor(txtName.Text, txtDescription.Text, sensorValue, type, (0, 0),
                (Double.Parse(txtMinValue.Text), Double.Parse(txtMaxValue.Text)));
            


        }

        private void ClearAddData_Click(object sender, RoutedEventArgs e)
        {
            ClearUserInput();
        }

        private void ClearUserInput()
        {
            txtName.Clear();
            txtDescription.Clear();
            txtLatitude.Clear();
            txtLongtitude.Clear();
            txtMinValue.Clear();
            txtMaxValue.Clear();
        }

        private void ConfirmModify_Click(object sender, RoutedEventArgs e)
        {
            Enum.TryParse(CBoxType.SelectedItem.ToString(), out sensorType myStatus);
            SensorList.Modify(sensor.Id, txtName.Text, txtDescription.Text, myStatus, (0, 0),
            (Double.Parse(txtMinValue.Text), Double.Parse(txtMaxValue.Text)));
        }

        private sensorType GetSensorType(int index)
        {
            sensorType sensorType;
            switch (index)
            {
                case 0:
                    sensorType = sensorType.Temperature;
                    break;
                case 1:
                    sensorType = sensorType.Humidity;
                    break;

                case 2:
                    sensorType = sensorType.ElPowerConsumption;
                    break;

                case 3:
                    sensorType = sensorType.WindowOrDoorSensor;
                    break;

                case 4:
                    sensorType = sensorType.Noise;
                    break;

                default:
                    sensorType = sensorType.None;
                    break;
            }
            return sensorType;
        }
    }
}
