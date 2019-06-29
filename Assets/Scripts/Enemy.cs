using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float Cooldown;
	public int BaseHealth;
	public int BulletSpeed;
	public float EnemyOffset;
	public float MoveSpeed;
	public int Bounty;
	public Attack EnemyAttack;

    public int Health { get; set; }

    // Other Prefabs (passed in via unity ui)
    public GameObject Bullet;

    // Start is called before the first frame update
    protected void Start()
    {
        Health = BaseHealth;
        // AttackTime = Cooldown;
        // AttackRange = 3 + Random.value * (2 * EnemyOffset) - EnemyOffset;
    }

	// Update is called once per frame
	protected void Update()
	{
		if (Health <= 0)
		{
			FindObjectOfType<ResourceManager>().ResourceCount += Bounty;
			Destroy(this.gameObject);
		}

		// attack if we are in range of a tower, move otherwise
		RaycastHit2D[] results = new RaycastHit2D[Map.GRID_WIDTH];

		if (GetComponent<Collider2D>().Raycast(new Vector2(-1, 0), results, EnemyAttack.AttackRange, 1 << 8) > 0 && results.Any(r => r.collider != null && r.collider.gameObject.GetComponent<Tower>() != null))
		{
			EnemyAttack.Do(Time.deltaTime);

			this.AttackProgress += Time.deltaTime;
			if (this.AttackProgress >= AttackTime)
			{
				ShootBullet();
			}

			this.AttackProgress %= AttackTime;
		}
		else
		{
			var xSpeed = Time.deltaTime * -MoveSpeed;
			this.gameObject.transform.Translate(xSpeed, 0, 0);
		}
	}


	private Bullet ShootBullet()
	{
		var bullet = Instantiate(Bullet, this.transform.position + new Vector3(0, .3f * Mathf.Cos(System.DateTime.Now.Millisecond), -1), this.transform.rotation).GetComponent<Bullet>();
		bullet.Initialize(BulletDamage, BulletSpeed, new Vector2(-1, 0));

		return bullet;
	}
}