using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	private int spawnTotal;

	public GameObject spawnPoint;
	public GameObject enemyPrefab;
	public int spawnDelay;
	public float spawnRate;
	public int spawnAmount;


	// Use this for initialization
	void Start () {
		startSpawn ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void startSpawn(){
		spawnTotal = 0;
		InvokeRepeating ("Spawn", spawnDelay, spawnRate);
	}


	void stopSpawn(){
		CancelInvoke ();
		startSpawn ();
	}

	void Spawn(){

		Rigidbody Enemy;
		Enemy = Instantiate(enemyPrefab, spawnPoint.transform.position, transform.rotation) as Rigidbody;
		spawnTotal++;
		print (spawnTotal);
		if (spawnTotal >= spawnAmount) {
			stopSpawn();
		}

	}
}