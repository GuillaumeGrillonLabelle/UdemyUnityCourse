using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
	static MusicPlayer instance = null;

	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;

	private AudioSource music;

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			SceneManager.sceneLoaded += SceneManager_sceneLoaded;
		}
	}

	private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
	{
		var level = scene.buildIndex;

		print("MusicPlayer: loaded level " + level);
		music.Stop();

		switch (level)
		{
			case 0:
				music.clip = startClip;
				break;

			case 1:
				music.clip = gameClip;
				break;

			default:
				music.clip = endClip;
				break;
		}

		music.loop = true;
		music.Play();
	}
}
