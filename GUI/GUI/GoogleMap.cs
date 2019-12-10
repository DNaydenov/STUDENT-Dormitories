using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class GoogleMap : GMapMarker
    {
        public  GMapControl GMap { get; private set; }
        public GoogleMap(GMapControl map) : base(new GMap.NET.PointLatLng(23, 23))
        {
            GMap = map;
        }
        
        public void InitMap()
        {

            //GMaps.Instance.Mode = AccessMode.ServerAndCache;
            //MainWindow map.CanDragMap = true;
            //map.Zoom = 5;
            //map.MaxZoom = 18;
            //map.MinZoom = 0;

            //double lat = 23;
            //double lng = 23;
            //map.MapProvider = GMapProviders.GoogleMap;
            //map.Position = new PointLatLng(lat, lng);

            //PointLatLng point = new PointLatLng(lat, lng);
            //GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(-25.966688, 32.580528),
            //                                           GMarkerGoogleType.green);


            //GMapMarker marker = new GMapMarker(point);
            //markers.Markers.Add(marker);

            // map.Overlays.Add(markers);
        }
    }
}
