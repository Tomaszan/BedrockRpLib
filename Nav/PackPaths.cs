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

namespace BedrockRpLib
{
    public static class PackPaths
    {

        // BP
        public static string bpAnimationControllersPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\animation_controllers");
        public static string bpAnimationsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\animations");
        public static string bpBlocksPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\blocks");
        public static string bpEntitiesPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\entities");
        public static string bpFunctionPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\function");
        public static string bpItemsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\items");
        public static string bpLootTablesPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\loot_tables");
        public static string bpRecipesPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\recipes");
        public static string bpSpawnRulesPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\spawn_rules");
        public static string bpTextPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\text");

        // Rp
        public static string itemTexturePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\textures\item_texture.json");
        public static string blocksJsonPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\blocks.json");
        public static string rpTextures = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\textures");
        public static string rpItemsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\items");
        public static string texturesItemPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\textures\items\custom");
        public static string texturesListPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\textures\textures_list.json");
        public static string rpEntityPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\entity");
        public static string rpRenderControllersPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\render_controllers");
        public static string rpSoundDefinitions = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\sounds\sound_definitions.json");
        public static string rpSoundsDirectory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\sounds");
        public static string rpSoundsDirectoryCustom = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\sounds\custom");


        public static List<string> filesNamesWithoutExtension { get; set; }
        public static string identifierPrefix { get; set; } = "shapescape:";


        public static void MirrorDirectories(string bpPath, string rpPath)
        {
            var subDirectories = Directory.GetDirectories(bpPath, "*", SearchOption.AllDirectories);
            foreach (var item in subDirectories)
            {
                var item2 = item.Replace(bpItemsPath, "")
                                .Replace("entities", "entity")
                                .Substring(1);
                var item3 = Path.Combine(rpPath, item2);
                Directory.CreateDirectory(item3);
            }
        }
        public static string BpToRpPathConvert(string bpPath)
        {
            var rpPath = bpPath.Replace("behavior_packs", "resource_packs")
                               .Replace("entities", "entity")
                               .Replace("bp", "rp")
                               .Replace(".behavior", ".entity");
            return rpPath;
        }
        public static void MirrorFiles(string bpPath, string rpPath)
        {
            var bpPathsLocal = Directory.GetFiles(bpPath, "*", SearchOption.AllDirectories);
            List<string> rpPathsLocal = new List<string>();
            foreach (var item in bpPathsLocal)
            {

                var item2 = BpToRpPathConvert(item);

                rpPathsLocal.Add(item2);
            }
            foreach (var item in rpPathsLocal)
            {
                if (!File.Exists(item))
                {
                    File.Create(item);
                }
            }
        }
        public static string GetIdentifier(string bpPath)
        {
            string identifier = string.Empty;

            var item2 = File.ReadAllText(bpPath);
            var item3 = item2.IndexOf("\"identifier\":");
            var item4 = item3 + 13;
            var colon = item2.IndexOf(":", item4);
            var item5 = colon;
            var quotationMark = item2.IndexOf("\"", item5);
            var stringLength = quotationMark - colon;
            identifier = item2.Substring(colon + 1, stringLength - 1);


            return identifier;

        }

    }
}
