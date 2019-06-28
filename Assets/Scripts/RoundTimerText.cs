using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimerText : MonoBehaviour
{
	private Text displayText;
	public int ResourceCount;
	private RoundManager roundManager;
	// Start is called before the first frame update
	void Start()
	{
		displayText = GetComponent<Text>();
		roundManager = FindObjectOfType<RoundManager>();

	}


	// Update is called once per frame
	void Update()
	{
		if (!roundManager.IsTimerZero)
		{
			displayText.text = $"Round {roundManager.RoundNumber}:\n{(int)roundManager.RoundTimer} Seconds";
		}
		else
		{
			displayText.text = $"Next Round:\n{roundManager.RoundNumber}";
		}
	}
}
