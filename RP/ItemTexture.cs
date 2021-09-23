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


namespace BedrockRpLib
{
    public class ItemTexture
    {
        public string ResourcePackName { get; set; } = "vanilla";

        public string TextureName { get; set; } = "atlas.items";
        public string Textures { get; set; }
        public string Item { get; set; }
        public List<string> itemName { get; set; } = new List<string>();


        public void GenerateItemTexture()
        {
            Directory.CreateDirectory(texturesItemPath);
            var itemTextureExists = File.Exists(itemTexturePath);
            if (itemTextureExists == true)
            {
                AppendItemTexture();
            }
            else if (itemTextureExists == false)
            {
                CreateItemTexture();
            }
            else
            {
                Console.WriteLine("Something went wrong, check ItemTexture class");
            }
        }
        public JObject CreateItemTexture()
        {
            var bpPathsLocal = Directory.GetFiles(bpItemsPath, "*", SearchOption.AllDirectories);

            JObject jObject = new JObject();
            JObject TextureData = new JObject();

            jObject.Add("resource_pack_name", ResourcePackName);
            jObject.Add("texture_name", TextureName);
            jObject.Add(new JProperty("texture_data", TextureData));
            string constantPath = "textures/items/custom/";

            foreach (var pathLocal in bpPathsLocal)
            {
                var objectName = GetIdentifier(pathLocal);
                var rpPathsLocal = BpToRpPathConvert(pathLocal);

                JObject Item = new JObject();
                Textures = constantPath + objectName;
                Item.Add($"textures", Textures);
                TextureData.Add($"{objectName}", Item);

            }
            var jObjectResult = JsonConvert.SerializeObject(jObject, Formatting.Indented);
            File.WriteAllText(itemTexturePath, jObjectResult);
            return jObject;
        }
        public JObject AppendItemTexture()
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
                var objectName = GetIdentifier(pathLocal);
                if (File.ReadLines(itemTexturePath).Any(line => line.Contains(objectName)) == false)
                {
                    string constantPath = "textures/items/custom/";
                    JObject Item = new JObject();
                    Textures = constantPath + objectName;
                    Item.Add($"textures", Textures);
                    // By this I access the proper node in the code, save it to variable and append everything I want. Thanks Will!
                    JObject textures = (JObject)jObject["texture_data"];
                    textures.Add($"{objectName}", Item);


                }
                else if (File.ReadLines(itemTexturePath).Any(line => line.Contains(objectName)) == true)
                {

                }
            }
            var jObjectResult = JsonConvert.SerializeObject(jObject, Formatting.Indented);
            File.WriteAllText(itemTexturePath, jObjectResult);
            return jObject;
        }
    }



}



/* from will

[7:12 PM] William Savage
    
            string s = @"D:\Shapescape\Projects\CF2 export tests\resource_packs\0.0.0.0\textures\item_texture.json";
            var j = JObject.Parse(File.ReadAllText(s));
            JObject textures = (JObject)j["texture_data"];


            JObject texDef = new JObject();
            texDef.Add("texture", "spoon");
            JProperty tex = new JProperty("big.spoon", texDef);


            textures.Add(tex);




​[7:13 PM] William Savage
    Each node in the json file can be accessed like a dictionary using the square brackets
​[7:14 PM] William Savage
    so things like this can be done
var j = (JObject)j["texture_data"]["big.spoon"];

*/