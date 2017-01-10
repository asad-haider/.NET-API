using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebAPI
{
    public static class Serialization<Model>
    {
        public static Model Serialize(string jsonResponse)
        {
            return JsonConvert.DeserializeObject<Model>(jsonResponse);
        }

        public static string DeSerialize(Model model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
