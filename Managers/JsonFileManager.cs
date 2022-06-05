using System.IO;
using Newtonsoft.Json;

namespace Astrostrider.Managers
{
    public class JsonFileManager
    {
        public static readonly string DefaultSpaceObjectDataJsonPath = @"Resources\Data\DefaultSpaceObjectData.json";
        public static readonly string UserSpaceObjectDataJsonPath = @"Resources\Data\UserSpaceObjectData.json";
        public static readonly string DefaultSpaceShipDataJsonPath = @"Resources\Data\DefaultSpaceShipData.json";
        public static readonly string UserSpaceShipDataJsonPath = @"Resources\Data\UserSpaceShipData.json";

        public static T Deserialize<T>(string readerPath)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader streamReader = new StreamReader(readerPath))
                {
                    JsonTextReader jsonTextReader = new JsonTextReader(streamReader);
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
            catch (FileNotFoundException)
            {
                return default(T);
            }
        }

        public static void Serialize<T>(T objectToSerialize, string writerPath)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter streamWriter = new StreamWriter(writerPath))
            {
                JsonWriter writer = new JsonTextWriter(streamWriter);
                serializer.Serialize(writer, objectToSerialize);
            }
        }
    }
}
