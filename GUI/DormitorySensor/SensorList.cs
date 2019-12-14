using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySensor
{
    public static class  SensorList
    {
        // consider using MVVM
        // consider using ObservableCollection 
        private static List<Sensor> listSensors;

        static SensorList()
        {
            ListSensors = new List<Sensor>();
        }

        public static List<Sensor> ListSensors
        {
            get { return listSensors; }
            set { listSensors = value; }
        }

       
        public static void AddSensor(string name, string description, SensorType type, double latitude, double longtitute)
        {
            Sensor s = new Sensor(name, description, type, latitude, longtitute);
            listSensors.Add(s);
        }

        // BONUS!!!
        public static void Remove(int index)
        {
            listSensors.RemoveAt(index - 1);
        }

        //
        public static void Modify(int index, string name, string description, SensorType type, double latitude, double longtitute)
        {
            //TODO
        }
    }
}
