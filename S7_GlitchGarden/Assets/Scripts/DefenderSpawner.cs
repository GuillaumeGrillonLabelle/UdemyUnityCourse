using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
	public DefendersSelection defendersSelection;

	private GameObject defendersParent;
	private const string DEFENDERS_PARENT_NAME = "Defenders";

	void Start()
	{
		defendersParent = GetOrCreateParentObject();
	}

	private void OnMouseDown()
	{
		if (defendersSelection.Selected != null)
		{
			var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos = new Vector3((int)(pos.x + 0.5f), (int)(pos.y + 0.5f), 0);
			var d = Instantiate(defendersSelection.Selected.defender, defendersParent.transform);
			d.transform.position = pos;
			d.name += " " + d.GetInstanceID();
		}
	}

	private static GameObject GetOrCreateParentObject()
	{
		var p = GameObject.Find(DEFENDERS_PARENT_NAME);
		if (p == null)
		{
			p = new GameObject(DEFENDERS_PARENT_NAME);
		}

		return p;
	}
}
