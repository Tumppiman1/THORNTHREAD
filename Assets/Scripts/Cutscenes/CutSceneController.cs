using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += EndReached;
            videoPlayer.Play();
        }
     
    }

    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene("Main Scene");
    }

  
    public void SkipCutscene()
    {
        videoPlayer.Stop();
        SceneManager.LoadScene(nextSceneName);
    }
}
