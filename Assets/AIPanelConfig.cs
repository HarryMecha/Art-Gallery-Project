using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AIPanelConfig : MonoBehaviour {
    
    public void PanelSet(Art art)
    {
        Image art2 = GameObject.Find("ArtImage").GetComponent<Image>();
        art2.sprite = art.artImage;
        string ad = art.Description;
        GameObject tm = GameObject.Find("DescriptionText");
        tm.GetComponent<TextMeshProUGUI>().SetText(ad);

    }
}
