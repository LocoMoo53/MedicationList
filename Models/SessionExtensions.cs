/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using Microsoft.AspNetCore.Http; // add using directive for HTTP
using Newtonsoft.Json; // add using directive for JSON


namespace MedicationList.Models
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value) // method for setting object in session
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key) // method for getting object in session
        {
            var value = session.GetString(key);
            return (string.IsNullOrEmpty(value)) ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}