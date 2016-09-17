using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;				// damage of the bullets
    public float timeBetweenBullets = 0.15f;	// how can quickly bullets are
    public float range = 100f;					// distance for the range of bullets


    float timer;
    Ray shootRay;					// what it is what we hit
	RaycastHit shootHit;			// whatever it is what we hit
    int shootableMask;				// make sure we only hit shootable enemies
	int shootableScenarioMask;				// make sure we only hit shootable things
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f; // how long the effects are going to be viewable


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");	// tagged as Shootable
		shootableScenarioMask = LayerMask.GetMask ("Scenario");	// tagged as Scenario
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)  // Fire1 is in the mouse button
        {
			Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
			DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();
		gunParticles.startLifetime = gunParticles.startLifetime;	// make in order to gunParticles loops

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position); // set the line with index 0 and the Vector of the transform component

        shootRay.origin = transform.position;	// starter point for the ray
		shootRay.direction = transform.forward;	// forward used to draw direction to the line (in front of the player)

		// passing values of the object hit, if it hits sth executes if
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask) ||
			Physics.Raycast (shootRay, out shootHit, range, shootableScenarioMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)		// if we hit an enemy
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
			gunLine.SetPosition (1, shootHit.point);	// whatever we hit (blocks also) draws the line
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);	// if we dont hit sth draws the line until finished the range
        }
    }
}
