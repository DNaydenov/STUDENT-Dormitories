using DormitorySensor;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
using System.ComponentModel;
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
            //Http client is initialized at the start of our app
            ApiHelper.InitializeClient();
            dataGrid.ItemsSource = SensorList.ListSensors;
            reportGrid.ItemsSource = SensorList.ListTickOfSensors;
            SensorList.LoadXmlFile();

            InitMap();
            InitTimer(10000);
        }

        public void InitMap()
        {
            //Sofia location
            BingMap.Center = new Location(42.698334, 23.319941);
            BingMap.LayoutUpdated += BingMap_LayoutUpdated;

            foreach (var sensor in SensorList.ListSensors)
            {
                AddPushpinToMap(sensor);
            }
        }

        private void InitTimer(int interval)
        {
            timer = new System.Timers.Timer(interval);
            timer.Elapsed += RefreshSensorsWraper;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void BingMap_LayoutUpdated(object sender, EventArgs e)
        {
            if (BingMap.ZoomLevel < 3.3)
            {
                BingMap.ZoomLevel = 3.3;
            }
        }

        private void RefreshSensorsWraper(Object source, System.Timers.ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                SensorList.RefreshSensors();
            }));
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
            AddPushpinToMap(SensorList.ListSensors.Last());
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
            if (!oldLocation.Equals(newLocation))
            {
                RelocatePin(oldLocation, newLocation);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            SensorList.Remove((Sensor)dataGrid.SelectedItem);
        }

        private void AddPushpinToMap(Sensor sensor)
        {
            OurPushpin newPin = new OurPushpin();
            newPin.SensorId = sensor.SensorId;
            newPin.Location = new Location(sensor.Location.latitude, sensor.Location.longtitude);
            BingMap.Children.Add(newPin);
            newPin.MouseDoubleClick += NewPin_MouseDoubleClick;
        }

        private void NewPin_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var pushpinId = (sender as OurPushpin).SensorId;
            var sensor = SensorList.ListSensors.Where(x => x.SensorId == pushpinId).FirstOrDefault();
            ShowGraphics(sensor);
        }

        private void RelocatePin(Location oldLocation, Location newLocation)
        {
            var oldPin = BingMap.Children.OfType<Pushpin>()
                .Where(x => x.Location.Latitude == oldLocation.Latitude && x.Location.Longitude == oldLocation.Longitude).FirstOrDefault();
            oldPin.Location = newLocation;
        }

        public static void LoadComboBoxItems(ComboBox cbo)
        {
            cbo.ItemsSource = Enum.GetValues(typeof(sensorType))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()),
                    typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
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
            ShowGraphics(selectedSensor);
        }

        private void ShowGraphics(Sensor sensor)
        {
            switch (sensor.Type)
            {
                case sensorType.Temperature:
                    TempreratureGraphicalRepresentation temperatureGraphics = new TempreratureGraphicalRepresentation(sensor);
                    temperatureGraphics.ShowDialog();
                    break;
                case sensorType.Humidity:
                    HumidityGraphicalRepresentation humidityGraphics = new HumidityGraphicalRepresentation(sensor);
                    humidityGraphics.ShowDialog();
                    break;
                case sensorType.ElPowerConsumption:
                    PowerConsumptionGraphicalRepresentation pwrGraphics = new PowerConsumptionGraphicalRepresentation(sensor);
                    pwrGraphics.ShowDialog();
                    break;
                case sensorType.Window:
                    string message = "The sensor indicates that the window is ";
                    if (sensor.Value == 1)
                    {
                        MessageBox.Show(message + "opened");
                    }
                    else
                    {
                        MessageBox.Show(message + "closed");
                    }
                    break;
                case sensorType.Noise:
                    NoiseGraphicalRepresentation noiseGraphics = new NoiseGraphicalRepresentation(sensor);
                    noiseGraphics.ShowDialog();
                    break;
                default:
                    MessageBox.Show("Error! No such sensor type");
                    break;
            }
        }
    }
}
