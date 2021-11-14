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
    public bool xPushed = false;

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

    private float getTextSpeed()
    {
        if (xPushed)
            return 0f;
        return 0.0375f;
    }

    IEnumerator typeSentence (string sentence)
    {
        dialogueText.text = "";
        bool firstLine = true;

        // Split the sentence by its spaces
        string[] words = sentence.Split(' ');

        foreach (string word in words)
        {
            float waitTime = getTextSpeed();

            // If a word plus length of current dialogue text is longer than 39, add a new line
            if (word.Length + dialogueText.text.Length > 39 && firstLine)
            {
                dialogueText.text += "\n";
                firstLine = false;
            }

            // Add each letter to text box, plus a space
            foreach (char letter in word)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(waitTime);
            }

            dialogueText.text += " ";
        }

        sentenceInProgress = false;
    }

    void EndDialogue()
    {
        dialogueStarted = false;
        animator.SetBool("isOpen", false);
    }
}
