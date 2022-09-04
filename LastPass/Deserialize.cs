using LastPass.Data;
using System.Text.Json;

namespace LastPass
{
    public class Deserialize
    {
        public List<Model> GetDeserializeJson(string jsonString)
        {
            List<Model>? userPasswords = JsonSerializer.Deserialize<List<Model>>(jsonString);
            return userPasswords;
        }
    }
}
