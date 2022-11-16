using GalleryDescriptions.Managers;
using HarmonyLib;
using Nick;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GalleryDescriptions.Patches
{
    [HarmonyPatch(typeof(GalleryScreen), "MoveImage")]
    class GalleryScreen_MoveImage
    {
        static BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
        static MethodInfo GetNextUnlockedSlot = typeof(GalleryScreen).GetMethod("GetNextUnlockedSlot", bindingFlags);
        static MethodInfo GetGallerySlot = typeof(GalleryScreen).GetMethod("GetGallerySlot", bindingFlags);
        static FieldInfo seekTex = typeof(SetSlotPortrait).GetField("seekTex", bindingFlags);

        static void Postfix(GalleryScreen __instance, int side, SetSlotPortrait ___galleryPicture)
        {
            var nextUnlockedSlot = GetNextUnlockedSlot.Invoke(__instance, new object[] { side });
            var gallerySlot = GetGallerySlot.Invoke(__instance, new object[] { nextUnlockedSlot }) as GallerySlot;
            var picturePath = gallerySlot.galleryPictureData.PicturePath;

            var id = picturePath.Split('/').Last();
            if (GalleryManager.TryGetGalleryImage(id, out Texture2D texture))
            {
                ___galleryPicture.rawImage.texture = texture;
                seekTex.SetValue(___galleryPicture, false);
                ___galleryPicture.rawImage.enabled = true;
            }
        }
    }
}
