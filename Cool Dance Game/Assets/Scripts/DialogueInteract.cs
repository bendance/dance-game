using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteract : MonoBehaviour
{
    public DialogueTrigger trigger;
    public DialogueManager manager;

    // Update is called once per frame
    void Update()
    {
        // Starting dialogue
        if(Input.GetKeyDown("z") && !manager.dialogueStarted)
        {
            trigger.triggerDialogue();
        }

        // Continue dialogue
        if(Input.GetKeyDown("z") && manager.dialogueStarted)
        {
            manager.DisplayNextSentence();
        }
    }
}
