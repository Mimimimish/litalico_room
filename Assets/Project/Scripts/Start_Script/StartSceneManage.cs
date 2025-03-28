using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManage : MonoBehaviour
{
    private bool isFadingOut = false;

    void Start()
    {
        StartCoroutine(WaitForStartInput());
    }

    IEnumerator WaitForStartInput()
    {
        yield return new WaitForSeconds(5);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isFadingOut)
            {
                StartCoroutine(WaitFadeOut());
                isFadingOut = true;
                break;
            }
            yield return null;
        }
    }

    IEnumerator WaitFadeOut()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("reception");
    }
}
