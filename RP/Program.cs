using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using static BedrockRpLib.PackPaths;

namespace BedrockRpLib
{
    class Program
    {

        static void Main(string[] args)
        {
            TexturesList texturesList = new TexturesList();
            texturesList.CreateTexturesList();
        }


    }
}
