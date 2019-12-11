using DormitorySensor;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsPresentation;
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
            
        }

        public void InitMap()
        {
            //GoogleMap gMap = new GoogleMap(map);
            //GMaps.Instance.Mode = AccessMode.ServerAndCache;
            //map.CanDragMap = true;
            //map.Zoom = 5;
            //map.MaxZoom = 18;
            //map.MinZoom = 0;

            //double lat = 23;
            //double lng = 23;
            //map.MapProvider = GMapProviders.GoogleMap;
            //map.Position = new PointLatLng(lat, lng);

            //PointLatLng point = new PointLatLng(lat, lng);
            //GMap.NET.WindowsForms.Markers.GMarkerGoogle marker1 = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(new PointLatLng(-25.966688, 32.580528),
            //                                           GMap.NET.WindowsForms.Markers.GMarkerGoogleType.green);


            ////GMapMarker marker = new GMapMarker(point);
            ////map.Markers.Add(marker);

            ////GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("marker");
            ////map.Over
        }

        public void InitViewAllList(DataGrid dataGrid)
        {
            SensorList.AddSensor("asd", "asd", SensorType.humidity, 20, 20); 
            SensorList.AddSensor("asd", "asasdsd", SensorType.humidity, 20, 20); 
            dataGrid.ItemsSource = SensorList.ListSensors;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddModifySensorWindow AddSensorWindow = new AddModifySensorWindow();
            AddSensorWindow.ShowDialog();
        }

        
        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            AddModifySensorWindow ModifySensorWindow = new AddModifySensorWindow();
            var sensorToModify = (Sensor)dataGrid.SelectedItem;
            ModifySensorWindow.Name1.Text = sensorToModify.Name;
            ModifySensorWindow.Name2.Text = sensorToModify.Description;
            //TODO
            ModifySensorWindow.Name3.Text = sensorToModify.Description;
            ModifySensorWindow.Name4.Text = sensorToModify.Description;
            ModifySensorWindow.Name5.Text = sensorToModify.Description;
            ModifySensorWindow.Name6.Text = sensorToModify.Description;
            ModifySensorWindow.Name7.Text = sensorToModify.Description;
            ModifySensorWindow.Name8.Text = sensorToModify.Description;
            ModifySensorWindow.Name9.Text = sensorToModify.Description;
            ModifySensorWindow.Name0.Text = sensorToModify.Description;
            ModifySensorWindow.ShowDialog();

        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var sensorToRemove = (Sensor)dataGrid.SelectedItem;
            SensorList.ListSensors.Remove(sensorToRemove);

            dataGrid.Items.Refresh();
        }
    }
}
