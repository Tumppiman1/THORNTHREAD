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

        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltip.SetText("-", "-");
        current.tooltip.gameObject.SetActive(false);
        
    }
    
}
