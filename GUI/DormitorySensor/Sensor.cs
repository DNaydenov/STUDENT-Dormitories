using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySensor
{
    public enum sensorType
    {
        Temperature,
        Humidity,
        ElPowerConsumption,
        WindowOrDoorSensor, // if possible another representation, ex. bool windowSensor (true/false)
        Noise,
        None
    }
    public class Sensor
    {
        #region Members
        private string name;
        private string description;
        private sensorType type;
        private (double latitude, double lonsgtitude) location;
       // private double value;
        private (double min, double max) acceptableValues;
        private bool tickOf;
       

        private static int ID=0;

        #endregion

        #region Ctors

        public Sensor(string name, string description, int value, sensorType type, (double latitude, double longtitude) location, (double min, double max) acceptableValues)
        {
            Name = name;
            Description = description;
            Value = value;
            Type = type;
            Location = location;
           // AcceptableValues = acceptableValues;
            Id = ID++;
            
        }
        //public Sensor(Sensor sens) : this(sens.name, sens.description, sens.type, sens.latitude, sens.longtitude, sens.acceptableValues)
        //{

        //}
        //public Sensor() : this(null, null, 0, 0.0, 0.0, null)//don't sure have to exist
        //{

        //}
        #endregion

        #region Props
        public Guid SensorId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Value { get; set; }
        public string ValueType{ get; set; }

        public int Id { get; }

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

        public (double latitude, double longtitude) Location
        {
            get { return location; }
            set { location = value; }
        }

        public (double min, double max) AcceptableValues
        {
              get { return (acceptableValues.min, acceptableValues.max); }
            set
            {
                if (value.min< value.max)
                {
                    acceptableValues = (value.min, value.max);
                }
                else
                {
                    acceptableValues = (value.max, value.min);

                }
            }
        }

        #endregion

        //public void PrintSensorInfo()
        //{
        //    Console.WriteLine("Name: {0} Description: {1} Type: {2} Latitude: {3} Longtitude: {4}", name, description, type,location);
        //}

        //public void PrintAll(List<Sensor> sensors)
        //{
        //    foreach (var item in sensors)
        //    {
        //        PrintSensorInfo();
        //    }
        //}
        //public Sensor ModifySensor(string name, string description, sensorType type, double latitude, double longtitute)
        //{
        //    //not ready 
        //    return null;
        //}
    }
}
