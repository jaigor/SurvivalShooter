using UnityEngine;
using System.Collections;


public class AlliesManager : MonoBehaviour {

	public GameObject allyToCall;
	public float timeToKill = 20.0f;
	public int scoreToAchieve = 20;

	float timer;
	Transform player;
	PlayerHealth playerHealth;
	GameObject scoreText;
	GameObject allyToSearch;
	private GameObject ally;

	Quaternion direction = new Quaternion(0f,0f,0f,0f);
	Vector3 location = new Vector3(0f,0f,0f);

	bb8Movement bb8Movement;

	void Start ()
	{
		timer = 0.0f;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		scoreText = GameObject.FindGameObjectWithTag ("ScoreManager");
		CallAlly ();
	}

	void CallAlly ()
	{
		ally = (GameObject) Instantiate (allyToCall, location, direction);
		bb8Movement = ally.GetComponent<bb8Movement> ();
		ally.SetActive(false);
	}

	void Update ()
	{
		timer += Time.deltaTime;
		spawnController ();

		if (playerHealth.currentHealth > 0 &&  
			ScoreManager.score >= scoreToAchieve)
		{
			scoreToAchieve += 100;
			timer = 0.0f;
			ally.SetActive(true);
		}
	}

	void spawnController()
	{
		if (timer > timeToKill &&
			ally.activeSelf == true)
		{
			ally.SetActive(false);
			//bb8Movement.clearColliders();
			bb8Movement.FindEnemy();
		}
	}
		
}