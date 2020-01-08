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

        public MainWindowViewModel()
        {
            SensorList.AddSensor("asd", "asd", sensorType.humidity, 20, 20);
            SensorList.AddSensor("asd", "asasdsd", sensorType.humidity, 20, 20);
        }

        public void Load()
        {
            AddSensor("asd", "asd", sensorType.humidity, 20, 20);
           // AddSensor("asd2", "asasdsd", SensorType.humidity, 20, 20);
        }

        public void AddSensor(string name, string description, sensorType type, double latitude, double longtitute)
        {
            Sensor s = new Sensor(name, description, type, latitude, longtitute);
            ListSensors.Add(s);
        }

//    }
//}
