using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    [SerializeField]
    public Sprite selected = null, notSelected = null, locked = null;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void selectedImage()
    {
        image.sprite = selected;
    }

    public void notSelectedImage()
    {
        image.sprite = notSelected;
    }

    public void lockedImage()
    {
        image.sprite = locked;
    }
}
