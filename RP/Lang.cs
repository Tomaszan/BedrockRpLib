﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using static BedrockRpLib.PackPaths;


namespace BedrockRpLib
{
    public class Lang
    {
        public void LangGenerator()
        {
            var prefix = "shapescape";
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            var bpEntities = Directory.GetFiles(PackPaths.bpEntitiesPath, "*", SearchOption.AllDirectories);
            var bpItems = Directory.GetFiles(PackPaths.bpItemsPath, "*", SearchOption.AllDirectories);
            var bpBlocks = Directory.GetFiles(PackPaths.bpBlocksPath, "*", SearchOption.AllDirectories);




            Console.WriteLine("\n\n\n### Entities' names");
            foreach (var item in bpEntities)
            {
                if ((Regex.IsMatch(item, "^(?!.*(timer|dummy|internal|main|tick|player|autogenerated))")) == true) //
                {
                    Console.WriteLine($"    entity.{prefix}:{GetIdentifier(item)}.name=" + myTI.ToTitleCase((GetIdentifier(item)).Replace("_", " ")));
                    Console.WriteLine($"    item.spawn_egg.{prefix}:{GetIdentifier(item)}.name=" + "Spawn " + myTI.ToTitleCase((GetIdentifier(item)).Replace("_", " ")) + "\n");
                }

            }

            Console.WriteLine("\n\n\n### Action and Interact Hit");
            foreach (var item in bpEntities)
            {
                if ((Regex.IsMatch(item, "^(?!.*(timer|dummy|internal|main|tick|player|autogenerated))")) == true) //
                {
                    Console.WriteLine($"    action.hint.exit.{prefix}:{GetIdentifier(item)}=Tap Sneak to Exit");
                    Console.WriteLine($"    action.interact.{prefix}:{GetIdentifier(item)}=Interact\n");
                }

            }

            Console.WriteLine("\n\n\n### Items' names");
            foreach (var item in bpItems)
            {
                if ((Regex.IsMatch(item, "^(?!.*(timer|dummy|internal|main|tick))")) == true) //
                {
                    Console.WriteLine($"    item.{prefix}:{GetIdentifier(item)}.name=" + myTI.ToTitleCase((GetIdentifier(item)).Replace("_", " ")));
                }

            }

            Console.WriteLine("\n\n\n### Blocks' names");
            foreach (var item in bpBlocks)
            {
                if ((Regex.IsMatch(item, "^(?!.*(timer|dummy|internal|main|tick))")) == true) //
                {
                    Console.WriteLine($"    tile.{prefix}:{GetIdentifier(item)}.name=" + myTI.ToTitleCase((GetIdentifier(item)).Replace("_", " ")));
                }

            }
        }
    }
}
