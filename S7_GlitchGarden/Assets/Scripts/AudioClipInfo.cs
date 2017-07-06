using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioClipInfo", menuName = "ScriptableObject Assets/New AudioClipInfo")]
public class AudioClipInfo : ScriptableObject
{
	public AudioClip clip;
	public bool loop;
}
