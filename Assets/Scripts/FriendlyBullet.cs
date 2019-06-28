using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : Bullet
{
 
    // Start is called before the first frame update
    
    void Start()
    {
    }

    // Update is called once per frame


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