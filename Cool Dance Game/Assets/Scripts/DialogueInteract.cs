﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteract : MonoBehaviour
{
    public DialogueTrigger trigger;
    public DialogueManager manager;
    private bool zReady = true;

    // Update is called once per frame
    void Update()
    {
        // when you press the key set zready to false
        if (Input.GetKeyDown("z") && zReady)
        {
            // Starting dialogue
            if(!manager.dialogueStarted)
            {
                trigger.triggerDialogue();
            }

            
            else if(manager.dialogueStarted)
            {
                manager.DisplayNextSentence();
            }

            zReady = false;
        }
        // when you release the key set zready to true
        else if(Input.GetKeyUp("z") && !zReady)
            zReady = true;
    }
}
