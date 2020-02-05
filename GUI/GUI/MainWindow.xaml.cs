using DormitorySensor;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;
using System.ComponentModel;
using System.Threading;
using System.Data;
using GUI.Graphics;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static System.Timers.Timer timer;

        public MainWindow()
        {
            InitializeComponent();
            InitMap();
            //Http client is initialized at the start of our app
            ApiHelper.InitializeClient();
            dataGrid.ItemsSource = SensorList.ListSensors;
            reportGrid.ItemsSource = SensorList.ListTickOfSensors;
            SensorList.LoadXmlFile();

            foreach (var location in SensorList.ListSensors.Select(x => new Location(x.Location.latitude, x.Location.longtitude)))
            {
                AddPushpinToMap(location);
            }
        }
        public void InitMap()
        {
            BingMap.ZoomLevel = 5;
            //Sofia location
            BingMap.Center = new Location(42.698334, 23.319941);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new System.Timers.Timer(10000);
            timer.Elapsed += SensorList.RefreshSensors;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            timer.Enabled = false;
            SensorList.SaveSensorListToXmlFile();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddModifySensorWindow AddSensorWindow = new AddModifySensorWindow((sender as Button).Content.ToString());
            AddSensorWindow.ShowDialog();
            var pinLocation = SensorList.ListSensors.Last().Location;
            AddPushpinToMap(new Location(pinLocation.latitude, pinLocation.latitude));
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            var sensorToModify = (Sensor)dataGrid.SelectedItem;
            AddModifySensorWindow ModifySensorWindow = new AddModifySensorWindow((sender as Button).Content.ToString(), sensorToModify);
            LoadComboBoxItems(ModifySensorWindow.CBoxType);

            ModifySensorWindow.txtName.Text = sensorToModify.Name;
            ModifySensorWindow.CBoxType.SelectedValue = Enum.GetName(typeof(sensorType), sensorToModify.Type);
            ModifySensorWindow.txtDescription.Text = sensorToModify.Description;
            ModifySensorWindow.txtLatitude.Text = sensorToModify.Location.latitude.ToString();
            ModifySensorWindow.txtLongtitude.Text = sensorToModify.Location.longtitude.ToString();
            ModifySensorWindow.txtMinValue.Text = sensorToModify.AcceptableValues.min.ToString();
            ModifySensorWindow.txtMaxValue.Text = sensorToModify.AcceptableValues.max.ToString();

            var oldLocation = new Location(sensorToModify.Location.latitude, sensorToModify.Location.longtitude);
            ModifySensorWindow.ShowDialog();
            var newLocation = new Location(sensorToModify.Location.latitude, sensorToModify.Location.longtitude);
            //dataGrid.ItemsSource = null;
            if (!oldLocation.Equals(newLocation))
            {
                RelocatePin(oldLocation, newLocation);
            }
            //dataGrid.Items.Refresh();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            SensorList.Remove((Sensor)dataGrid.SelectedItem);
        }

        private void AddPushpinToMap(Location location)
        {
            OurPushpin newPin = new OurPushpin();
            newPin.SensorId = Guid.NewGuid();
            newPin.MouseDoubleClick += NewPin_MouseDoubleClick;
            newPin.Location = location;
            BingMap.Children.Add(newPin);
        }

        private void NewPin_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var test = (sender as OurPushpin).SensorId;
        }

        private void RelocatePin(Location oldLocation, Location newLocation)
        {
            var oldPin = BingMap.Children.OfType<Pushpin>()
                .Where(x => x.Location.Latitude == oldLocation.Latitude && x.Location.Longitude == oldLocation.Longitude).FirstOrDefault();
            BingMap.Children.Remove(oldPin);
            AddPushpinToMap(newLocation);
        }

        public static void LoadComboBoxItems(ComboBox cbo)
        {
            cbo.ItemsSource = Enum.GetValues(typeof(sensorType))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();
            cbo.DisplayMemberPath = "Description";
            cbo.SelectedValuePath = "value";
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var selectedSensor = (Sensor)dataGrid.SelectedItem;
            if(selectedSensor.Type == sensorType.Noise)
            {
                NoiseGraphicalRepresentation noiseGraphics = new NoiseGraphicalRepresentation(selectedSensor);
                noiseGraphics.ShowDialog();
            }
            else if(selectedSensor.Type == sensorType.Window)
            {
                if(selectedSensor.Value == 1)
                {
                    MessageBox.Show("The senosor indicates that the window is opened");
                }
                else
                {
                    MessageBox.Show("The senosor indicates that the window is closed");
                }
            }
            else if (selectedSensor.Type == sensorType.Humidity)
            {
                HumidityGraphicalRepresentation humidityGraphics = new HumidityGraphicalRepresentation(selectedSensor);
                humidityGraphics.ShowDialog();
            }
            else if (selectedSensor.Type == sensorType.ElPowerConsumption)
            {
                PowerConsumptionGraphicalRepresentation pwrGraphics = new PowerConsumptionGraphicalRepresentation(selectedSensor);
                pwrGraphics.ShowDialog();
            }
            else
            {
                TempreratureGraphicalRepresentation temperatureGraphics = new TempreratureGraphicalRepresentation(selectedSensor);
                temperatureGraphics.ShowDialog();
            }
        }
    }

}
