using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    private Transform aimspot;
    private Transform cameraTransform;
    private Transform zoomoutTransform;
    private Transform zoominTransform;
    private Transform currentTransform;
    private bool isDownSights;

    private bool lerpForward;
    private float fracJourney;
    private bool isLerping;

    

	// Use this for initialization
	void Start () {
        aimspot = GameObject.Find("aimspot").transform;
        zoomoutTransform = GameObject.Find("zoomoutLocation").transform;
        zoominTransform = GameObject.Find("zoominLocation").transform;
        currentTransform = zoomoutTransform;
        Debug.Log(aimspot);
        cameraTransform = GetComponent<Transform>();
        isDownSights = false;
        isLerping = false;
        cameraTransform.position = zoomoutTransform.position;
        lerpForward = false;
        fracJourney = 0f;
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(isLerping);
        Debug.Log(fracJourney);

        //if (isLerping)
        //{
        //    if (lerpForward)
        //    {
        //        fracJourney += .05f;
        //    }
        //    else
        //    {
        //        fracJourney -= .05f;
        //    }
        //   if (fracJourney <= 0f)
        //    {
        //        fracJourney = 0f;
        //        isLerping = false;
        //    } else if (fracJourney >= 1f)
        //    {
        //       fracJourney = 1f;
        //        isLerping = false;
        //    }
        //}

        if (lerpForward)
        {
            fracJourney += .1f;
        }
        else
        {
            fracJourney -= .1f;
        }

        cameraTransform.LookAt(aimspot);

        cameraTransform.position = Vector3.Lerp(zoomoutTransform.position, zoominTransform.position, fracJourney);
        //if (isDownSights)
        //{
        //    currentTransform = zoominTransform;
        //} else
        //{
        //    currentTransform = zoomoutTransform;
        //}
        //cameraTransform.position = currentTransform.position;

        //if (Input.GetMouseButtonDown(1) && !isLerping)
        //{
        //    lerpForward = true;
        //    isLerping = true;
        //}

        if (Input.GetMouseButtonDown(1))
        {
            if (fracJourney <= 0f)
            {
                fracJourney = 0f;
            }
            lerpForward = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (fracJourney >= 1f)
            {
                fracJourney = 1f;
            }
            lerpForward = false;
        }

       
	}
}
