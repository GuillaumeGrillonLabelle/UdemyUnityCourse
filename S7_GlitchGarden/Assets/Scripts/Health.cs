using UnityEngine;

public class Health : MonoBehaviour
{
	public event System.EventHandler OnDeath;

	public float maxHealth;

	private float currentHealth;

	void Start()
	{
		currentHealth = maxHealth;
	}

	public void ApplyDamage(float damage)
	{
		currentHealth -= damage;

		if (currentHealth <= 0f)
		{
			//print("Health component of " + gameObject.name + " triggers death.");

			var ev = OnDeath;
			if (ev != null)
				ev(this, System.EventArgs.Empty);
		}
	}
}
