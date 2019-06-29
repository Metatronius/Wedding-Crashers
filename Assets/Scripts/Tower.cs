using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Tower : Entity
{
	public List<Sprite> Sprites;
	public Sprite HighlightSprite;
    public int BulletDamage { get; set; }
    public int BulletSpeed;
	public int Health { get; set; }
	public float AttackProgress { get; set; }
	public float AttackTime { get; set; }
    public int Damage;
	public GameObject TowerBullet;
	public float Cooldown;
	public float Range;
	public float AttackRange { get; set; }
	public int BaseHealth;
	public int Cost;
	public int SpeedLevel;
    public int DamageLevel;
	public int UpgradeCostPerLevel;
	public bool IsSelected { get; set; }

	public Attack TowerAttack {get;set;}
	public Health TowerHealth {get;set;}

	public int UpgradeCost
	{
		get
		{
			return (1 + SpeedLevel + DamageLevel) * UpgradeCostPerLevel;
		}
	}

	private int spriteIndex = 0;
	private UpgradeButton upgradeButton;
	private RoundManager roundManager;
	private bool selected;

    // Start is called before the first frame update
    protected void Start()
    {
        Health = BaseHealth;
        AttackTime = Cooldown;
        AttackProgress = AttackTime;
        AttackRange = Range;
        BulletDamage = Damage;
		upgradeButton = Resources.FindObjectsOfTypeAll<UpgradeButton>().Single();
		roundManager = FindObjectOfType<RoundManager>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if(Health <= 0)
        {
            Map.Delete(this.X, this.Y);
        }
		var raycastResults = Physics2D.RaycastAll(this.transform.position, new Vector2(1, 0), AttackRange, ~0);
		if(raycastResults.Any(r => r.collider != null && r.collider.gameObject.GetComponent<Enemy>() != null)) // && r.collider.gameObject.GetComponent<Enemy>() != null)
		{

            this.AttackProgress += Time.deltaTime;
            if (this.AttackProgress >= AttackTime)
            {
				// shoot the bullet
				Animate();
                ShootBullet();
                //Instantiate(TowerBullet, this.transform.position + new Vector3(0, .3f * Mathf.Sin(System.DateTime.Now.Millisecond), -2), this.transform.rotation);

            }

            this.AttackProgress %= AttackTime;
        }
    }
    private FriendlyBullet ShootBullet()
    {
        var bullet = Instantiate(TowerBullet, this.transform.position + new Vector3(0, .3f * Mathf.Sin(System.DateTime.Now.Millisecond), -1), this.transform.rotation).GetComponent<FriendlyBullet>();
        bullet.Initialize(BulletDamage, BulletSpeed, new Vector2(1, 0));

        return bullet;
    }

	public void OnMouseEnter()
	{
		if(roundManager.IsRoundOver)
		{
			this.gameObject.GetComponent<SpriteRenderer>().sprite = HighlightSprite;
		}
	}

	public void OnMouseExit()
	{
		if (roundManager.IsRoundOver && !selected)
		{
			this.gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[0];
		}
	}

	public void SetSelected(bool selected)
	{
		this.selected = selected;

		if(selected)
		{
			this.gameObject.GetComponent<SpriteRenderer>().sprite = HighlightSprite;
		}
		else
		{
			this.gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[0];
		}
	}

	public void OnMouseUpAsButton()
	{
		upgradeButton.Select(this);
	}

	private void Animate()
	{
		spriteIndex = (spriteIndex + 1) % Sprites.Count;
		this.gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[spriteIndex];
	}

	public void Refresh()
	{
		Health = BaseHealth;
		this.gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[0];
	}

	public void UpgradeSpeed()
	{
		SpeedLevel++;
		ShowUpgrade();
		BaseHealth *= 2;
		AttackTime = Cooldown/(SpeedLevel * .8f);
		Refresh();
	}

    public void UpgradeDamage()
    {
        DamageLevel++;
        ShowUpgrade();
        BaseHealth *= 2;
        BulletDamage += Damage;
    }

	public void ShowUpgrade()
	{
		upgradeButton.Select(this);
	}
}