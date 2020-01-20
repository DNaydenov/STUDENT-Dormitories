using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DormitorySensor
{
    public static class  SensorList
    {
        private static readonly string cstPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DormitorySensors\\SensorList.xml");

        static SensorList()
        {
            ListSensors = new ObservableCollection<Sensor>();
        }

        public static ObservableCollection<Sensor> ListSensors { get; }

        public static void AddSensor(string name, string description, int value, sensorType type, (double latitude, double longtitude) location, (double min, double max) acceptableValues)
        {
            Sensor s = new Sensor(name, description, value, type, location, acceptableValues);
            ListSensors.Add(s);
        }

        public static void Remove(Sensor sensor)
        {
            ListSensors.Remove(sensor);
        }

        public static void Modify(int id, string name, string description, sensorType type, (double latitude, double longtitude) location, (double min, double max) acceptableValues)
        {
            var sensorToModify = ListSensors.Where(item => item.Id == id).FirstOrDefault();
            sensorToModify.Name = name;
            sensorToModify.Description = description;
            sensorToModify.Type = type;
            sensorToModify.Location = location;
            sensorToModify.AcceptableValues = acceptableValues;
        }

        public static void SaveSensorListToXmlFile()
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("SensorList");
            foreach(var sensors in ListSensors)
            {
                XElement sensor =
                    new XElement("Sensor",
                        new XAttribute("Name", sensors.Name),
                        new XAttribute("Type", sensors.Type.ToString()),
                        new XAttribute("Value", sensors.Value),
                        new XAttribute("Description", sensors.Description),
                            new XElement("Location",
                                new XAttribute("Latitude", sensors.Location.latitude),
                                new XAttribute("Longtitude", sensors.Location.longtitude)),
                            new XElement("AcceptableValues",
                                new XAttribute("MinValue", sensors.AcceptableValues.min),
                                new XAttribute("MaxValue", sensors.AcceptableValues.max)));
                root.Add(sensor);
            }
            doc.Add(root);
            doc.Save(cstPath);
        }

        public static void LoadXmlFile()
        {
            var sensors = XDocument.Load(cstPath).Root.Elements("Sensor");
            foreach(var sensorload in sensors)
            {
                var name = sensorload.Attribute("Name").Value;
                var desc = sensorload.Attribute("Description").Value;
                var value = int.Parse(sensorload.Attribute("Value").Value);
                var type = (sensorType)Enum.Parse(typeof(sensorType), sensorload.Attribute("Type").Value);
                var location = (Double.Parse(sensorload.Element("Location").Attribute("Latitude").Value),
                                Double.Parse(sensorload.Element("Location").Attribute("Longtitude").Value));
                var acceptableValues = (Double.Parse(sensorload.Element("AcceptableValues").Attribute("MinValue").Value),
                                        Double.Parse(sensorload.Element("AcceptableValues").Attribute("MaxValue").Value));

                Sensor sensor = new Sensor(name, desc, value, type, location, acceptableValues);
                ListSensors.Add(sensor);
            }

        }
    }
}
