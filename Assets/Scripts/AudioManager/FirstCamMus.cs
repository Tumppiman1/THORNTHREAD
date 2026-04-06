using UnityEngine;

public class FirstCamMus : MonoBehaviour
{
    void Start()
    {
        ZOneMusic zone = GetComponent<ZOneMusic>();

        if (zone != null)
        {
            AudioManager.Instance.PlayAmb(zone.ambientName);
        }
    }
}
