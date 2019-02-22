using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    private Transform aimspot;
    private Transform cameraTransform;

	// Use this for initialization
	void Start () {
        aimspot = GameObject.Find("aimspot").transform;
        Debug.Log(aimspot);
        cameraTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        cameraTransform.LookAt(aimspot);
	}
}
