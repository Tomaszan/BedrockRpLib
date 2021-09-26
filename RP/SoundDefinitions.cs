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
    public class SoundDefinition
    {

        public string formatVersion { get; set; } = "1.8.0";
        public string category { get; set; } = "neutral";
        public JArray[] sounds { get; set; }
        public string name { get; set; }

        public void GenerateSoundDefinitions()
        {
            Directory.CreateDirectory(rpSoundsDirectory);
            var soundDefinitionsExist = File.Exists(rpSoundDefinitions);
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
            JObject soundDefinitions = new JObject();
            JObject Sound = new JObject();

            jObject.Add("format_version", formatVersion);
            jObject.Add(new JProperty("sound_definitions", soundDefinitions));

            foreach (var pathLocal in rpPathsLocal)
            {
                var objectName = "custom." + Path.GetFileNameWithoutExtension(pathLocal);

                // Changing path to get relative path to the file
                int item1 = pathLocal.IndexOf(@"sounds\custom\");
                string soundPath = pathLocal.Substring(item1)
                                            .Replace("\\", "/")
                                            .Replace(".ogg", "")
                                            .Replace(".mp3", "")
                                            .Replace(".wav", "");



                JArray sounds = new JArray();
                JObject SoundName = new JObject();

                name = soundPath;

                SoundName.Add("name", name);

                soundDefinitions.Add($"{objectName}", Sound);
                Sound.Add($"category", category);
                sounds.Add(SoundName);
                Sound.Add(new JProperty("sounds", sounds));

            }
            var jObjectResult = JsonConvert.SerializeObject(jObject, Formatting.Indented);
            File.WriteAllText(rpSoundDefinitions, jObjectResult);
            return jObject;
        }
        public JObject AppendSoundDefinitions()
        {


            var jObject = JObject.Parse(File.ReadAllText(rpSoundDefinitions));

            JObject Sound = new JObject();

            var rpPathsLocal = Directory.GetFiles(rpSoundsDirectoryCustom, "*", SearchOption.AllDirectories);

            foreach (var pathLocal in rpPathsLocal)
            {
                var objectName = "custom." + Path.GetFileNameWithoutExtension(pathLocal);
                if (File.ReadLines(rpSoundDefinitions).Any(line => line.Contains(objectName)) == false)
                {
                    // Changing path to get relative path to the file
                    int item1 = pathLocal.IndexOf(@"sounds\custom\");
                    string soundPath = pathLocal.Substring(item1)
                                                .Replace("\\", "/")
                                                .Replace(".ogg", "")
                                                .Replace(".mp3", "")
                                                .Replace(".wav", "");
                    // Creating certain music file .json object
                    JArray sounds = new JArray();
                    JObject SoundName = new JObject();

                    name = soundPath;

                    SoundName.Add("name", name);

                    Sound.Add($"category", category);
                    sounds.Add(SoundName);
                    Sound.Add(new JProperty("sounds", sounds));

                    // By this I access the proper node in the code, save it to variable and append everything I want. Thanks Will!
                    JObject soundDefinitions = (JObject)jObject["sound_definitions"];
                    soundDefinitions.Add($"{objectName}", Sound);


                }
            }
            var jObjectResult = JsonConvert.SerializeObject(jObject, Formatting.Indented);
            File.WriteAllText(rpSoundDefinitions, jObjectResult);
            return jObject;
        }
    }
}
