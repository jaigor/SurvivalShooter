using UnityEngine;
using System.Collections;


public class bb8Movement : MonoBehaviour {

	public float radiusOfPlayer = 10f;
	public LayerMask _collisionLayer;

	Transform player;
	PlayerHealth playerHealth;
	NavMeshAgent nav;
	AudioSource audio;
	Animator anim;

	Collider[] hitColliders;

	void Awake () 
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth> ();
		nav = GetComponent<NavMeshAgent> ();
		audio = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
	}

	void Update () 
	{
		if (playerHealth.currentHealth > 0)
		{
			FindEnemy ();
		}
	}

	public void FindEnemy()
	{
		//Collider[] hitColliders = Physics.OverlapSphere(player.position, radiusOfPlayer, _collisionLayer); 
		hitColliders = Physics.OverlapSphere(player.position, radiusOfPlayer, _collisionLayer);

		if (hitColliders.Length == 0) 
		{
			Moving (false);
			return;
		}
		else 
		{
			for (int i = 0; i < hitColliders.Length; i++)
			{
				PursueEnemy(hitColliders[i].gameObject.transform.position);
			}
		}
	}

	public void clearColliders()
	{
		Debug.Log ("Clear");
		for (int i = 0; i > hitColliders.Length; i++)
		{
			hitColliders [i] = null;
		}
	}

	void PursueEnemy(Vector3 enemyPosition)
	{
		//Debug.Log ("Play");
		if (float.IsNaN(enemyPosition.x) ||
			float.IsNaN(enemyPosition.z)) 
		{
			return;
		} 
		else 
		{
			audio.Play ();
			nav.SetDestination (enemyPosition);
			Moving (true);
		}
	}

	void Moving(bool isMoving)
	{
		anim.SetBool ("isMoving", isMoving);
	}
}
