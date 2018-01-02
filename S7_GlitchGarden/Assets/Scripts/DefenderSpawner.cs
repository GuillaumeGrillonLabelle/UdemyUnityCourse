using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
	public DefendersSelection defendersSelection;

	private GameObject defendersParent;
	private const string DEFENDERS_PARENT_NAME = "Defenders";
    private StarDisplay starDisplay;

	void Start()
	{
		defendersParent = GetOrCreateParentObject();
        starDisplay = FindObjectOfType<StarDisplay>();
	}

	private void OnMouseDown()
	{
		if (defendersSelection.Selected != null)
		{
            var defenderPrefab = defendersSelection.Selected.defender;
            var defender = defenderPrefab.GetComponent<Defender>();
            if (starDisplay.UseStars(defender.defenderCost) == StarDisplay.Status.SUCCESS)
            {
                InstantiateDefender(defenderPrefab);
            }
            else
            {
                Debug.Log("Insufficient stars");
            }
        }
	}

    private void InstantiateDefender(GameObject defenderPrefab)
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector3((int)(pos.x + 0.5f), (int)(pos.y + 0.5f), 0);
        var d = Instantiate(defenderPrefab, defendersParent.transform);
        d.transform.position = pos;
        d.name += " " + d.GetInstanceID();
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
