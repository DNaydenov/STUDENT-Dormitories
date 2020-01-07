using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySensor
{
    public static class  SensorList
    {
        // consider using MVVM
        // consider using ObservableCollection 
        private static ObservableCollection<Sensor> listSensors = new ObservableCollection<Sensor>();

        static SensorList()
        {
            ListSensors = new ObservableCollection<Sensor>();
        }

        public static ObservableCollection<Sensor> ListSensors
        {
            get { return listSensors; }
            set { listSensors = value; }
        }

        public static void AddSensor(string name, string description, SensorType type, double latitude, double longtitute, Tuple<double,double> acceptableValues)
        {
            Sensor s = new Sensor(name, description, type, latitude, longtitute, acceptableValues);
            listSensors.Add(s);
        }

        public static void Remove(Sensor sensor)
        {
            listSensors.Remove(sensor);
        }

        public static void Modify(int id, string name, string description,
            SensorType type, double latitude, double longtitute, Tuple<double, double> acceptableValues)
        {
            var sensorToModify =listSensors.Where(item => item.Id == id).FirstOrDefault();
            sensorToModify.Name = name;
            sensorToModify.Description = description;
            sensorToModify.Type = type;
            sensorToModify.Latitude= latitude;
            sensorToModify.Longtitude= longtitute;
            sensorToModify.AcceptableValues = acceptableValues;
        }
    }
}
