using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public string playerName = "Player";
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    GameObject player;
    PlayerHealth playerHealth;
    bool playerInRange;
    float timer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(playerName);
        playerHealth = player.GetComponent<PlayerHealth>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }
	}

    void Attack()
    {
        timer = 0f;
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }
}
