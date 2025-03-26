using System.Collections;
using UnityEngine;
using TMPro;

public class TextWaveEffect : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    private float waveDuration = 0.8f; // 波が1往復する時間
    private float scaleMultiplier = 1.05f; // Y方向の拡大率
    private float moveAmount = 1.5f; // 上方向への移動距離
    private float waveSpread = 0.3f; // 波の広がり度合い（隣接する文字への影響度）
    private float waitTime = 0.8f; // 波が1回終わった後の待機時間
    private float startDelay = 5f; // テキスト表示開始までの遅延時間

    private Vector3[][] originalVertices;
    private float[] originalScales;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        textMeshPro.alpha = 0f;

        StartCoroutine(WaitAndStartTextAnimation());
    }

    IEnumerator WaitAndStartTextAnimation()
    {
        yield return new WaitForSeconds(startDelay);

        textMeshPro.alpha = 1f;

        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        textMeshPro.ForceMeshUpdate();
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        int charCount = textInfo.characterCount;
        if (charCount == 0) yield break;

        originalVertices = new Vector3[textInfo.meshInfo.Length][];
        originalScales = new float[charCount];

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            originalVertices[i] = (Vector3[])textInfo.meshInfo[i].vertices.Clone();
        }

        for (int i = 0; i < charCount; i++)
        {
            originalScales[i] = 1f;
        }

        float time = 0;
        while (true)
        {
            textMeshPro.ForceMeshUpdate();
            time += Time.deltaTime;

            if (time < waveDuration)
            {
                for (int i = 0; i < charCount; i++)
                {
                    if (textInfo.characterInfo[i].isVisible)
                    {
                        float waveOffset = i * waveSpread;
                        float progress = Mathf.Sin((time * Mathf.PI * 2 / waveDuration) - waveOffset);

                        if (i == Mathf.FloorToInt(time / waveDuration * charCount) % charCount)
                        {
                            ApplyWaveEffect(i, progress, originalVertices, textInfo, textMeshPro);
                        }
                        else if (i == Mathf.FloorToInt(time / waveDuration * charCount) % charCount - 1 || i == Mathf.FloorToInt(time / waveDuration * charCount) % charCount + 1) // n-1, n+1
                        {
                            ApplyWaveEffect(i, progress * 0.5f, originalVertices, textInfo, textMeshPro);
                        }
                    }
                }
            }
            else
            {
                ResetTextToOriginalState(textInfo);
                
                yield return new WaitForSeconds(waitTime);
                time = 0;
            }

            textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

            yield return null;
        }
    }

    private void ApplyWaveEffect(int i, float progress, Vector3[][] originalVertices, TMP_TextInfo textInfo, TextMeshProUGUI textMeshPro)
    {
        TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
        int vertexIndex = charInfo.vertexIndex;
        int materialIndex = charInfo.materialReferenceIndex;

        Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;
        Vector3[] originalVerts = originalVertices[materialIndex];

        float scaleY = Mathf.SmoothStep(1, scaleMultiplier, (progress + 1) / 2);
        float moveY = Mathf.SmoothStep(0, moveAmount, (progress + 1) / 2);

        for (int j = 0; j < 4; j++)
        {
            Vector3 offset = originalVerts[vertexIndex + j] - charInfo.bottomLeft;
            offset.y *= scaleY;

            vertices[vertexIndex + j] = charInfo.bottomLeft + offset + Vector3.up * moveY;
        }
    }

    private void ResetTextToOriginalState(TMP_TextInfo textInfo)
    {
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (textInfo.characterInfo[i].isVisible)
            {
                TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
                int vertexIndex = charInfo.vertexIndex;
                int materialIndex = charInfo.materialReferenceIndex;

                Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

                Vector3[] originalVerts = originalVertices[materialIndex];
                for (int j = 0; j < 4; j++)
                {
                    vertices[vertexIndex + j] = originalVerts[vertexIndex + j];
                }
            }
        }
        
        textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }
}
