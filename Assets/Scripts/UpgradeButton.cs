using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
	private RoundManager roundManager;
	private ResourceManager resourceManager;
	private Text buttonText;

	private Tower selectedTower { get; set; }

    // Start is called before the first frame update
    void Start()
    {
		gameObject.SetActive(false);
		roundManager = FindObjectOfType<RoundManager>();
		resourceManager = FindObjectOfType<ResourceManager>();
		buttonText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
		GetComponents<MonoBehaviour>();
    }

	public void Select(Tower t)
	{
		Deselect();

		gameObject.SetActive(true);
		buttonText.text = $"UPGRADE: {t.UpgradeCost} HEKS";
		selectedTower = t;

		if (selectedTower != null)
		{
			selectedTower.SetSelected(true);
		}
	}

	public void Deselect()
	{
		if(selectedTower != null)
		{
			selectedTower.SetSelected(false);
		}

		gameObject.SetActive(false);
		selectedTower = null;
	}

	public void Upgrade()
	{
		if (resourceManager.ResourceCount >= selectedTower.UpgradeCost)
		{
			resourceManager.ResourceCount -= selectedTower.UpgradeCost;
			selectedTower.UpgradeSpeed();
			Deselect();
		}
	}
}
