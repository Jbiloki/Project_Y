using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    PlayerMovement playerMovement;
    bool isDead;
    bool damaged;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = maxHealth;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
	}

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        /*if(currentHealth <= 0 && !isDead)
        {
            Death();
        }*/
    }
}
