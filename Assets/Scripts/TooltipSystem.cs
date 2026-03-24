using System.Collections;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;

    public Tooltip tooltip;

    public void Awake()
    {
        current = this;
    }

    public static void Show(string content, string header = "")
    {
        current.tooltip.transform.position = Input.mousePosition;
        current.tooltip.SetText(content, header);
        // StartCoroutine(Activate());
        
        Vector2 position = Input.mousePosition;
        float x = position.x / Screen.width;
        float y = position.y / Screen.height;
        if (x <= y && x <= 1 - y) //left
            current.tooltip.rectTransform.pivot = new Vector2(-0.15f, y);
        else if (x >= y && x <= 1 - y) //bottom
            current.tooltip.rectTransform.pivot = new Vector2(x, -0.1f);
        else if (x >= y && x >= 1 - y) //right
            current.tooltip.rectTransform.pivot = new Vector2(1.1f, y);
        else if (x <= y && x >= 1 - y) //top
            current.tooltip.rectTransform.pivot = new Vector2(x, 1.3f);
            
        current.tooltip.transform.position = position;

        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltip.SetText("-", "-");
        current.tooltip.gameObject.SetActive(false);
        
    }
    
}
