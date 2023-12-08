using System.IO;
using Newtonsoft.Json;
using Microsoft.Xna.Framework.Input;

namespace Arcadia.Input
{
    public class KeyBindingsReader
    {
        public static KeyBindings ReadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<KeyBindings>(json);
            }
            else
            {
                // Handle file not found or invalid JSON
                return new KeyBindings();
            }
        }
    }

    public class KeyBindings
    {
        public Keys Jump { get; set; } = Keys.Space;
        public Keys MoveLeft { get; set; } = Keys.A;
        public Keys MoveRight { get; set; } = Keys.D;
        public Keys Sprint { get; set; } = Keys.LeftShift;
    }
}
