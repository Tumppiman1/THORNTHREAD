using NUnit.Framework;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EnemyTriggerManager : MonoBehaviour
{
    public static EnemyTriggerManager instance;
    public GameObject Blorbo;
   
  
   
    public GameObject nextCamera;

    void Start()
            {
       if (Blorbo == null)
        {
            this.gameObject.SetActive(false);
        }
        if (nextCamera == null)
        {
            this.gameObject.SetActive(false);
        }

    }


    private void OnEnable()
    {

        Invoke(nameof(NextCamera), 1f);

    }

    void NextCamera()
    {
        transform.parent.parent.gameObject.SetActive(false);
        nextCamera.SetActive(true);
        //nextCamera.GetComponent<CinemachineCamera>().ForceCameraPosition(nextCamera.transform.position);
        GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().FindCurrentActiveScreen();
    }

    private void OnDisable()
    {
        Destroy(Blorbo);
    }
}
