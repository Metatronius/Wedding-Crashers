using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceSpendingButton : MonoBehaviour
{
    private ResourceManager resourceManager;
    public int Price { get; set; }
    public int BasePrice;

    // Start is called before the first frame update
    void Start()
    {
        Price = BasePrice;
    }
    void Press(IUpgradable e)
    {

        if(resourceManager.Spend(Price))
        {
            e.Upgrade();
        }
    }
}