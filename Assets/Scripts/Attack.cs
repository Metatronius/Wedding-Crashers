using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int BaseAttackDamage;
    public float BaseAttackTime;
    public float AttackRange;

    private float AttackTime;
    private float AttackProgress;
    private bool InProgress;
    public Vector2 Direction; //(-1, 0) = Left, (1, 0) = Right

    private void DoAttack() // int bulletDamage, int bulletSpeed, Bullet bullet
    {
        var bullet = Instantiate(Bullet, this.transform.position + new Vector3(0, .3f * Mathf.Cos(System.DateTime.Now.Millisecond), -1), this.transform.rotation).GetComponent<Bullet>();
        bullet.Initialize(BulletDamage, BulletSpeed, new Vector2(-1, 0));

        return bullet;
    }
}