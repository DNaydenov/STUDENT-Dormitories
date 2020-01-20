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
            InitViewAllList(dataGrid);

            //this.DataContext = new MainWindowViewModel();

            //Http client is initialized at the start of our app
            ApiHelper.InitializeClient();
        }
        public void InitMap()
        {
            
            Pushpin pin1 = new Pushpin();
            pin1.Location = new Location(42.698334, 23.319941);
            BingMap.Children.Add(pin1);
            BingMap.ZoomLevel = 9;
            //Sofia location
            BingMap.Center = new Location(42.698334, 23.319941);
        }

        public void InitViewAllList(DataGrid dataGrid)
        {
           //SensorList.AddSensor("asd", "asd", sensorType.humidity, (20, 20), (10,40)); 
           //SensorList.AddSensor("asd", "asasdsd", sensorType.humidity, (20, 20), (10, 40)); 
           dataGrid.ItemsSource = SensorList.ListSensors;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //(this.DataContext as MainWindowViewModel).AddSensor("asd2", "asasdsd", sensorType.humidity, 20, 20, new Tuple<double, double>(0,50));
            AddModifySensorWindow AddSensorWindow = new AddModifySensorWindow((sender as Button).Content.ToString());
            AddSensorWindow.ShowDialog();
            var pinLocation = SensorList.ListSensors.Last().Location;
            Pushpin pin1 = new Pushpin();
            pin1.Location = new Location(pinLocation.latitude, pinLocation.longtitude);
            BingMap.Children.Add(pin1);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //(this.DataContext as MainWindowViewModel).Load();
        }

        private void RelocatePin(Location oldLocation, Location newLocation)
        {
            var oldPin = BingMap.Children.OfType<Pushpin>()
                .Where(x => x.Location.Latitude == oldLocation.Latitude && x.Location.Longitude == oldLocation.Longitude).FirstOrDefault();
            BingMap.Children.Remove(oldPin);

            Pushpin newPin = new Pushpin()
            {
                Location = newLocation
            };
            BingMap.Children.Add(newPin);
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
