using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	Transform player;
    NavMeshAgent nav;
    EnemyHealth enemyHealth;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();

        enemyHealth = GetComponent<EnemyHealth>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (enemyHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
	}

}
