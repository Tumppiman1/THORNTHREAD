using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutSceneEndCOntrol : MonoBehaviour
{
    public VideoPlayer VideoPlayer2;
    public string nextSceneName;


    private void Start()
    {
      
        if (VideoPlayer2 != null)
        {
            VideoPlayer2.loopPointReached += EndReached2;
            VideoPlayer2.Play();
        }
    }

    void EndReached2(VideoPlayer vp)
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void SkipCutscene()
    {
        VideoPlayer2.Stop();
        SceneManager.LoadScene(nextSceneName);
    }
}
