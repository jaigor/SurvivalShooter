using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
	// player health variables
	[Range(0,200)]
    public int startingHealth = 100; // default value
	public int currentHealth;
    // references to the HUD components
	public Slider healthSlider;
    public Image damageImage;

    public AudioClip deathClip;
    // animation shown on screen when takes damage
	public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;	// movement script asociated with our player
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;		// instance the player with the starting health stablished
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;	// shows flash on screen
        }
        else
        {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);	// interpolates between damage color and transparent (clear)
        }
        damaged = false;					// when the damage ends the flash animation it is reset to false
    }

	// sends the amount of damage to the object
    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;	// substracts damage from currenthealth

		healthSlider.value = ((float) currentHealth/startingHealth) * healthSlider.maxValue;	// updates the health HUD slider (by percentage) 

        playerAudio.Play ();	// plays audio from audio source component

        if(currentHealth <= 0 && !isDead)	// set player dead when health is less than 0 and it is not already dead
        {
            Death ();
        }
    }

	public void TakeHealth(int amount)
	{
		currentHealth += amount;
		if (currentHealth > 100) {
			currentHealth = 100;
		} else {
			currentHealth = currentHealth;
		}

		healthSlider.value = ((float) currentHealth/startingHealth) * healthSlider.maxValue;	// updates the health HUD slider (by percentage)
	}


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;	// changes the clip from the audio source
        playerAudio.Play ();

        playerMovement.enabled = false;	// stops getting inputs for player movement
        playerShooting.enabled = false;

		if (Input.GetKeyDown (KeyCode.Mouse0)) 
		{
			RestartLevel ();
		}
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
