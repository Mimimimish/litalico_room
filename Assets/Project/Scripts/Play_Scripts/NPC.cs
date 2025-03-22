using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField, Header("【会話情報データ】")]
    public DialogData dialogData;
    [SerializeField, Header("【会話可能時に表示されるアイコン】")]
    public GameObject talkIcon;
}
