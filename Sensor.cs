using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySensors
{
    public enum SensorType
    {
        temperature,
        humidity,
        elPowerConsumption,
        windowSensor, // if possible another representation, ex. bool windowSensor (true/false)
        noiseSensor
    }
    public class Sensor
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private SensorType type;

        public SensorType Type
        {
            get { return type; }
            set { type = value; }
        }
        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        private double longtitude;

        public double MyProperty
        {
            get { return longtitude; }
            set { longtitude = value; }
        }

        public Sensor(string _name, string _description, SensorType _type, double _latitude,double _longtitute)
        {
            name = _name;
            description = _description;
            type = _type;
            latitude = _latitude;
            longtitude = _longtitute;
        }
        public Sensor(Sensor sens) : this(sens.name, sens.description, sens.type, sens.latitude, sens.longtitude)
        {

        }
        public Sensor():this(null,null,0,0.0,0.0)
        {

        }
        public void PrintSensorInfo()
        {
            Console.WriteLine("Name: {0} Description: {1} Type: {2} Latitude: {3} Longtitude: {4}", name, description, type, latitude, longtitude);
        }

        public void PrintAll(List<Sensor> sensors)
        {
            foreach (var item in sensors)
            {
                PrintSensorInfo();
            }
        }
        public Sensor ModifySensor(string name, string description, SensorType type, double latitude, double longtitute)
        {   
           //not ready 
        }
    }
}
