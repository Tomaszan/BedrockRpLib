using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using static BedrockRpLib.PackPaths;

namespace BedrockRpLib
{
    public class TexturesList
    {
        public void GenerateTexturesList()
        {
            Directory.CreateDirectory(rpTextures);
            var itemTextureExists = File.Exists(texturesListPath);
            if (itemTextureExists == true)
            {
                //AppendTexturesList();
            }
            else if (itemTextureExists == false)
            {
                CreateTexturesList();
            }
            else
            {
                Console.WriteLine("Something went wrong, check TexturesList class");
            }

        }
        public void CreateTexturesList()
        {
            var texturesPathLocal = Directory.GetFiles(rpTextures, "*", SearchOption.AllDirectories);
            JArray texturesList = new JArray();
            foreach (var item in texturesPathLocal)
            {
                var item2 = item.Replace(rpTextures, "")
                                .Replace(".png", "")
                                .Replace(".tga", "")
                                .Replace(@"\", "/");
                var item3 = "textures" + item2;
                if (!item3.Contains(".json"))
                {
                    texturesList.Add(item3);
                }
            }


            JObject jObjectTexturesList = new JObject();
            jObjectTexturesList.Add("", texturesList);

            var jObjectTexturesListResult = JsonConvert.SerializeObject(texturesList, Formatting.Indented);
            File.WriteAllText(texturesListPath, jObjectTexturesListResult);
        }

        public void AppendTexturesList()
        {

        }
    }
}
