using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;	 // how fast enemy sinks to the floor
    public int scoreValue = 10;		 // how much enemy increases our score
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;			// moment when enemy starts to sink


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();	// component searched in component children
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);	// move the transform in a negative up direction (sink)
        }
    }

	// it is passed amount of damage made and the coordinates for the hitPoint
    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
		hitParticles.transform.position = hitPoint;	// generates (moves) the particles animation into the hitpoint
        hitParticles.Play();
		hitParticles.startLifetime = hitParticles.startLifetime;	// make in order to gunParticles loops

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

		capsuleCollider.isTrigger = true;	// deactivates the enemy as an obstacle (no collider)

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }

	// it is public because we called it from an animation
    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;		// in order to the object can sink
        GetComponent <Rigidbody> ().isKinematic = true;		// colliders dont affect to the enemy 
        isSinking = true;
        ScoreManager.score += scoreValue;		// updates the scores adding the points for killing enemy
        Destroy (gameObject, 2f);				// delete object from the scene
    }
}
