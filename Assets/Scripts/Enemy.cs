using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float Cooldown;
	public int HP;
	public int BulletDamage;
	public int BulletSpeed;
	public float EnemyOffset;
	public float MoveSpeed;

    public int Health { get; set; }
    private float AttackTime { get; set; }
    private float AttackProgress { get; set; }
    private float AttackRange { get; set; }
    private bool Attacking { get; set; }
	public int Bounty;
    

    // Other Prefabs (passed in via unity ui)
    public GameObject Bullet;

    // Start is called before the first frame update
    protected void Start()
    {
        Health = HP;
        AttackTime = Cooldown;
        AttackRange = 3 + Random.value * (2 * EnemyOffset) - EnemyOffset;
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

		if (GetComponent<Collider2D>().Raycast(new Vector2(-1, 0), results, AttackRange, 1 << 8) > 0 && results.Any(r => r.collider != null && r.collider.gameObject.GetComponent<Tower>() != null))
		{
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