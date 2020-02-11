using System;
using System.ComponentModel;

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
        public Sensor() : this("", Guid.NewGuid(), 0, sensorType.ElPowerConsumption, "", (0, 0), (0, 0))//TEST FOR DATA BINDING
        {

        }
        #endregion

        #region Props
        public bool TickOf { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public Guid SensorId { get; set; }

        public int Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        public sensorType Type { get; set; }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public (double latitude, double longtitude) Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }

        public (double min, double max) AcceptableValues
        {
            get { return acceptableValues; }
            set
            {
                acceptableValues = value.min > value.max ? (value.max, value.min) : (value.min, value.max);
                OnPropertyChanged("AcceptableValues");
            }
        }
        #endregion

        internal bool IsValueOutOfRange(int value, (double min, double max) AcceptableValues) =>
            (value >= acceptableValues.min && value <= acceptableValues.max) ? false : true;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
