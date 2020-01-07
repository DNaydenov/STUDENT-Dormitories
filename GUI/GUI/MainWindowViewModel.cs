//using DormitorySensor;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GUI
//{
//    public  class MainWindowViewModel
//    {
//        // INotifyPropertyChanged
//        private ObservableCollection<Sensor> _listSensors = new ObservableCollection<Sensor>();
//        public  ObservableCollection<Sensor> ListSensors
//        {
//            get { return _listSensors; }
//            set { _listSensors = value; }
//        }

//        public MainWindowViewModel()
//        {
//            SensorList.AddSensor("asd", "asd", SensorType.humidity, 20, 20,new Tuple<double, double>(10,50)) ;
//            SensorList.AddSensor("asd", "asasdsd", SensorType.humidity, 20, 20, new Tuple<double, double>(10, 50));
//        }

//        public void Load()
//        {
//            AddSensor("asd", "asd", SensorType.humidity, 20, 20, new Tuple<double, double>(10, 50));
//           // AddSensor("asd2", "asasdsd", SensorType.humidity, 20, 20);
//        }

//        public void AddSensor(string name, string description, SensorType type, double latitude, double longtitute, Tuple<double, double> tuple)
//        {
//            Sensor s = new Sensor(name, description, type, latitude, longtitute, tuple);
//            ListSensors.Add(s);
//        }

//    }
//}
