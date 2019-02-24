using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour
{
    // Components
    private Transform aimspotTransform;
    private Transform gunTransform;

    private bool isShot;
    private Vector3 latchPosition;
    private float shotRange;
    private RaycastHit hit;

    private playerController playerControllerRef;

    // Start is called before the first frame update
    void Start()
    {
        aimspotTransform = GameObject.Find("aimspot").transform;
        gunTransform = GetComponent<Transform>();

        isShot = false;
        latchPosition = new Vector3(0, 0, 0);
        shotRange = 40f;
        hit = new RaycastHit();

        playerControllerRef = GameObject.Find("player").GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("gun position: " + gunTransform.position);
        Debug.Log("latch position: " + latchPosition);
        if (isShot)
        {
            Debug.DrawRay(gunTransform.position, latchPosition - gunTransform.position, Color.green);
            Debug.DrawLine(gunTransform.position, latchPosition, Color.green);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Vector3 launchForce = (latchPosition - gunTransform.position);
                launchForce.y += (Vector3.Distance(gunTransform.position, latchPosition));
                launchForce = Vector3.Normalize(launchForce) * 2000f;
                playerControllerRef.launch(launchForce);
                isShot = false;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                isShot = false;
            }
        }

        Debug.DrawRay(gunTransform.position, aimspotTransform.position - gunTransform.position, Color.cyan);
        if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(gunTransform.position, aimspotTransform.position - gunTransform.position, out hit, shotRange))
        {
            Ray ray = new Ray(gunTransform.position, aimspotTransform.position - gunTransform.position);
            latchPosition = ray.GetPoint(hit.distance);
            isShot = true;
        }

    }
}
