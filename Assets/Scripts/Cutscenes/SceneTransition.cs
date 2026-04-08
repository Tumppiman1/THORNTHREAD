using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
   
    public void OnPlayButton ()
    {
        SceneManager.LoadScene("CutSceneIntro");
    }


   
}
