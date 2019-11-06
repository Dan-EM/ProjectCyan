using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public int health;
    public int healthLevel;

    public Image[] healthBar;
    public Sprite health3;
    public Sprite health2;
    public Sprite health1;
    //for invincibility. Maybe change transparency of sprite eventually too    NOT USED YET
    private int timer;
    private bool isInvincible;//i say may him invincible after danage and put a timer;

    // Use this for initialization
    void Start () {

        healthBar[0].enabled = true;
        healthBar[1].enabled = false;
        healthBar[2].enabled = false;
        healthBar[3].enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        //in the event it tries to go over max health
        if (health > healthLevel)
        {
            health = healthLevel;
        }

        //crappy code to decrease health
        if(health == 3)
        {
            healthBar[0].enabled = true;
            healthBar[1].enabled = false;
            healthBar[2].enabled = false;
            healthBar[3].enabled = false;

        }
        else if (health == 2)
        {
            healthBar[0].enabled = false;
            healthBar[1].enabled = true;
            healthBar[2].enabled = false;
            healthBar[3].enabled = false;
        }
        else if (health == 1)
        {
            healthBar[0].enabled = false;
            healthBar[1].enabled = false;
            healthBar[2].enabled = true;
            healthBar[3].enabled = false;
        }
        else if (health == 0)
        {
            healthBar[0].enabled = false;
            healthBar[1].enabled = false;
            healthBar[2].enabled = false;
            healthBar[3].enabled = true;
        }

	}
}
