using GalleryDescriptions.Utilities;
using HarmonyLib;
using Nick;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GalleryDescriptions.Patches
{
    [HarmonyPatch(typeof(GalleryImage), "Activate")]
    class GalleryImage_Activate
    {
        static GameInstance gi;
        static bool isCoroutineRunning;
        // Token: 0x06001908 RID: 6408 RVA: 0x0006F434 File Offset: 0x0006D634
        public Action<object> OnPressedGallerySlot(GallerySlot gallerySlot)
        {
            if (gallerySlot.galleryPictureData.Unlocked)
            {
                RectTransform component = this.mainContainer.GetComponent<RectTransform>();
                if (component != null)
                {
                    component.anchoredPosition = Vector2.zero;
                }
                this.seeingPicture = true;
                this.galleryPagesList[this.currentPageIndex].ToggleMenuInputFlow(fakse, false);
                this.gallerybacground.SetActive(true);
                this.galleryPicture.gameObject.SetActive(true);
                this.galleryPictures.SetResourceTexture(galleryslot.galleryPictureData.PicturePath);
                this.onPressedNo = this.onPressedClosePicture;
            }
            return null;
        }

        static IEnumerator NullGameInstance()
        {
            isCoroutineRunning = true;
            yield return new WaitForEndOfFrame();
            gi = null;
            isCoroutineRunning = false;
        }
    }
}