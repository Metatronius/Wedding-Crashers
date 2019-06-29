using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResourceText : MonoBehaviour
{
    private Text displayText;
    private ResourceManager resourceManager;
    // Start is called before the first frame update
    void Start()
    {
        displayText = GetComponent<Text>();
        resourceManager = FindObjectOfType<ResourceManager>();
    }


    // Update is called once per frame
    void Update()
    {
        displayText.text = $"Hecks:\n{resourceManager.ResourceCount}";
    }
}
