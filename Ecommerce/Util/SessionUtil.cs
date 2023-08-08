namespace Ecommerce.Util {
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System.IO;



    public static class SessionExtensions {
        public static void SetObject<T>(this ISession session, string key, T value) {
            var jsonValue = JsonConvert.SerializeObject(value);
            session.SetString(key, jsonValue);
        }

        public static T GetObject<T>(this ISession session, string key) {
            var jsonValue = session.GetString(key);
            return jsonValue == null ? default : JsonConvert.DeserializeObject<T>(jsonValue);
        }
    }

}
