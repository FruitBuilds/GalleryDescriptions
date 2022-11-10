using BepInEx;
using GalleryDescriptions.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GalleryDescriptions.Managers
{
    static class GalleryManager
    {
        static readonly Dictionary<string, Texture2D> GalleryDict = new Dictionary<string, Texture2D>();

        static readonly string folder = Path.Combine(Paths.PluginPath, "GalleryDescriptions", "Gallery Descriptions");
        static readonly List<string> allowedFileTypes = new List<string> { ".png", ".jpg" };

        public static void Init()
        {
            Directory.CreateDirectory(folder);
            LoadAllImages();
        }

        static void LoadAllImages()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            foreach (var file in Directory.GetFiles(folder).Where(x => allowedFileTypes.Contains(Path.GetExtension(x).ToLower())))
            {
                var texture = LoadImageFromFile(file);
                var id = Path.GetFileNameWithoutExtension(file);
                if (GalleryDict.ContainsKey(id.ToLower()))
                {
                    Plugin.LogWarning($"Multiple gallery images found for {id.ToLower()}! Ignoring {Path.GetFileName(file)}");
                    continue;
                }
                GalleryDict.Add(id.ToLower(), texture);
            }

            foreach (var resource in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            {
                var texture = LoadImageFromEmbeddedResource(resource);
                var array = resource.Split('.');
                var id = array[array.Length - 2];

                if (GalleryDict.ContainsKey(id.ToLower()))
                {
                    Plugin.LogWarning($"Multiple gallery images found for {id.ToLower()}! Ignoring {Path.GetFileName(resource)}");
                    continue;
                }

                GalleryDict.Add(id.ToLower(), texture);
            }

            stopwatch.Stop();
            Plugin.LogInfo($"Loaded {GalleryDict.Keys.Count} sprite{(GalleryDict.Keys.Count == 1 ? "" : "s")} in {stopwatch.ElapsedMilliseconds} ms.");

            
        }

        internal static bool TryGetGalleryImage(string id, out Texture2D texture) => GalleryDict.TryGetValue(id, out texture);

        static Texture2D LoadImageFromFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var image = new Texture2D(2, 2);
            image.LoadImage(bytes);
            image.wrapMode = TextureWrapMode.Clamp;

            return image;
        }

        static Texture2D LoadImageFromEmbeddedResource(string path)
        {
            Texture2D image = new Texture2D(2, 2);

            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(path))
            {
                byte[] imageData = new byte[stream.Length];
                stream.Read(imageData, 0, (int)stream.Length);
                image.LoadImage(imageData);
                image.wrapMode = TextureWrapMode.Clamp;
            }

            return image;
        }
    }
}