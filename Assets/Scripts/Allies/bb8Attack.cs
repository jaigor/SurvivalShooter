using UnityEngine;
using System.Collections;

public class bb8Attack : MonoBehaviour {

	public float timeBetweenAttacks = 0.3f;	// allows a period for each attack
	public int attackDamage = 20;			// amount of damaged made

	// references to the enemy
	GameObject enemy;
	EnemyHealth enemyHealth;
	bool enemyInRange;				// tells if the enemy can be attacked
	float timer;					// variable to keep everything sync


	// function to call when an object collides with other collider
	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.layer == 9) 	// if collides with player
		{
			enemyInRange = true;		// the player can be attacked
			enemy = other.gameObject;
			enemyHealth = enemy.GetComponent<EnemyHealth>();
		}
	}

	// function to call when an object no longer collides with other collider
	void OnTriggerExit (Collider other)
	{
		if(other.gameObject.layer == 9)
		{
			enemyInRange = false;
		}
	}


	void Update ()
	{
		timer += Time.deltaTime; 	// time collected until now

		// if time is greater or equal than the time established between attacks and the player it is in range, and we are not dead
		if(timer >= timeBetweenAttacks && enemyInRange && enemyHealth.currentHealth > 0)	
		{
			Attack ();			// executes the attack
		}
	}


	void Attack ()
	{
		timer = 0f;			// resets the time in order to not attack each second
		enemyHealth.TakeDamage (attackDamage, enemy.transform.position);
	}
}