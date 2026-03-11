using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ClickTransition : MonoBehaviour
{
    [SerializeField] private string gameScene;

    private void Awake()
    {

    }

    public void Battle()
    {

            SceneManager.LoadSceneAsync(gameScene);

       

    }
}
