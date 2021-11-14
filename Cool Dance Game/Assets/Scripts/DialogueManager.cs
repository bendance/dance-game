using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Animator animator;
    public bool sentenceInProgress = false;
    public bool dialogueStarted;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        dialogueStarted = true;
        Debug.Log("Starting conversation with " + dialogue.name);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        sentenceInProgress = true;

        Debug.Log(sentenceInProgress);

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        
        StartCoroutine(typeSentence(sentence));
    }

    IEnumerator typeSentence (string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }

        sentenceInProgress = false;
        Debug.Log(sentenceInProgress);
    }

    void EndDialogue()
    {
        dialogueStarted = false;
        animator.SetBool("isOpen", false);
    }
}
