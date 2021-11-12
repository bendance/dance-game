using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteract : MonoBehaviour
{
    public DialogueTrigger trigger;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("z"))
        {
            trigger.triggerDialogue();
        }
    }
}
