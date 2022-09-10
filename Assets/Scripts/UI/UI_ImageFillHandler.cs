using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ImageFillHandler : MonoBehaviour
{
    [SerializeField] private Image image;

    /// <summary>
    /// Sets the UI Image Fill to the passed value
    /// </summary>
    /// <param name="newFill"></param>
    public void SetFill(float newFill)
    {
        newFill = Mathf.Clamp01(newFill);
        image.fillAmount = newFill;
    }
}
