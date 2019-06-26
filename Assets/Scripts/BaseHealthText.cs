using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthText : MonoBehaviour
{
    private Text displayText;
    private Base throne;
    // Start is called before the first frame update
    void Start()
    {
        throne = FindObjectOfType<Base>();
        displayText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        displayText.text = $"Cake:\n{throne.Health}/{throne.HP}";
    }
}
