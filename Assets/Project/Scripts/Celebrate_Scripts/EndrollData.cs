using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndrollData", menuName = "ScriptableObjects/EndrollData", order = 1)]
public class EndrollData : ScriptableObject
{
    public List<string> endrollTexts; // エンドロールに表示する文字列のリスト
    public List<Sprite> endrollImages; // エンドロールに表示する画像のリスト
}
