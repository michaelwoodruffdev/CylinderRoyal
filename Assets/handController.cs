using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handController : MonoBehaviour {

    private Transform handTransform;
    private Vector3 zRotation;
    private float rotateSpeed;

	// Use this for initialization
	void Start () {
        handTransform = GetComponent<Transform>();
        zRotation = new Vector3(0, 0, 0);
        rotateSpeed = 100f;
	}
	
	// Update is called once per frame
	void Update () {
        zRotation.z = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
        handTransform.Rotate(zRotation);

	}
}
