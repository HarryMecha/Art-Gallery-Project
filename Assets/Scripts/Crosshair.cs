using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    public Vector3 defPosition;
    public bool artselected;
    public bool movement;
    private Art art;
    public CanvasGroup canvGroup;
    void Start()
    {
        artselected = false;
        movement = true;
    }

    IEnumerator changeViewout()
    {
        while (artselected == true && (transform.position != art.cameraPosition.transform.position))
        {
            transform.position = Vector3.Lerp(transform.position, art.cameraPosition.transform.position, 7.0f * Time.deltaTime);
            yield return null;
        }
        while (artselected == false && (transform.position != defPosition))
        {
            transform.position = Vector3.Lerp(transform.position, defPosition, 7.0f * Time.deltaTime);
            yield return null;
        }

        if ((transform.position == art.cameraPosition.transform.position))
        {
            yield break;
        }
        if ((transform.position == defPosition))
        {
            movement = true;
            yield break;
        }
    }

    void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
        if(movement == true)
        {
            defPosition = transform.position;
        }
 if (Input.GetKeyDown(KeyCode.Escape) && artselected == true && (transform.position == art.cameraPosition.transform.position))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    artselected = false;
                    StartCoroutine("changeViewout");
                    canvGroup.GetComponent<PanelFade>().Fade();
                }

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Art")
            {
                art = hit.collider.GetComponent<Art>();

                if (Input.GetMouseButtonDown(0) && artselected == false && (transform.position == defPosition))
                {
                    Cursor.lockState = CursorLockMode.None;
                    artselected = true;
                    movement = false;
                    StartCoroutine("changeViewout");
                    canvGroup.GetComponent<PanelFade>().Fade();
                    canvGroup.GetComponent<AIPanelConfig>().PanelSet(art);
                }
               


            }
        }

    }
}

