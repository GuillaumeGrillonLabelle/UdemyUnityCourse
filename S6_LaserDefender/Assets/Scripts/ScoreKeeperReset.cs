using UnityEngine;

public class ScoreKeeperReset : MonoBehaviour
{

	void Start()
	{
		var sc = FindObjectOfType<ScoreKeeper>();
		if (sc) sc.Reset();
	}
}
