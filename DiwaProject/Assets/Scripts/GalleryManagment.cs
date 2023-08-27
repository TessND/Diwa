using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class GalleryManagment : MonoBehaviour
{
    [SerializeField] List<Sprite> Girls;
    [SerializeField] List<GameObject> Images;

    private void Start()
    {
        SetTargetGraphic();
    }

    private void SetTargetGraphic()
    {
        foreach (var image in Images)
        {
            image.GetComponent<Button>().targetGraphic = image.GetComponent<Image>();
            image.GetComponent<Image>().raycastTarget = false;
        }

    }

    public void UpdateGallery()
    {
        Girls = GameManager.Instance.CardsSprite;

        for (int i = 0; i != Girls.Count; ++i)
        {
            int index = int.Parse(Girls[i].name);

            if (Images[index].GetComponent<Image>().sprite.name == "Background")
            {
                Image settingsImage = Images[index].GetComponent<Image>();

                settingsImage.sprite = Girls[i];
                settingsImage.color = new Color(255f, 255f, 255f, 255f);
                settingsImage.raycastTarget = true;

                GameManager.Instance.IndexOpenedImages.Add(index);
            }

        }
    }
    public void LoadGallery(int indexImage)
    {
        Image settingsImage = Images[indexImage].GetComponent<Image>();
        settingsImage.sprite = settingsImage.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
        settingsImage.color = new Color(255f, 255f, 255f, 255f);
        settingsImage.raycastTarget = true;
    }
}
