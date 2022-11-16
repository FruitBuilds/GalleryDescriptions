using GalleryDescriptions.Managers;
using HarmonyLib;
using Nick;
using UnityEngine;
using System.Linq;
using System.Reflection;

namespace GalleryDescriptions.Patches
{
    [HarmonyPatch(typeof(GalleryScreen), nameof(GalleryScreen.OnPressedGallerySlot))]
    static class GalleryScreen_OnPressedGallerySlot
    {
        static BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
        static FieldInfo seekTex = typeof(SetSlotPortrait).GetField("seekTex", bindingFlags);

        static void Postfix(GallerySlot gallerySlot, SetSlotPortrait ___galleryPicture)
        {
            var id = gallerySlot.galleryPictureData.PicturePath.Split('/').Last();
            if (GalleryManager.TryGetGalleryImage(id, out Texture2D texture))
            {
                ___galleryPicture.rawImage.texture = texture;
                seekTex.SetValue(___galleryPicture, false);
                ___galleryPicture.rawImage.enabled = true;
            }
        }
    }
}
