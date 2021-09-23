﻿using Newtonsoft.Json;
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
    class SoundDefinition
    {

        public string formatVersion { get; set; } = "1.8.0";
        public string category { get; set; } = "neutral";
        public string sounds { get; set; }
        public string name { get; set; }

        public void GenerateSoundDefinitions()
        {
            Directory.CreateDirectory(rpSoundsDirectory);
            var soundDefinitionsExist = File.Exists(rpSoundDefinition);
            if (soundDefinitionsExist == true)
            {
                AppendSoundDefinitions();
            }
            else if (soundDefinitionsExist == false)
            {
                CreateSoundDefinitions();
            }
            else
            {
                Console.WriteLine("Something went wrong, check SoundDefinitions class");
            }
        }
        public JObject CreateSoundDefinitions()
        {
            var rpPathsLocal = Directory.GetFiles(rpSoundsDirectoryCustom, "*", SearchOption.AllDirectories);
            
            JObject jObject = new JObject();
            JObject formatVersion = new JObject();
            JObject soundDefinitions = new JObject();
            JObject Sound = new JObject();

            jObject.Add("format_version", formatVersion);
            jObject.Add(new JProperty("sound_definitions", soundDefinitions));

            foreach (var pathLocal in rpPathsLocal)
            {
                var objectName = Path.GetFileName(pathLocal);

                // Changing path to get relative path to the file
                int item1 = pathLocal.IndexOf("sounds/custom/");
                string soundPath = pathLocal.Substring(item1);


                JObject Name = new JObject();
                JArray Sounds = new JArray() { };



                Name = soundPath;

                soundDefinitions.Add($"{objectName}", Sound);
                Sound.Add($"category", category);
                Sound.Add(new JProperty("sounds", Sounds));








            }
            var jObjectResult = JsonConvert.SerializeObject(jObject, Formatting.Indented);
            File.WriteAllText(itemTexturePath, jObjectResult);
            return jObject;
        }
        public JObject AppendSoundDefinitions()
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