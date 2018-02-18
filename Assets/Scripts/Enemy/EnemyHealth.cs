using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;

    SphereCollider sphereCollider;
    bool isDead;
    bool isSinking;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        isDead = false;
        isSinking = false;
        currentHealth = startingHealth;
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if(isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
	}

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if(isDead)
        {
            return;
        }
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        isDead = true;
        sphereCollider.isTrigger = true;
        StartSinking();
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
}
