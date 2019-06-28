using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
	private Button roundButton;
	public int RoundNumber { get; set; }
	public  bool IsRoundOver { get; set; }

	public  float RoundTimer { get; set; }
	public float RoundDuration;
	private Map map { get; set; }
	private bool gameOver;
	private Button retryButton;
	private UpgradeButton upgradeButton;
    public bool IsTimerZero { get; set; }

	// Start is called before the first frame update
	void Start()
    {
		retryButton = FindObjectsOfType<Button>().Single(button => button.CompareTag("RetryButton"));
		upgradeButton = Resources.FindObjectsOfTypeAll<UpgradeButton>().Single();
		retryButton.gameObject.SetActive(false);
		IsRoundOver = true;
        IsTimerZero = true;
		RoundNumber = 1;
		RoundTimer = RoundDuration;
		roundButton = FindObjectsOfType<Button>().Single(button => (button.CompareTag("RoundButton")));
		map = FindObjectOfType<Map>();
	}

    // Update is called once per frame
    void Update()
    {
		if(!IsRoundOver)
		{
			RoundTimer -= Time.deltaTime;
            if(RoundTimer <= 0)
            {
                IsTimerZero = true;
            }
		    if(IsTimerZero && FindObjectOfType<Enemy>() == null && !gameOver)
		    {
		    	EndRound();
		    	RoundTimer = RoundDuration;
                IsRoundOver = true;
            }
		}
	}
	public void StartRound()
	{
		IsRoundOver = false;
        IsTimerZero = false;
		roundButton.gameObject.SetActive(false);
		upgradeButton.Deselect();
		upgradeButton.gameObject.SetActive(false);
		map.DeactivateNodes();
	}

	public void EndRound()
	{
		upgradeButton.Deselect();
		roundButton.gameObject.SetActive(true);
		RoundNumber++;
		var friendlyBullets = FindObjectsOfType<FriendlyBullet>();
		var towers = FindObjectsOfType<Tower>();
		var enemyBullets = FindObjectsOfType<Bullet>();
		foreach(var bullet in friendlyBullets)
		{
			Destroy(bullet.gameObject);
		}
		foreach (var tower in towers) 
		{
			tower.Refresh();
		}
		foreach (var bullet in enemyBullets)
		{
			Destroy(bullet.gameObject);
		}
		map.FillEmptySpaces();
		map.ActivateNodes();
	}

	public void GameOver()
	{
		gameOver = true;
		FindObjectOfType<EnemySpawnCoordinator>().BeginMidnightMochiMode();
		retryButton.gameObject.SetActive(true);
	}
}
