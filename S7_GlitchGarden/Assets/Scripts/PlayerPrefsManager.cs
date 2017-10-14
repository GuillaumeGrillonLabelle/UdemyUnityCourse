using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{

	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";

	public static void SetMasterVolume(float volume)
	{
		if (volume >= 0f && volume <= 1f)
		{
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		}
		else
		{
			Debug.LogErrorFormat("Master volume of {0} is out of [0; 1] range.", volume);
		}
	}

	public static float GetMasterVolume()
	{
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f);
	}

	public static void UnlockLevel(int level)
	{
		if (level >= 0 && level <= SceneManager.sceneCountInBuildSettings - 1)
		{
			PlayerPrefs.SetInt(LEVEL_KEY + level, 1);
		}
		else
		{
			Debug.LogErrorFormat("Trying to unlock level #{0}, but it's not in build order.", level);
		}
	}

	public static bool IsLevelUnlocked(int level)
	{
		if (level >= 0 && level <= SceneManager.sceneCountInBuildSettings - 1)
		{
			return PlayerPrefs.GetInt(LEVEL_KEY + level, 0) == 1;
		}

		Debug.LogErrorFormat("Trying to query level #{0}, but it's not in build order.", level);
		return false;
	}

	public static void SetDifficulty(int difficulty)
	{
		if (difficulty >= 1 && difficulty <= 3)
		{
			PlayerPrefs.SetInt(DIFFICULTY_KEY, difficulty);
		}
		else
		{
			Debug.LogErrorFormat("Difficulty of {0} is out of [1; 3] range.", difficulty);
		}
	}

	public static int GetDifficulty()
	{
		return PlayerPrefs.GetInt(DIFFICULTY_KEY, 2);
	}
}
