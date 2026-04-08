using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
   
    public void OnPlayButton ()
    {
        SceneManager.LoadScene("CutSceneIntro");
    }

    public void OnCorpseButton()
    {
        SceneManager.LoadScene("CutSceneEnd");
    }
   
}
