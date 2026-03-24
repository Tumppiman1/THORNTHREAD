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
    
    private bool _hovered = false;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //TooltipSystem.Show(content, header);
        _hovered = true;
        StartCoroutine(Delay());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // TooltipSystem.Hide();
        _hovered = false;
        StopAllCoroutines();
        TooltipSystem.Hide();
    }

    void Show()
    {
        
        TooltipSystem.Show(content, header);
    }

    void Hide()
    {
        
    }

    IEnumerator Delay()
    {
        
        yield return new WaitForSeconds(0.5f);
        TooltipSystem.Show(content, header);
    }
}
