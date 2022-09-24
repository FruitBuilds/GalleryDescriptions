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
    // needs to be named Postfix to tell Harmony to run it after the base game's method
    static void Postfix(GallerySlot gallerySlot, SetSlotPortrait ___galleryPicture)
    {
        // need some method to look up an id string and return a sprite
        // you also need some kind of manager to handle loading the new gallery images from disk or from embedded resources
        // see: https://github.com/DeadlyKitten/CustomStockIcons/blob/master/CustomStockIcons/Managers/IconManager.cs
        // to generate a Sprite from a texture you can use the following code, where `texture` is the texture you want to convert
        // Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2f, texture.height / 2f));
        if (galleryManager.TryGetGallerySprite(gallerySlot.galleryPictureData.ID, out Sprite sprite))
        {
            // if true then you can replace the image

            // not 100% sure this will work, if not you might need to use reflection or something to change some things on this object
            ___galleryPicture.SetSprite(sprite);
        }
    }
}
