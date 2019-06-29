using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	public int ResourceCount;

	public bool Spend(int amount)
	{
		if(ResourceCount >= amount)
		{
			ResourceCount -= amount;
			return true;
		}

		return false;
	}

	public void Gain(int amount)
	{
        ResourceCount += amount;
	}
}
