using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public string header;
    
    //[TextArea(15,20)]
    [Multiline()]
    public string content;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //TooltipSystem.Show(content, header);
        StartCoroutine(Delay());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // TooltipSystem.Hide();
        StopAllCoroutines();
        TooltipSystem.Hide();
    }

    IEnumerator Delay()
    {
        
        yield return new WaitForSeconds(0.5f);
        TooltipSystem.Show(content, header);
    }
}
