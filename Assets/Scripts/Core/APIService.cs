using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace uichat.core
{
    public class APIService
    {
        public static T Fetch<T>(string path)
        {
            // TODO: ideally this would be an async http call and include error handling
            TextAsset raw = Resources.Load<TextAsset>(path);
            
            return JsonConvert.DeserializeObject<Payload<T>>(raw.text).Data;
        }
    }

    [Serializable]
    public class Payload<T>
    {
        public T Data;
    }
}


