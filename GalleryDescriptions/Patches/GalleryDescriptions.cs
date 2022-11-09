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
                    
            ___galleryPicture.SetSprite(sprite);
        }
    }
}
