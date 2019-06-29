using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    // Start is called before the first frame update
    public int BaseHealth;
    public int Health {get; set;}
	public List<Sprite> Sprites;
    void Start()
    {
        Health = BaseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var enemy = collider.gameObject.GetComponent<Enemy>();

        if(enemy != null)
        {
            this.Health--;
            Destroy(enemy.gameObject);


			if (Health <= 0)
			{
				FindObjectOfType<RoundManager>().GameOver();
				Destroy(this.gameObject);
			}
			else
			{
				this.gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[(int)(Health / (float)BaseHealth * Sprites.Count)];
			}
        }
    }
}
