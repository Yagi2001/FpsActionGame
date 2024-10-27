using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "DialogueVoices", menuName = "ScriptableObjects/DialogueVoices" )]
public class DialogueVoices : ScriptableObject
{
    public string dialogueName;
    public AudioClip[] audioClips;
    public float duration;
}
