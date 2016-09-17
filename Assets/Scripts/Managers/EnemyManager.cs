using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;		// time between a monster spawns
    public Transform[] spawnPoints;		// array of the spawnPoint for the enemys


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

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);	// random number chosen from the elements of the spawn Array

		// clones the enemy with a random position and rotation from the spawnPoints array
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
