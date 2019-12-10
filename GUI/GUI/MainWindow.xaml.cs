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
            List<dynamic> it = new List<dynamic>();
            it.Add(new { no = 1, name = "sensor1", desc = "asdf", curValue = 23, graphics = "some URL1" });
            it.Add(new { no = 2, name = "sensor2", desc = "asdf", curValue = 24, graphics = "some URL2" });
            it.Add(new { no = 3, name = "sensor3", desc = "asdf", curValue = 25, graphics = "some URL3" });
            it.Add(new { no = 4, name = "sensor4", desc = "asdf", curValue = 26, graphics = "some URL4" });
            it.Add(new { no = 5, name = "sensor5", desc = "asdf", curValue = 27, graphics = "some URL5" });
            it.Add(new { no = 6, name = "sensor6", desc = "asdf", curValue = 28, graphics = "some URL6" });
            it.Add(new { no = 7, name = "sensor5", desc = "asdf", curValue = 27, graphics = "some URL5" });
            it.Add(new { no = 8, name = "sensor6", desc = "asdf", curValue = 28, graphics = "some URL6" });
            it.Add(new { no = 9, name = "sensor5", desc = "asdf", curValue = 27, graphics = "some URL5" });
            it.Add(new { no = 0, name = "sensor6", desc = "asdf", curValue = 28, graphics = "some URL6" });
            dataGrid.ItemsSource = it;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddSensorWindow addSensorWindow = new AddSensorWindow();
            addSensorWindow.ShowDialog();
        }
    }
}
