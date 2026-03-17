using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EnemyTriggerManager : MonoBehaviour
{
    public static EnemyTriggerManager instance;
    public GameObject SupriseCam;
    [SerializeField] private string gameScene;
    
    void Start()
            {
       if (SupriseCam == null)
        {
            this.gameObject.SetActive(false);
        }


    }


    public void Battle()
    {


        


    }
}
