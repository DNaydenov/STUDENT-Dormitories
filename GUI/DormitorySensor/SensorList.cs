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

        public static void AddSensor(string name, string description, int value, sensorType type, (double latitude, double longtitude) location, (double min, double max) acceptableValues)
        {
            Sensor s = new Sensor(name, description, value, type, location, acceptableValues);
            listSensors.Add(s);
        }

        public static void Remove(Sensor sensor)
        {
            listSensors.Remove(sensor);
        }


        public static void Modify(int id, string name, string description, sensorType type, (double latitude, double longtitude) location, (double min, double max) acceptableValues)
        {
            var sensorToModify = listSensors.Where(item => item.Id == id).FirstOrDefault();
            sensorToModify.Name = name;
            sensorToModify.Description = description;
            sensorToModify.Type = type;
            sensorToModify.Location = location;
            sensorToModify.AcceptableValues = acceptableValues;
        }
    }
}
