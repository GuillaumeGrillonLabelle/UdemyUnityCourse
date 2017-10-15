using UnityEngine;


public enum MovementType { Stopped, Walk, Jump }

[RequireComponent(typeof(Animator), typeof(Health))]
public class Attacker : MonoBehaviour
{
	private const string ANIM_TRIGGER_JUMP = "JumpTrigger";
	private const string ANIM_PARAMETER_IS_ATTACKING = "IsAttacking";

	public float walkingSpeed;
	public bool canJump;
	public float jumpingSpeed;
	public float damage;
	public float onTargetDeathStopAttackingAfter = .1f;
	public float seenEverySeconds;

	public event System.EventHandler OnDeath;

	private float currentSpeed;
	private Defender currentTarget;
	private Defender CurrentTarget
	{
		get { return currentTarget; }
		set
		{
			if (currentTarget == value)
				return;

			if (currentTarget != null)
			{
				currentTarget.OnDeath -= On_Target_Death;
			}

			currentTarget = value;

			if (currentTarget != null)
			{
				currentTarget.OnDeath += On_Target_Death;
			}
		}
	}

	private Animator animator;
	private Health hp;

	void Start()
	{
		animator = GetComponent<Animator>();
		hp = GetComponent<Health>();
		hp.OnDeath += On_Self_Death;
	}

	private void On_Self_Death(object sender, System.EventArgs e)
	{
		hp.OnDeath -= On_Self_Death;

		//print(gameObject.name + " is dead.");

		var ev = OnDeath;
		if (ev != null)
			ev(this, System.EventArgs.Empty);

		CurrentTarget = null;

		Destroy(gameObject);
	}

	public void ApplyDamage(float damage)
	{
		hp.ApplyDamage(damage);
	}

	void Update()
	{
		transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
	}

	public void SetMovement(MovementType m)
	{
		switch (m)
		{
			case MovementType.Stopped:
				currentSpeed = 0;
				break;
			case MovementType.Walk:
				currentSpeed = walkingSpeed;
				break;
			case MovementType.Jump:
				if (!canJump)
					throw new System.Exception(name + " cannot jump.");
				currentSpeed = jumpingSpeed;
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		//print(name + " touched " + other.name);

		var defender = other.gameObject.GetComponent<Defender>();

		if (defender == null)
			return;

		if (canJump && defender.canBeJumpedOver)
		{
			animator.SetTrigger(ANIM_TRIGGER_JUMP);
		}
		else
		{
			Attack(defender);
		}
	}

	private void Attack(Defender defender)
	{
		CurrentTarget = defender;
		animator.SetBool(ANIM_PARAMETER_IS_ATTACKING, true);
	}

	public void StrikeCurrentTarget()
	{
		if (CurrentTarget != null)
		{
			//print(name + " striked " + CurrentTarget.name + " for " + damage + " damage.");

			CurrentTarget.ApplyDamage(damage);
		}
	}

	private void On_Target_Death(object sender, System.EventArgs e)
	{
		CurrentTarget = null;

		if (onTargetDeathStopAttackingAfter <= 0)
		{
			StopAttacking();
		}
		else
		{
			Invoke("StopAttacking", onTargetDeathStopAttackingAfter);
		}
	}

	private void StopAttacking()
	{
		animator.SetBool(ANIM_PARAMETER_IS_ATTACKING, false);
	}
}
