using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DormitorySensor
{
    public static class SensorList
    {
        private static readonly string cstPath = Path.Combine(
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName,
            "SensorList.xml");

        static SensorList()
        {
            ListSensors = new ObservableCollection<Sensor>();
            ListTickOfSensors = new ObservableCollection<Sensor>();
        }

        public static ObservableCollection<Sensor> ListSensors { get; }
        public static ObservableCollection<Sensor> ListTickOfSensors { get; }

        public static void AddSensor(string name, Guid sensorId, int value, sensorType type, string description, (double latitude, double longtitude) location, (double min, double max) acceptableValues)
        {
            Sensor s = new Sensor(name, sensorId, value, type, description, location, acceptableValues);
            ListSensors.Add(s);
            if (s.TickOf)
            {
                ListTickOfSensors.Add(s);
            }
        }

        public static void Remove(Sensor sensor)
        {
            ListSensors.Remove(sensor);
        }

        public static void Modify(string name, Guid sensorId, sensorType type, string description, (double latitude, double longtitude) location, (double min, double max) acceptableValues)
        {
            var sensorToModify = ListSensors.Where(item => item.SensorId == sensorId).FirstOrDefault();
            var tickOfBeforeModify = sensorToModify.TickOf;
            sensorToModify.Name = name;
            sensorToModify.Type = type;
            sensorToModify.Description = description;
            sensorToModify.Location = location;
            sensorToModify.AcceptableValues = acceptableValues;

            sensorToModify.TickOf = sensorToModify.IsValueOutOfRange(sensorToModify.Value, acceptableValues);
            ModifyListTickOf(tickOfBeforeModify, sensorToModify);
        }

        private static void ModifyListTickOf(bool tickOfBeforeModify, Sensor sensorToModify)
        {
            if (!tickOfBeforeModify && sensorToModify.TickOf)
            {
                ListTickOfSensors.Add(sensorToModify);
            }
            else if (tickOfBeforeModify && !sensorToModify.TickOf)
            {
                ListTickOfSensors.Remove(sensorToModify);
            }
        }

        public static void SaveSensorListToXmlFile()
        {
            try
            {
                XDocument doc = new XDocument();
                XElement root = new XElement("SensorList");
                foreach (var sensors in ListSensors)
                {
                    XElement sensor =
                        new XElement("Sensor",
                            new XAttribute("Name", sensors.Name),
                            new XAttribute("SensorId", sensors.SensorId),
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
            catch { }
        }

        public static void LoadXmlFile()
        {
            try
            {
                if (File.Exists(cstPath))
                {
                    var sensors = XDocument.Load(cstPath).Root.Elements("Sensor");
                    foreach (var sensorload in sensors)
                    {
                        var name = sensorload.Attribute("Name").Value;
                        var sensorId = Guid.Parse(sensorload.Attribute("SensorId").Value);
                        var value = int.Parse(sensorload.Attribute("Value").Value);
                        var type = (sensorType)Enum.Parse(typeof(sensorType), sensorload.Attribute("Type").Value);
                        var desc = sensorload.Attribute("Description").Value;
                        var location = (Double.Parse(sensorload.Element("Location").Attribute("Latitude").Value),
                                        Double.Parse(sensorload.Element("Location").Attribute("Longtitude").Value));
                        var acceptableValues = (Double.Parse(sensorload.Element("AcceptableValues").Attribute("MinValue").Value),
                                                Double.Parse(sensorload.Element("AcceptableValues").Attribute("MaxValue").Value));
                        AddSensor(name, sensorId, value, type, desc, location, acceptableValues);
                    }
                }
            }
            catch { }
        }

        public async static void RefreshSensors()
        {
            foreach (var sensor in ListSensors.ToList())
            {
                var tickOfBeforeModify = sensor.TickOf;
                sensor.Value = await SensorProcessor.LoadSensorInfo(sensor.SensorId.ToString(), sensor.Type.ToDescriptionString().ToLower());
                sensor.TickOf = sensor.IsValueOutOfRange(sensor.Value, sensor.AcceptableValues);
                ModifyListTickOf(tickOfBeforeModify, sensor);
            }
        }

    }
}
