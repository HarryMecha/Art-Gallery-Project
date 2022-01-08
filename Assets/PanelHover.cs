using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelHover : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler{

    private bool mFaded = false;
    public float Duration = 0.1f;
    public CanvasGroup canvGroup;

    private void Start()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Fade();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Fade();
    }

    public void Fade()
    {
        mFaded = !mFaded;
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, mFaded ? 1 : 0));
    }

    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)
    {
        float counter = 0f;

        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);
            yield return null;
        }

    }


}
