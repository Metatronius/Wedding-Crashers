using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : Entity
{
    public Sprite HoverSprite;
    public Sprite ActiveSprite;
    public Sprite DefaultSprite;
    public GameObject BasicTower;
	private ResourceManager resourceManager;


    
    // Start is called before the first frame update
    void Start()
    {
		resourceManager = FindObjectOfType<ResourceManager>();

	}

	// Update is called once per frame
	void Update()
    {

    }

    void OnMouseEnter()
    {
        this.GetComponent<SpriteRenderer>().sprite = HoverSprite;
    }

    void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().sprite = DefaultSprite;
    }

    private void OnMouseDown()
    {
        this.GetComponent<SpriteRenderer>().sprite = ActiveSprite;

    }

    private void OnMouseUpAsButton()
    {
		var cost = BasicTower.GetComponent<Tower>().Cost;

		if (resourceManager.ResourceCount >= cost)
		{
			Map.Set(X, Y, Instantiate(BasicTower, new Vector3(this.transform.position.x, this.transform.position.y, 1), this.transform.rotation).GetComponent<Entity>());
			resourceManager.ResourceCount -= cost;
			Destroy(this.gameObject);
		}
    }
}
