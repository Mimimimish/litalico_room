using UnityEngine;
using TMPro;

public class TextWaveEffect : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public float waveSpeed = 2.0f; 
    public float waveHeight = 5.0f;

    void Start()
    {
        textMeshPro = GetComponent<TMP_Text>();
    }

    void Update()
    {
        textMeshPro.ForceMeshUpdate();
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;

            Vector3 offset = new Vector3(0, Mathf.Sin(Time.time * waveSpeed + i * 0.3f) * waveHeight, 0);
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            for (int j = 0; j < 4; j++)
            {
                textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].vertices[vertexIndex + j] += offset;
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
        }
    }
}
