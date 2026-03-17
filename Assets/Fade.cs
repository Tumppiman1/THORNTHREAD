using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] public CanvasGroup blackScreen;
    [SerializeField] public Image blackScreenImage;
    
    public bool fadeIn = false;
    public bool fadeOut = false;
    
    void Update()
    {
        if (fadeIn && blackScreen.alpha <= 1) 
        {
            blackScreen.alpha += Time.deltaTime;

            if (blackScreen.alpha >= 1) 
            {
                fadeIn = false;
                Invoke(nameof (FadeOut), 1);
            }
        }

        if (fadeOut && blackScreen.alpha >= 0) 
        {
            blackScreen.alpha -= Time.deltaTime * 2;

            if (blackScreen.alpha <= 0) 
            {
                fadeOut = false;
                blackScreenImage.gameObject.SetActive(false);    
            }
        }
        
    }

    void FadeOut()
    {
        fadeOut = true;
    }

    public void StartFade()
    {
        blackScreenImage.gameObject.SetActive(true);
        fadeIn = true;
    }
}
