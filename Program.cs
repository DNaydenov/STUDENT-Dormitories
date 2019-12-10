﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySensors
{
    class Program
    {
        static void Main(string[] args)
        {
            var sensors = new List<Sensor>();
            Sensor sensor1 = new Sensor("SensorNumber1", "This is a sensor in 57a block", SensorType.elPowerConsumption, 256.4, 333.2);
            Sensor sensor2 = new Sensor("SensorNumber2", "This is a sensor in 57b block", SensorType.temperature, 255.4, 323.2);
            sensors.Add(sensor1);
            sensors.Add(sensor2);
            sensor1.ModifySensor("SensorNumber2", "This is a sensor in 57b block", SensorType.temperature, 255.4, 323.222222222222);
            sensor1.PrintSensorInfo();



        }

       
    }
}
