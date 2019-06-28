using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private readonly float flipInterval = .1f;
	private float flipMeter = 0;

	public int AttackDamage;
	public float MoveSpeed;
	public Vector2 MoveDirection;

	public void Initialize(int damage, float speed,  Vector2 direction)
	{
		this.AttackDamage = damage;
		this.MoveDirection = direction;
		this.MoveSpeed = speed;
	}

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
     void Update()
    {
		
		this.gameObject.transform.Translate(MoveDirection.normalized * MoveSpeed * Time.deltaTime);

        flipMeter += Time.deltaTime;

        if (flipMeter >= flipInterval)
        {
            // fuck yes flip
            var sprite = this.GetComponent<SpriteRenderer>();
            sprite.flipY = !sprite.flipY;
        }

        flipMeter %= flipInterval;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var tower = collider.gameObject.GetComponent<Tower>();

        if (tower != null)
        {
            tower.Health -= AttackDamage;
            Destroy(this.gameObject);
        }
		else
		{
			var throne = collider.gameObject.GetComponent<Base>();
			
			if(throne != null)
			{
				Destroy(this.gameObject);
			}
		}
    }
}