using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : MonoBehaviour
{
    public int AttackDamage { get; set; }
    private float flipInterval = .1f;
    private float flipMeter = 0;

    // Start is called before the first frame update
    void Start()
    {
        AttackDamage = 1;
    }

    // Update is called once per frame
    void Update()
    {
		
        var xSpeed = Time.deltaTime * 5;
        this.gameObject.transform.Translate(xSpeed, 0, 0);

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
        var enemy = collider.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.Health -= AttackDamage;
            Destroy(this.gameObject);
        }
    }
}