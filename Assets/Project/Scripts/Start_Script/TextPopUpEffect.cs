using System.Collections;
using UnityEngine;
using TMPro;

public class TexPopUpEffect : MonoBehaviour
{
    public TMP_Text textMeshPro;
    private float popUpHeight = 2.5f;
    private float popUpSpeed = 0.06f;
    private float delayBetweenChars = 0.001f;

    void Start()
    {
        StartCoroutine(PopUpEffect());
    }

    IEnumerator PopUpEffect()
    {
        textMeshPro.ForceMeshUpdate();
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        while (true)
        {
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible) continue;

                yield return StartCoroutine(AnimateCharacter(i, textInfo));
            }
        }
    }

    IEnumerator AnimateCharacter(int charIndex, TMP_TextInfo textInfo)
    {
        float time = 0;
        int vertexIndex = textInfo.characterInfo[charIndex].vertexIndex;
        TMP_MeshInfo[] cachedMeshInfo = textInfo.CopyMeshInfoVertexData();

        while (time < popUpSpeed)
        {
            float offsetY = Mathf.Lerp(0, popUpHeight, time / popUpSpeed);
            MoveCharacter(textInfo, charIndex, vertexIndex, cachedMeshInfo, offsetY);
            time += Time.deltaTime;
            yield return null;
        }

        time = 0;
        while (time < popUpSpeed)
        {
            float offsetY = Mathf.Lerp(popUpHeight, 0, time / popUpSpeed);
            MoveCharacter(textInfo, charIndex, vertexIndex, cachedMeshInfo, offsetY);
            time += Time.deltaTime;
            yield return null;
        }
    }

    void MoveCharacter(TMP_TextInfo textInfo, int charIndex, int vertexIndex, TMP_MeshInfo[] cachedMeshInfo, float offsetY)
    {
        for (int j = 0; j < 4; j++)
        {
            textInfo.meshInfo[textInfo.characterInfo[charIndex].materialReferenceIndex].vertices[vertexIndex + j] =
                cachedMeshInfo[textInfo.characterInfo[charIndex].materialReferenceIndex].vertices[vertexIndex + j] +
                new Vector3(0, offsetY, 0);
        }

        textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
    }
}
