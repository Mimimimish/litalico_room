using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogData")]
public class DialogData : ScriptableObject
{
    [SerializeField, Header("【話し手】")]
    private string talkerName;
    [SerializeField, Header("【会話文】"), TextArea]
    private string[] sentences;


    public string TalkerName => talkerName;
    public string[] Sentences => sentences;
}
