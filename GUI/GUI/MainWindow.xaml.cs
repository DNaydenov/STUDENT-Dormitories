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

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitMap();
            //Http client is initialized at the start of our app
            ApiHelper.InitializeClient();
        }
        public void InitMap()
        {
            BingMap.ZoomLevel = 9;
            //Sofia location
            BingMap.Center = new Location(42.698334, 23.319941);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SensorList.LoadXmlFile();
            dataGrid.ItemsSource = SensorList.ListSensors;
            foreach(var location in SensorList.ListSensors.Select(x => new Location(x.Location.latitude,x.Location.longtitude)))
            {
                AddPushpinToMap(location);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
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
            ModifySensorWindow.txtDescription.Text = sensorToModify.Description;
            ModifySensorWindow.CBoxType.SelectedValue = Enum.GetName(typeof(sensorType), sensorToModify.Type);
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

        private void AddPushpinToMap(Location location)
        {
            Pushpin newPin = new Pushpin();
            newPin.Location = location;
            BingMap.Children.Add(newPin);
        }

        private void RelocatePin(Location oldLocation, Location newLocation)
        {
            var oldPin = BingMap.Children.OfType<Pushpin>()
                .Where(x => x.Location.Latitude == oldLocation.Latitude && x.Location.Longitude == oldLocation.Longitude).FirstOrDefault();
            BingMap.Children.Remove(oldPin);
            AddPushpinToMap(newLocation);
        }

        private async void loadSensorInfo_Click(object sender, RoutedEventArgs e)
        {
            //var sensorInfo = await SensorProcessor.LoadSensorInfo();
            //sensorDateText.Text = $"Sensor date {sensorInfo.timeStamp}";
            ////sensorTypeText.Text = $"Sensor type {sensorInfo.valueType}";
            //sensorTypeText.Text = $"Sensor value {sensorInfo.value}";
            ////sunsetText.Text = $"Sunset is at {sunInfo.measure_type_of_value}";
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
    }
}
