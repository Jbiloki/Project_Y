using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Vector3 movement;
    Rigidbody playerRigidBody;
    float camRayLength = 100f;
    int floorMask;
    public float moveSpeed;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
    }
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement = movement.normalized * moveSpeed * Time.deltaTime;
        playerRigidBody.MovePosition(transform.position + movement);
        followMouse();
	}

    void followMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if(Physics.Raycast(camRay,out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
        }
    }
}
