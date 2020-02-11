using Newtonsoft.Json;
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
        public static async Task<int> LoadSensorInfo(string ID, string sensorType)
        {

            string url = string.Format("api/sensor?sensorId={0}&sensorType={1}", ID, sensorType);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<dynamic>();
                    return Convert.ToInt32(result["value"]);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
