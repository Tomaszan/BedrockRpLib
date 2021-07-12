using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using static BedrockRpLib.PackPaths;

namespace BedrockRpLib.RP
{
    public static class BlockRP
    {
        public static string[] formatVersion { get; set; } = [1, 1, 0];
        public static string identifier { get; set; }
        public static string isotropic { get; set; } = "false";
        public static string sound { get; set; } = "stone";
        public static string textures { get; set; }



        public void GenerateBlocksJson()
        {
            var blocksJsonExists = File.Exists(blocksJsonPath);
            if (blocksJsonExists == true)
            {
                AppendBlocksJson();
            }
            else if (blocksJsonExists == false)
            {
                CreateBlocksJson();
            }
            else
            {
                Console.WriteLine("Something went wrong, check BlockRP class");
            }
        }
        public JObject CreateBlocksJson()
        {
            var bpPathsLocal = Directory.GetFiles(bpBlocksPath, "*", SearchOption.AllDirectories);
            List<string> rpPathsLocal = new List<string>();
            foreach (var item in bpPathsLocal)
            {
                rpPathsLocal.Add(BpToRpPathConvert(item));
            }
            JObject jObject = new JObject();
            JObject TextureData = new JObject();

            jObject.Add("format_version", formatVersion);
            jObject.Add("texture_name", TextureName);
            jObject.Add(new JProperty("texture_data", TextureData));
            string constantPath = "textures/items/custom/";

            foreach (var pathLocal in rpPathsLocal)
            {
                var fileName = FileNameWithoutExtension(pathLocal);

                JObject Item = new JObject();
                Textures = constantPath + fileName;
                Item.Add($"textures", Textures);
                TextureData.Add($"{fileName}", Item);

            }
            var jObjectResult = JsonConvert.SerializeObject(jObject, Formatting.Indented);
            File.WriteAllText(itemTexturePath, jObjectResult);
            return jObject;
        }
        public JObject AppendBlocksJson()
        {


            var jObject = JObject.Parse(File.ReadAllText(itemTexturePath));


            var bpPathsLocal = Directory.GetFiles(bpItemsPath, "*", SearchOption.AllDirectories);
            List<string> rpPathsLocal = new List<string>();
            foreach (var item in bpPathsLocal)
            {

                rpPathsLocal.Add(BpToRpPathConvert(item));
            }

            foreach (var pathLocal in rpPathsLocal)
            {
                var fileName = FileNameWithoutExtension(pathLocal);
                if (File.ReadLines(itemTexturePath).Any(line => line.Contains(fileName)) == false)
                {
                    string constantPath = "textures/items/custom/";
                    JObject Item = new JObject();
                    Textures = constantPath + fileName;
                    Item.Add($"textures", Textures);
                    // By this I access the proper node in the code, save it to variable and append everything I want. Thanks Will!
                    JObject textures = (JObject)jObject["texture_data"];
                    textures.Add($"{fileName}", Item);


                }
                else if (File.ReadLines(itemTexturePath).Any(line => line.Contains(fileName)) == true)
                {

                }
            }
            var jObjectResult = JsonConvert.SerializeObject(jObject, Formatting.Indented);
            File.WriteAllText(itemTexturePath, jObjectResult);
            return jObject;
        }
    }
}
