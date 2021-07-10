using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using static BedrockRpLib.PackPaths;

namespace BedrockRpLib.RP
{
    class EntityRP
    {
        public string formatVersion { get; set; } = "1.8.0";
        public string clientEntity { get; set; }
        public string description { get; set; }
        public string identifier { get; set; }
        public string spawnEgg { get; set; }
        public string baseColor { get; set; } = "#00ff2a";
        public string overlayColor { get; set; } = "#eb00d7";
        public string textures { get; set; }
        public string geometry { get; set; }
        public string materials { get; set; }
        public string[] renderControllers { get; set; }
        public string EntityDefault { get; set; }
        public string animationControllers { get; set; }
        public string animations { get; set; }
        public string particleEffects { get; set; }
        public string soundEffects { get; set; }

        public void GenerateEntityRP(string bpPath, string rpPath)
        {
            Directory.CreateDirectory(rpEntityPath);

            var bpPathsLocal = Directory.GetFiles(bpPath, "*", SearchOption.AllDirectories);
            List<string> rpPathsLocal = new List<string>();
            foreach (var item in bpPathsLocal)
            {
                rpPathsLocal.Add(BpToRpPathConvert(item));
            }
            foreach (var item in rpPathsLocal)
            {

                if (File.Exists(item) == true)
                {
                    var fileName = FileNameWithoutExtension(item);

                    Console.WriteLine($"File {fileName} already exists");
                }
                else if (File.Exists(item) == false)
                {

                    var fileName = FileNameWithoutExtension(item);

                    JObject jObjectEntity = new JObject();


                    jObjectEntity.Add("format_version", formatVersion);

                    JObject clientEntity = new JObject();
                    JObject description = new JObject();

                    jObjectEntity.Add("minecraft:client_entity", clientEntity);
                    clientEntity.Add(new JProperty("description", description));
                    string identifier = identifierPrefix + fileName;
                    description.Add(new JProperty("identifier", identifier));
                    JObject spawnEgg = new JObject();
                    description.Add(new JProperty("spawn_egg", spawnEgg));
                    spawnEgg.Add(new JProperty("base_color", baseColor));
                    spawnEgg.Add(new JProperty("overlay_color", overlayColor));

                    JObject materials = new JObject();
                    string materialsDefault = "entity_alphatest";
                    materials.Add(new JProperty("default", materialsDefault));

                    JObject geometry = new JObject();
                    description.Add(new JProperty("geometry", geometry));
                    string geometryDefault = "geometry.empty";
                    geometry.Add(new JProperty("default", geometryDefault));

                    JObject textures = new JObject();
                    description.Add(new JProperty("textures", textures));
                    string texturesDefault = "textures/entity/empty";
                    textures.Add(new JProperty("default", texturesDefault));

                    JArray renderControllers = new JArray() { "controller.render.default" };
                    description.Add(new JProperty("render_controllers", renderControllers));

                    var jObjectEntityResult = JsonConvert.SerializeObject(jObjectEntity, Formatting.Indented);

                    File.WriteAllText(item, jObjectEntityResult);
                }
                else
                {
                    Console.WriteLine("Something went wrong, check EntityRP class");
                }
            }



        }
    }
}
