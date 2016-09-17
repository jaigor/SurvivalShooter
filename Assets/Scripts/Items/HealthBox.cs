using UnityEngine;
using System.Collections;

public class HealthBox : MonoBehaviour 
{
	public int lifeToPlus = 30;
	public float timeBetweenHealth = 0.6f;
	GameObject player;
	PlayerHealth playerHealth;

	AudioSource hitAudio;
	bool isUsed;
	bool playerInRange;
	float timer;

	void Awake () 
	{
		//Debug.Log ("Instancia");
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		hitAudio = GetComponent<AudioSource> ();
	}

	// function to call when an object collides with other collider
	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player) 	// if collides with player
		{
			playerInRange = true;		// the player can be healed
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
		timer += Time.deltaTime; 

		if (isUsed) 
		{
			Destroy (gameObject, 0.5f);
		}

		if (timer >= timeBetweenHealth && playerInRange && playerHealth.currentHealth > 0 && playerHealth.currentHealth != playerHealth.startingHealth)
		{
			Heal ();
		}
	}

	void Heal()
	{
		timer = 0f;
		hitAudio.Play ();
		playerHealth.TakeHealth (lifeToPlus);
		isUsed = true;
	}
}
