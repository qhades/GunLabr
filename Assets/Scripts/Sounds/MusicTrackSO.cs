using UnityEngine;

[CreateAssetMenu(fileName = "MusicTrack_", menuName = "Scriptable Objects/Sounds/Music Track")]
public class MusicTrackSO : ScriptableObject
{
    #region Header MUSIC TRACK DETAILS
    [Space(10)]
    [Header("MUSIC TRACK DETAILS")]
    #endregion

    public string musicName;
    public AudioClip musicClip;
    public float musicVolume = 1f;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUltilities.ValidateCheckEmptyString(this, nameof(musicName), musicName);
        HelperUltilities.ValidateCheckNullValue(this, nameof(musicClip), musicClip);
        HelperUltilities.ValidateCheckPositiveValue(this, nameof(musicVolume), musicVolume, true);
    }
#endif
    #endregion
}
