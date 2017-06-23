using UnityEngine;

public class Brick : MonoBehaviour
{
	public Sprite[] hitSprites;
	public AudioClip crackSound;
	public float crackSoundVolume;

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
			}
		}
	}
}
