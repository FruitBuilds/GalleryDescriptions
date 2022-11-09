using GalleryDescriptions.Utilities;
using HarmonyLib;
using Nick;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GalleryDescriptions.Patches
{
    [HarmonyPatch(typeof(GalleryScreen), OnPressedGallerySlot)]
static class GalleryScreen_OnPressedGallerySlot
{
    static void Postfix(GallerySlot gallerySlot, SetSlotPortrait ___galleryPicture)
    {
        if (___galleryslot == -1) return;

            if (!isCoroutineRunning)
            

          
        if (galleryManager.TryGetGallerySprite(gallerySlot.galleryPictureData.ID, out Sprite sprite))
        {
             Plugin.LogDebug($"Replacing gallery images for {agent.GameUniqueIdentifier}");
                    for (int i = 0; i < ___gallery.Length; i++)
                        ___gallery[i].sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2f, texture.height / 2f));
            // if true then you can replace the image

            // not 100% sure this will work, if not you might need to use reflection or something to change some things on this object
            ___galleryPicture.SetSprite(sprite);
        }
    }
}
