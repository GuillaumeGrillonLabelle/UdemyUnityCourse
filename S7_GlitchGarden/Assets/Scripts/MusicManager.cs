using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
	static MusicManager instance = null;

	public AudioClipInfo[] audioClips;

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
			ChangeVolume(PlayerPrefsManager.GetMasterVolume());
			SceneManager.sceneLoaded += SceneManager_sceneLoaded;
		}
	}

	public void ChangeVolume(float volume)
	{
		music.volume = volume;
	}

	private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
	{
		var newLevelIndex = scene.buildIndex;

		print("MusicPlayer: loaded level " + newLevelIndex);

		if (newLevelIndex >= 0 && newLevelIndex < audioClips.Length)
		{
			var newClipInfo = audioClips[newLevelIndex];
			if (!string.Equals(music.clip ? music.clip.name : null, newClipInfo != null && newClipInfo.clip ? newClipInfo.clip.name : null))
			{
				music.Stop();
				music.clip = newClipInfo.clip;

				if (newClipInfo != null && newClipInfo.clip != null)
				{
					var loop = newClipInfo.loop;

					print("MusicPlayer: playing " + (music.clip ? music.clip.name : "null") + " (looping: " + loop + ")");
					music.loop = loop;
					music.Play();
				}
			}
		}
	}
}
