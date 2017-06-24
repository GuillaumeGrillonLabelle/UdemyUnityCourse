using System.Linq;
using UnityEngine;

public class Brick : MonoBehaviour
{
	public Sprite[] hitSprites;
	public AudioClip crackSound;
	public float crackSoundVolume;
	public GameObject smoke;

	private int timesHit;
	private SpriteRenderer spriteRenderer;

	private static int breakableCount = 0;
	private bool isBreakable;

	public static void ResetBreakableCount()
	{
		breakableCount = 0;
	}

	private void Reset()
	{
		crackSoundVolume = .1f;
	}

	void Start()
	{
		timesHit = 0;
		spriteRenderer = GetComponent<SpriteRenderer>();

		isBreakable = CompareTag("Breakable");
		if (isBreakable)
		{
			breakableCount++;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		AudioSource.PlayClipAtPoint(crackSound, transform.position, crackSoundVolume);
		if (isBreakable)
		{
			var maxHits = hitSprites.Length + 1;
			if (++timesHit >= maxHits)
			{
				SpawnSmoke();
				Destroy(gameObject);
				if (--breakableCount <= 0)
				{
					FindObjectOfType<LevelManager>().LoadNextLevel();
				}
			}
			else
			{
				var newSprite = hitSprites[timesHit - 1];
				if (newSprite != null)
				{
					spriteRenderer.sprite = newSprite;
				}
				else
				{
					Debug.LogErrorFormat(this,
						"Brick Sprite missing at index: {0} for {1}. The hitSprites array is [{2}].",
						timesHit - 1,
						ToString(),
						string.Join(", ", hitSprites.Select((s) => s == null ? "null" : s.name).ToArray()));
				}
			}
		}
	}

	private void SpawnSmoke()
	{
		var smokePuff = Instantiate(smoke, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
		var main = smokePuff.GetComponent<ParticleSystem>().main;
		var alpha = main.startColor.color.a;
		main.startColor = new Color(
			spriteRenderer.color.r,
			spriteRenderer.color.g,
			spriteRenderer.color.b,
			alpha);
	}
}
