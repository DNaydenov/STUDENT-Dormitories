using DormitorySensor;
using System;
using System.Diagnostics;
using System.Windows;

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
            // Check IDataErrorInfo interface
            // Check ValudateOnDataErrors, NotifyOnDataErroes on binding
            // Check set tooltip with style
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
                Debug.Assert(true, "Check Buttons Content");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.LoadComboBoxItems(CBoxType);
        }

        private async void ConfirmAdd_Click(object sender, RoutedEventArgs e)
        {
            var sensorId = Guid.NewGuid();
            sensorType type = (sensorType)Enum.Parse(typeof(sensorType), CBoxType.SelectedValue.ToString());
            var sensorValue = await SensorProcessor.LoadSensorInfo(sensorId.ToString(), type.ToDescriptionString().ToLower());

            SensorList.AddSensor(txtName.Text, sensorId, sensorValue, type, txtDescription.Text,
                (Double.Parse(txtLatitude.Text), Double.Parse(txtLongtitude.Text)),
                (Double.Parse(txtMinValue.Text), Double.Parse(txtMaxValue.Text)));
            Close();
        }

        private void ConfirmModify_Click(object sender, RoutedEventArgs e)
        {
            sensorType type = (sensorType)Enum.Parse(typeof(sensorType), CBoxType.SelectedValue.ToString());
            SensorList.Modify(txtName.Text, sensor.SensorId, type, txtDescription.Text,
                (Double.Parse(txtLatitude.Text), Double.Parse(txtLongtitude.Text)),
                (Double.Parse(txtMinValue.Text), Double.Parse(txtMaxValue.Text)));
            Close();
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
    }
}
