using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnCoordinator : MonoBehaviour
{

	private EnemySpawner[] spawners;
	private RoundManager roundManager;
	private bool mochi; //game over

	private float timeSinceLastSpawn { get; set; }

	private int difficultyLevel
	{
		get
		{
			return (roundManager.RoundNumber);
		}
	}
	
	private float spawnInterval
	{
		get
		{
			return mochi ? .04f : 2f / difficultyLevel;
		}
	}

	public EnemySpawner Spawner;
	public List<Enemy> Enemies;
    

    // Start is called before the first frame update
    void Start()
    {
		mochi = false;
        timeSinceLastSpawn = 0;
		roundManager = FindObjectOfType<RoundManager>();

		spawners = new EnemySpawner[Map.GRID_HEIGHT];

        for (int i = 0; i < Map.GRID_HEIGHT; i++)
        {
            spawners[i] = Instantiate(Spawner, new Vector3(11, i*-1.62f + 2.35f, -1), new Quaternion()).GetComponent<EnemySpawner>();
        }
    }

	// Update is called once per frame
	void Update()
	{
		if ( !roundManager.IsTimerZero || mochi)
		{
			timeSinceLastSpawn += Time.deltaTime;

			if (timeSinceLastSpawn >= spawnInterval)
			{
				var randomNumberGenerator = new System.Random();
				// select random spawner
				var spawner = spawners[randomNumberGenerator.Next(Map.GRID_HEIGHT)];

				// select random enemy
				var enemyToSpawn = Enemies[randomNumberGenerator.Next(Enemies.Count)];

				// spawn selected enemy from selected spawner
				spawner.Spawn(enemyToSpawn);

				timeSinceLastSpawn %= spawnInterval;
			}
			

		}
	}

	public void BeginMidnightMochiMode()
	{
		mochi = true;
	}
}