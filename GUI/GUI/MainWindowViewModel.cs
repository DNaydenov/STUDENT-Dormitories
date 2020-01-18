////using DormitorySensor;
//using DormitorySensor;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GUI
//{
//    public class MainWindowViewModel
//    {
//        // INotifyPropertyChanged
//        private ObservableCollection<Sensor> _listSensors = new ObservableCollection<Sensor>();
        

//        public ObservableCollection<Sensor> ListSensors
//        {
//            get { return _listSensors; }
//            set { _listSensors = value; }
//        }

//        public MainWindowViewModel()
//        {
//            //SensorList.AddSensor("asd", "asd", sensorType.humidity, 20, 20, 29, 29);
//            //SensorList.AddSensor("asd", "asasdsd", sensorType.humidity, 20, 200,299,300);
//        }

//        public void Load()
//        {
//              //AddSensor("Name1","This is info about the senosor",sensorType.humidity,299.4,1008.5, new Tuple<double, double>(Double.Parse(Name6.Text), Double.Parse(Name7.Text)));
//              //AddSensor("asd2", "asasdsd", sensorType.humidity, 20, 20, new Tuple<double, double>(Double.Parse(Name6.Text), Double.Parse(Name7.Text)));
//        }

//        public void AddSensor(string name, string description, sensorType type, (double latitude, double longtitude) location, (double min, double max) acceptableValues)
//        {
//            Sensor s = new Sensor(name, description, type, location, acceptableValues);
//            ListSensors.Add(s);
//        }
//    }
//}
