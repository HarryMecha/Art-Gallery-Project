using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFade : MonoBehaviour {

    private bool mFaded = false; //Bool that determines if the panel is faded or not

    public float Duration = 0.4f;

    private CanvasGroup canvGroup;

    private void Start()
    {
        canvGroup = GetComponent<CanvasGroup>();
        canvGroup.alpha = 0;
    }



    public void Fade()
    {   
        mFaded = !mFaded;
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, mFaded ? 1 : 0));
    }
  
    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)
    {
        float counter = 0f;

        while(counter < Duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);
            yield return null;
        }

    }

}
