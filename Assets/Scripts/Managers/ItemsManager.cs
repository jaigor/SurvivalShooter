using UnityEngine;

public class ItemsManager : MonoBehaviour
{
	public PlayerHealth playerHealth;
	public GameObject healthPack;
	public float spawnTime = 30.0f;		// time between a healthbox spawns
	Quaternion direction = new Quaternion(0f,0f,0f,0f);

	void Start ()
	{
		// invokes method spawn, with the inital and the repeated time established
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}


	void Spawn ()
	{
		if(playerHealth.currentHealth <= 0f)		// if player it is dead
		{
			return;
		}

		float spawnPointXIndex = Random.Range (-35f, 35f);	// random number chosen from the elements of the spawn Array
		float spawnPointZIndex = Random.Range (-35f, 35f);
		Vector3 posibleLocation = new Vector3 (spawnPointXIndex, 0f, spawnPointZIndex);

		NavMeshHit hit;
		if (NavMesh.SamplePosition(posibleLocation, out hit, 1.5f, NavMesh.AllAreas))
		{
			Vector3 goodLocation = hit.position;
			Instantiate (healthPack, goodLocation, direction);
		}

	}


}
