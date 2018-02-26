using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Import AI from unity engine for access to the navmesh component
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    //Public variables can be declaired here or in editor.
    public int startingHealth = 100;
    public int currentHealth;
    //How fast enemy sinks into ground when dead.
    public float sinkSpeed = 2.5f;

    //Sphere collider is used as a trigger to detect if
    //we are in range of the enemy.
    SphereCollider sphereCollider;
    bool isDead;
    bool isSinking;

	//Animation Controll
	Animator anim;

    //Awake is similar to start but runs more often to avoid glitches from pausing or
    //incorrect initalization.
    private void Awake()
    {
        /*
         * Get component is used to reference objects that are connected
         * to the same gameObject that the script is connected to.
         * Here we also have a sphere collider on our enemy
         * so we can get access to it in code by saying
         * GetComponent<ITEM_TO_ACCESS>();
         */
        sphereCollider = GetComponent<SphereCollider>();
        isDead = false;
        isSinking = false;
        currentHealth = startingHealth;

		//AnimationCode
		anim = GetComponent <Animator> ();
    }
	
	// Update is called once per frame
	void Update () {
		if(isSinking)
        {
            /*
             * If we are sinking then we move downwards.
             * We multiply smoothing by Time.deltaTime because we want to average out the smoothing.
             * Time is a constant among all computers so this will help slower and faster computers perform similar
             */
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
	}
    /// <summary>
    /// Take damage is used by the player script to attack the enemy this script is attached to.
    /// </summary>
    /// <param name="amount"> Amount of damage given to enemy </param>
    /// <param name="hitPoint"> The exact point the enemy is hit
    /// This is used to apply affects on dmg like blood. </param>
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if(isDead)
        {
            return;
        }
        //Remove damage from health
        currentHealth -= amount;
        //If health is 0 or below run death method
        if(currentHealth <= 0)
        {
            Death();
        }
    }
    /// <summary>
    /// Run when enemies are dead triggers a death boolean
    /// Make sphereCollider trigger so the player can walk through dead bodies
    /// Run StartSinking method.
    /// </summary>
    void Death()
    {
		//animation Trigger
		AnimationChange ();

        isDead = true;
        sphereCollider.isTrigger = true;
        StartSinking();
    }

    /// <summary>
    /// Get the navmesh component and disable it to release the resources it uses
    /// Make the body kinematic using ragdoll physics
    /// Set sinking variable to true
    /// </summary>
    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        //After 2 seconds destroy object this script is attached to.
        Destroy(gameObject, 2f);
    }


	/// <summary>
	/// Animations change. Activate the death animation
	/// </summary>
	void AnimationChange (){
		//AnimationCode
		//Trigger DeathAnimation
		print("Death");
		anim.SetInteger("WalkingCtrl", 0);
		anim.SetTrigger ("DeathTrigger");
		//WaitForSecondsRealtime (2f);

	}
}
