using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource speechText;

    public void PlayTextSpeech()
    {
        speechText.Play(0);
    }
}
