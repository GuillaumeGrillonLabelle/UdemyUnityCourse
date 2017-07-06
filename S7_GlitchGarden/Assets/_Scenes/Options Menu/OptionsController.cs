using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
	public Slider volumeSlider;
	public Slider difficultySlider;

	private MusicManager musicPlayer;

	private void Start()
	{
		musicPlayer = FindObjectOfType<MusicManager>();

		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();

		difficultySlider.value = PlayerPrefsManager.GetDifficulty();
	}

	public void OnVolumeChanged()
	{
		if (musicPlayer != null)
		{
			musicPlayer.ChangeVolume(volumeSlider.value);
		}
	}

	public void SaveAndExit()
	{
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
		PlayerPrefsManager.SetDifficulty((int)difficultySlider.value);

		FindObjectOfType<LevelManager>().LoadLevel("Start Menu");
	}

	public void ResetToDefaults()
	{
		volumeSlider.value = 0.5f;
		difficultySlider.value = 2;
	}
}
