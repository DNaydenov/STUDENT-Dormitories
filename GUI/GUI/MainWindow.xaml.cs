using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
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
            map.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            InitializeComponent();
            map.MapProvider = GMapProviders.GoogleMap;
            map.Position= new PointLatLng(23, 23);

            GMapOverlay oMarker = new GMapOverlay("marker1");
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(-25.966688, 32.580528),
            GMarkerGoogleType.green);
            oMarker.Markers.Add(marker);
            //map.OverLays
        }
    }
}
