using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    // components
    private Transform playerTransform;
    private Rigidbody playerRigidBody;

    // rotation fields
    private float rotateSpeed;

    // translation fields
    private float moveSpeed;
    private Vector3 translation;

    // jump fields
    private bool jumped;
    private Vector3 jumpForceVector;
    private float jumpForce;
    private bool isMidAir;

    // scale vector fields for crouching and standing
    private Vector3 standVector;
    private Vector3 crouchVector;

    private Vector3 launchForce;
    private bool isLaunched;

	// INITIALIZE
    /////////////
	void Start () {
        initializeFields();
    }

    void initializeFields()
    {
        // components
        playerTransform = GetComponent<Transform>();
        playerRigidBody = GetComponent<Rigidbody>();

        // rotation fields
        rotateSpeed = 200f;

        // translation fields
        moveSpeed = 10f;

        // jump fields
        jumped = false;
        jumpForce = 500f;
        jumpForceVector = new Vector3(0, jumpForce, 0);
        isMidAir = false;

        // scale vector fields for crouching and standing
        standVector = new Vector3(0.5778568f, 0.7373421f, 0.5068351f);
        crouchVector = new Vector3(0.5778568f, 0.4373421f, 0.5068351f);

        isLaunched = false;
        launchForce = new Vector3(0, 0, 0);
    }

    // UPDATE
    /////////
    void Update () {
        handleMovement();
	}

    void handleMovement()
    {
            playerTransform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed, 0);

        if (!sprint() && !crouch())
        {
            walk();
        }

        Vector3 translation = calculateTranslation();
        playerTransform.Translate(translation * Time.deltaTime);

        if (!isMidAir && Input.GetKeyDown(KeyCode.Space))
            jumped = true;
    }

    Vector3 calculateTranslation()
    {
        float deltaZ = 0;
        float deltaX = 0;

        if (Input.GetKey(KeyCode.A)) deltaZ -= moveSpeed;
        if (Input.GetKey(KeyCode.D)) deltaZ += moveSpeed;
        if (Input.GetKey(KeyCode.W)) deltaX -= moveSpeed;
        if (Input.GetKey(KeyCode.S)) deltaX += moveSpeed;

        return new Vector3(deltaX, 0, deltaZ);
    }

    bool sprint ()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerTransform.localScale = standVector;
            moveSpeed = 15f;
            return true;
        }
        return false;
    }

    bool crouch ()
    {
        if (Input.GetKey(KeyCode.C))
        {
            playerTransform.localScale = crouchVector;
            moveSpeed = 4f;
            return true;
        }
        return false;
    }

    void walk ()
    {
        playerTransform.localScale = standVector;
        moveSpeed = 10f;
    }

    // public method for grapple to launch player
    public void launch(Vector3 launchForce)
    {
        this.launchForce = launchForce;
        isLaunched = true;
    }

    // UNITY PHYSICS FUNCTIONS
    //////////////////////////
    void FixedUpdate ()
    {
        if (jumped)
        {
            Debug.Log("WHATTUP");
            playerRigidBody.AddForce(jumpForceVector);
            jumped = false;
        }

        if (isLaunched)
        {
            playerRigidBody.AddForce(launchForce);
            isLaunched = false;
        }
    }

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.name == "ground")
        {
            isMidAir = false;
        }
    }

    void OnCollisionExit (Collision col)
    {
        if (col.gameObject.name == "ground")
        {
            isMidAir = true;
        }
    }
}
