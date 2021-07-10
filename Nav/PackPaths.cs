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
        public static string bpAnimationControllersPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\animation_controllers");
        public static string bpAnimationsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\animations");
        public static string bpBlocksPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\blocks");
        public static string bpEntitiesPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\entities");
        public static string bpFunctionPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\function");
        public static string bpItemsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\items");
        public static string bpLootTablesPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\loot_tables");
        public static string bpRecipesPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\recipes");
        public static string bpSpawnRulesPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\spawn_rules");
        public static string bpTextPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"behavior_packs\0\text");

        // Rp
        public static string itemTexturePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\textures\item_texture.json");
        public static string rpTextures = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\textures");
        public static string rpItemsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\items");
        public static string texturesItemPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\textures\items\custom");
        public static string texturesListPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\textures\textures_list.json");
        public static string rpEntityPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\entity");
        public static string rpRenderControllersPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, @"resource_packs\0\render_controllers");


        public static List<string> filesNamesWithoutExtension { get; set; }
        public static string identifierPrefix { get; set; } = "shapescape:";



        public static string FileNameWithoutExtension(string path)
        {

            var lastSlash = path.LastIndexOf(@"\");
            string fileName = path.Replace(".rp_item", "")
                               .Replace(".item", "")
                               .Replace(".json", "")
                               .Substring(lastSlash)
                               .Replace(@"\", "");

            return fileName;
        }


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

        public static void MirrorFiles(string bpPath, string rpPath)
        {
            var bpPathsLocal = Directory.GetFiles(bpPath, "*", SearchOption.AllDirectories);
            List<string> rpPathsLocal = new List<string>();
            foreach (var item in bpPathsLocal)
            {
                var item2 = item.Replace("behavior_packs", "resource_packs")
                                .Replace("entities", "entity")
                                .Replace("bp", "rp")
                                .Replace(".behavior", ".entity");
                rpPathsLocal.Add(item2);
            }
            foreach (var item in rpPathsLocal)
            {
                if (!File.Exists(item))
                {
                    File.Create(item);
                }
            }
            //File.Create()
        }
        public static string BpToRpPathConvert(string bpPath)
        {
            var rpPath = bpPath.Replace("behavior_packs", "resource_packs")
                               .Replace("entities", "entity")
                               .Replace("bp", "rp")
                               .Replace(".behavior", ".entity");
            return rpPath;
        }

    }
}
