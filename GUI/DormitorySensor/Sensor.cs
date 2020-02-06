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
        [Description("Electric power")]
        ElPowerConsumption,
        [Description("Window")]
        Window,
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
    public class Sensor : INotifyPropertyChanged
    {
        #region Members

        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        private int value;
        private string description;
        private (double latitude, double lonsgtitude) location;
        private (double min, double max) acceptableValues;

        #endregion

        #region Ctors

        public Sensor(string name, Guid sensorId, int value, sensorType type, string description, (double latitude, double longtitude) location, (double min, double max) acceptableValues)
        {
            Name = name;
            SensorId = sensorId;
            Value = value;
            Type = type;
            Description = description;
            Location = location;
            AcceptableValues = acceptableValues;
            TickOf = IsValueOutOfRange(value, acceptableValues);
        }
        //public Sensor(Sensor sens) : this(sens.name, sens.description, sens.type, sens.latitude, sens.longtitude, sens.acceptableValues)
        //{

        //}
        //public Sensor() : this(null, null, 0, 0.0, 0.0, null)//don't sure have to exist
        //{

        //}
        #endregion

        #region Props
        public bool TickOf { get; set; }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public Guid SensorId { get; set; }

        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        public sensorType Type { get; set; }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
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
                if (value.min < value.max)
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

        public bool IsValueOutOfRange(int value, (double min, double max) AcceptableValues)
        {
            return (value >= acceptableValues.min && value <= acceptableValues.max) ? false : true;
        }
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
