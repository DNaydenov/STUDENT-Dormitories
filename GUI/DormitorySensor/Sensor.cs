using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySensor
{
    public enum sensorType
    {
        [Description("Temperature")]
        Temperature,
        [Description("Humidity")]
        Humidity,
        [Description("Electric power consumption ")]
        ElPowerConsumption,
        [Description("Window/door ")]
        WindowOrDoorSensor, // if possible another representation, ex. bool windowSensor (true/false)
        [Description("Noise")]
        Noise
    }

    public static class SensorTypeEnumExtensions
    {
        public static string ToDescriptionString(this sensorType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
    public class Sensor
    {
        #region Members
        private sensorType type;
        private (double latitude, double lonsgtitude) location;
        private (double min, double max) acceptableValues;
        private bool tickOf;
       
        #endregion

        #region Ctors

        public Sensor(string name, Guid sensorId,  int value, sensorType type, string description, (double latitude, double longtitude) location, (double min, double max) acceptableValues)
        {
            Name = name;
            SensorId = sensorId;
            Value = value;
            Type = type;
            Description = description;
            Location = location;
            AcceptableValues = acceptableValues;
        }
        //public Sensor(Sensor sens) : this(sens.name, sens.description, sens.type, sens.latitude, sens.longtitude, sens.acceptableValues)
        //{

        //}
        //public Sensor() : this(null, null, 0, 0.0, 0.0, null)//don't sure have to exist
        //{

        //}
        #endregion

        #region Props
        public string Name { get; set; }

        public Guid SensorId { get; set; }

        public int Value { get; set; }

        public sensorType Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Description { get; set; }

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
    }
}
