using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour {

	public int damage = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 20f;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
    float effectsDisplayTime = 0.2f;

    //Script is from unity tutorial use this to get a better understanding if comments aren't enough
    //https://unity3d.com/learn/tutorials/projects/survival-shooter/harming-enemies?playlist=17144 

    private void Awake()
    {
        //shootableMask is used later to tell the Raycast function whether the object that is hit is an enemy or not
        shootableMask = LayerMask.GetMask("Enemy");

        gunLine = GetComponent<LineRenderer>();

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //A timer to use for fire rate
        timer += Time.deltaTime;
        
        //Shoot if Fire1(Mouse Click) is pressed
        if (Input.GetButton("Fire2") && timer >= timeBetweenBullets)
        {
            // ... shoot the gun.
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects();
        }

    }

    public void DisableEffects()
    {
        // Disable the line renderer
        gunLine.enabled = false;
    }

    void Shoot()
    {
        //set the timer to use in update function
        timer = 0f;

        //enable gunline to draw a line
        gunLine.enabled = true;

        //set the position of the line wherever AttackDirect is
        gunLine.SetPosition(0, transform.position);

        //sets the ray to start at AttackDirect
        shootRay.origin = transform.position;

        //sets the direction of the ray to go towards the positive Z-axis of AttackDirect
        shootRay.direction = transform.forward;

        //If the ray hits an object with Enemy mask then true
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            //Get the health object the ray collided with if object is not an enemy then it returns null
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            
            //if enemy has a health then it takes dmg 
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage, shootHit.point);
            }
            //End the line at the object
            gunLine.SetPosition(1, shootHit.point);
        }
        //If object didnt hit an enemy
        else
        {
            //end line at the range we set
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
