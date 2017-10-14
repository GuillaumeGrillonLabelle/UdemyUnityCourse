using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Health))]
public class Defender : MonoBehaviour
{
	private const string ANIM_TRIGGER_HIT = "HitTrigger";
	private const string ANIM_PARAMETER_IS_ATTACKING = "IsAttacking";
	private const string PROJECTILES_PARENT_NAME = "Projectiles";

	public bool canBeJumpedOver;
	public bool hasHitAnimation;
	public GameObject projectile;
	public Transform projectileOffset;

	public event System.EventHandler OnDeath;

	private Animator animator;
	private Health hp;

	private bool canAttack;
	private bool isAttacking;
	private GameObject projectilesParent;

	void Start()
	{
		animator = GetComponent<Animator>();
		hp = GetComponent<Health>();
		hp.OnDeath += On_Self_Death;

		if (projectileOffset == null)
			projectileOffset = transform;

		canAttack = projectile != null;

		projectilesParent = GetOrCreateParentObject();
	}

	private bool IsAttacking
	{
		get { return isAttacking; }
		set
		{
			if (isAttacking == value)
				return;
			isAttacking = value;
			animator.SetBool(ANIM_PARAMETER_IS_ATTACKING, isAttacking);
		}
	}

	private void On_Self_Death(object sender, System.EventArgs e)
	{
		hp.OnDeath -= On_Self_Death;

		//print(gameObject.name + " is dead.");

		var ev = OnDeath;
		if (ev != null)
			ev(this, System.EventArgs.Empty);

		Destroy(gameObject);
	}

	void Update()
	{
		if (canAttack)
		{
			IsAttacking = AreThereAttackersInMyLane();
		}
	}

	private bool AreThereAttackersInMyLane()
	{
		return FindObjectsOfType<Attacker>().Any(a => Mathf.Approximately(a.transform.position.y, transform.position.y));
	}

	public void ApplyDamage(float damage)
	{
		hp.ApplyDamage(damage);

		if (hasHitAnimation)
		{
			animator.SetTrigger(ANIM_TRIGGER_HIT);
		}
	}

	public void FireProjectile()
	{
		if (projectile != null)
		{
			var p = Instantiate(projectile);
			p.transform.parent = projectilesParent.transform;
			p.transform.position = projectileOffset.position;
		}
	}

	private static GameObject GetOrCreateParentObject()
	{
		var p = GameObject.Find(PROJECTILES_PARENT_NAME);
		if (p == null)
		{
			p = new GameObject(PROJECTILES_PARENT_NAME);
		}

		return p;
	}
}
