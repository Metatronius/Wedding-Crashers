using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnTriggerEnter2D(Collider2D collision)
	{
		var rabbit = collision.gameObject.GetComponent<Enemy>();

		if(rabbit != null)
		{
			Destroy(rabbit.gameObject);
		}
	}
}
