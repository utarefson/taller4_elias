using LastPass.Data;
using System.Text.Json;

namespace LastPass
{
    public class deserializar
    {
        public List<Model> DeserializeJson(string jsonString)
        {
            List<Model>? userPasswords = JsonSerializer.Deserialize<List<Model>>(jsonString);
            return userPasswords;
        }
    }
}
