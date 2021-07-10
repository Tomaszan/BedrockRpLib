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
    class ItemsRP
    {
        public string formatVersion { get; set; } = "1.12.0";

        public string minecraftItem { get; set; }
        public string description { get; set; }
        public string identifier { get; set; }
        public string category { get; set; } = "Nature";
        public string components { get; set; }
        public string minecraftIcon { get; set; }
        public string minecraftUseAnimation { get; set; }
        public void GenerateItemRP(string bpPath, string rpPath)
        {
            var bpPathsLocal = Directory.GetFiles(bpPath, "*", SearchOption.AllDirectories);
            List<string> rpPathsLocal = new List<string>();
            foreach (var item in bpPathsLocal)
            {
                rpPathsLocal.Add(BpToRpPathConvert(item));
            }
            foreach (var item in rpPathsLocal)
            {
                if (!File.Exists(item))
                {

                    var fileName = FileNameWithoutExtension(item);

                    JObject jObjectrpItem = new JObject();


                    jObjectrpItem.Add("format_version", formatVersion);

                    JObject minecraftItem = new JObject();
                    JObject description = new JObject();

                    jObjectrpItem.Add("minecraft:item", minecraftItem);
                    minecraftItem.Add(new JProperty("description", description));
                    string identifier = identifierPrefix + fileName;
                    description.Add(new JProperty("identifier", identifier));
                    description.Add(new JProperty("category", category));

                    JObject components = new JObject();
                    jObjectrpItem.Add(new JProperty("components", components));
                    string minecraftIcon = fileName;
                    components.Add(new JProperty("minecraft:icon", minecraftIcon));

                    var jObjectrpItemResult = JsonConvert.SerializeObject(jObjectrpItem, Formatting.Indented);


                    File.WriteAllText(item, jObjectrpItemResult);
                }
            }

        }
    }
}
