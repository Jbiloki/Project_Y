using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Support : MonoBehaviour {

    /*Transform is a data type to get the location of an object in 3d space.
      Here we noted that it is a PUBLIC variable, so if you go to the unity inspector
      you will have the opportunity to select what it's target is.
    */
    public Transform target;
    //Smoothing is also public and can be declaired here or in the editor
    public float smoothing = 5f;
    /*Offset is a Vector3 type (Meaning it looks for 3d space point)
      It is declaired like such Vector3 variable_name = new Vector3(x coord, y coord, z coord)
      Here it is used to offset the camera from it's target
    */
    Vector3 offset;


	// Use this for initialization it is called at the start of a game
	void Start () {
        /*Our offset is our position in 3d space minus the target position.
         You will notice there is no object for the first transform.position.
         When this is the case it assumes the target is the object the script is attached to.
        */
        offset = transform.position - target.position;
	}
	
	// Fixed Update is different from update as it is called once every 20 frames
	void FixedUpdate () {
        //Get 3d posiiton of the target + the offset
        Vector3 targetCamPos = target.position + offset;
        /*Set our camera position to the new position
          Lerp is used for a smooth transition between two positions
          Here the two positions are our starting position, our new position, and then our smoothing
          We multiply smoothing by Time.deltaTime because we want to average out the smoothing
          Time is a constant among all computers so this will help slower and faster computers perform similar
        */
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
