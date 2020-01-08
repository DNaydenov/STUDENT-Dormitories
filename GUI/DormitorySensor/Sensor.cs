using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySensor
{
    public enum sensorType
    {
        temperature,
        humidity,
        elPowerConsumption,
        windowSensor, // if possible another representation, ex. bool windowSensor (true/false)
        noiseSensor
    }
    public class Sensor
    {
        #region Members
        private string name;
        private string description;
        private sensorType type;
        private double latitude;
        private double longtitude;
       // private double value;
        private Tuple<double, double> acceptableValues;
        private bool tickOf;
       

        #endregion

        #region Ctors

        public Sensor(string name, string description, sensorType type, double latitude, double longtitute)
        {
            Name = name;
            Description = description;
            Type = type;
            Latitude = latitude;
            Longtitude = longtitute;

        }
        public Sensor(Sensor sens) : this(sens.name, sens.description, sens.type, sens.latitude, sens.longtitude)
        {

        }
        public Sensor() : this(null, null, 0, 0.0, 0.0)
        {

        }
        #endregion

        #region Props
        public Guid sensorId { get; set; }
        public DateTime timeStamp { get; set; }
        public string value { get; set; }
        public string valueType{ get; set; }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public sensorType Type
        {
            get { return type; }
            set { type = value; }
        }

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public double Longtitude
        {
            get { return longtitude; }
            set { longtitude = value; }
        }

        #endregion




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
        public Sensor ModifySensor(string name, string description, sensorType type, double latitude, double longtitute)
        {
            //not ready 
            return null;
        }
    }
}
