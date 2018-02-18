using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Vector3 movement;
    Rigidbody playerRigidBody;
    float camRayLength = 100f;
    int floorMask;
    public float moveSpeed;

	//Animattion
	private Animator anim;

    private void Awake()
    {	
		//Animation
		anim = GetComponent<Animator> ();

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

		//Animation
		AnimationUpdate(moveHorizontal, moveVertical);
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

	void AnimationUpdate(float moveHorizontal, float moveVertical){
		UpdateWalkingAnimation (moveHorizontal, moveVertical);
		ResetSpecialAnimation ();
		UpdateAnimation ();
	}

	void ResetSpecialAnimation(){
		anim.SetInteger ("SwipeCtrl", 0);
		anim.SetInteger ("AtkCtrl", 0);
		anim.SetInteger ("FlyCtrl", 0);
	}

	void UpdateWalkingAnimation(float mHorizontal, float mVertical)
	{
		if (mHorizontal != 0) {
			anim.SetInteger ("IdleCtrl", 0);
			anim.SetInteger ("WalkingCtrl", 1);
		} else {
			anim.SetInteger ("IdleCtrl", 1);
			anim.SetInteger ("WalkingCtrl", 0);
		}

	}
	void UpdateAnimation()
	{
		if(Input.GetKey(KeyCode.Alpha3)){
			anim.SetInteger ("SwipeCtrl", 1);
		}
		if(Input.GetKey(KeyCode.Mouse0)){
			anim.SetInteger ("AtkCtrl", 1);
		}
		if(Input.GetKey(KeyCode.Alpha2)){
			anim.SetInteger ("FlyCtrl", 1);
		}

	}


}
