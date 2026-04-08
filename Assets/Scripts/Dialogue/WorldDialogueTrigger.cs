using UnityEngine;

public class WorldDialogueTrigger : MonoBehaviour
{
    public DialogueData dialogue;
    public bool triggerOnce = true;

    private bool hasTriggered = false;

    private void OnMouseDown() // works with collider + camera
    {
        if (triggerOnce && hasTriggered) return;

        if (dialogue != null)
        {
            DialogueManager.Instance.StartDialogue(dialogue);
            hasTriggered = true;
        }
    }
}