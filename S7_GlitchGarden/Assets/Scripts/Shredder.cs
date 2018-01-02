using UnityEngine;

public class Shredder : MonoBehaviour
{
	private void Start()
	{
	}

	protected void OnTriggerEnter2D(Collider2D other)
	{
		Destroy(other.gameObject);
	}

	private void OnDrawGizmos()
	{
		var triggerBox = GetComponent<BoxCollider2D>();
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, triggerBox.bounds.size);
	}
}
