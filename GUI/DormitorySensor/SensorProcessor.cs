using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySensor
{
    public class SensorProcessor
    {
        public static async Task<Sensor> LoadSensorInfo()
        {
            string url = "api/sensor?sensorId=b549c8fb-4538-4cf7-9d8f-39ac27b27f25&sensorType=temperature";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    //var jsonResponse = await response.Content.ReadAsStringAsync();
                    Sensor result = await response.Content.ReadAsAsync<Sensor>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
