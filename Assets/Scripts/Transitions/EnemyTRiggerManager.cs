using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EnemyTriggerManager : MonoBehaviour
{
  public static EnemyTriggerManager instance;


    [SerializeField] private Button North;
    [SerializeField] private Button South;
    [SerializeField] private Button East;
    [SerializeField] private Button West;
    [SerializeField] private ScreenListScript Screen;


    private GameObject Cam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void MoveNorth()
    {

        Cam = Screen.screenList[0];

    }

    }
