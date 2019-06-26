using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static readonly int GRID_WIDTH = 4;
    public static readonly int GRID_HEIGHT = 4;
    public Entity[,] grid = new Entity[GRID_HEIGHT,GRID_WIDTH];
    public Entity BaseTile;

    // Start is called before the first frame update
    void Start()
    {
		FillEmptySpaces();
    }

    private void CreateNode(int x, int y)
    {
        var tileDimensions = BaseTile.GetComponent<SpriteRenderer>().bounds.size;
        var tile = Instantiate(BaseTile, new Vector3(x * tileDimensions.x - 4, y * tileDimensions.y - 2.5f, 1), new Quaternion()).GetComponent<Node>();
        Set(x, y, tile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Delete(int x, int y)
    {
        Destroy(grid[x, y].gameObject);
        //CreateNode(x, y);
    }
	public void FillEmptySpaces()
	{
		for (int y = 0; y <= grid.GetUpperBound(0); y++)
		{
			for (int x = 0; x <= grid.GetUpperBound(1); x++)
			{
				if (grid[x, y] == null)
				{
					CreateNode(x, y);
				}
			}
		}
	}

    public void Set(int x, int y, Entity obj)
    {
        grid[x, y] = obj;
        obj.Map = this;
        obj.X = x;
        obj.Y = y;
    }
	public void ActivateNodes()
	{
		foreach(var entity in grid)
		{
			entity.gameObject.SetActive(true);
		}
	}
	public void DeactivateNodes()
	{
		foreach (var entity in grid)
		{
			if (entity is Node)
			{
				entity.gameObject.SetActive(false);
			}
		}
	}
}