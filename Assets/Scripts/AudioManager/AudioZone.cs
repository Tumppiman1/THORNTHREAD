using UnityEngine;

public class AudioZone : MonoBehaviour
{
    public float forestVolume = 1f;
    public float riverVolume = 0f;
    public float torchVolume = 0f;
    public float castleVolume = 0f;
    public float caldronVolume = 0f;
    

    public float fadeSpeed = 2f;

    private bool isActive = false;

    public void ActivateZone()
    {
        isActive = true;
    }

    public void DeactivateZone()
    {
        isActive = false;
    }

    void Update()
    {
        var audio = AudioManager.Instance;

        audio.forestSource.volume =
            Mathf.Lerp(audio.forestSource.volume, forestVolume, Time.deltaTime * 2f);

        audio.riverSource.volume =
            Mathf.Lerp(audio.riverSource.volume, riverVolume, Time.deltaTime * 2f);

        audio.torchSource.volume =
            Mathf.Lerp(audio.torchSource.volume, torchVolume, Time.deltaTime * 2f);

        audio.castleSource.volume =
            Mathf.Lerp(audio.castleSource.volume, castleVolume, Time.deltaTime * 2f);

        audio.caldronSource.volume =
            Mathf.Lerp(audio.caldronSource.volume, caldronVolume, Time.deltaTime * 2f);
    }
}