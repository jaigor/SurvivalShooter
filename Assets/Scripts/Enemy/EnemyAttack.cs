using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;	// allows a period for each attack
    public int attackDamage = 10;			// amount of damaged made


    Animator anim;

	// references to the player
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;				// tells if the player can be attacked
    float timer;					// variable to keep everything sync


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }

	// function to call when an object collides with other collider
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player) 	// if collides with player
        {
            playerInRange = true;		// the player can be attacked
        }
    }

	// function to call when an object no longer collides with other collider
    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime; 	// time collected until now

		// if time is greater or equal than the time established between attacks and the player it is in range, and we are not dead
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)	
        {
            Attack ();			// executes the attack
        }

        if(playerHealth.currentHealth <= 0)		// if the player dies
        {
            anim.SetTrigger ("PlayerDead");		// changes the trigger of the animation
        }
    }


    void Attack ()
    {
        timer = 0f;			// resets the time in order to not attack each second

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
