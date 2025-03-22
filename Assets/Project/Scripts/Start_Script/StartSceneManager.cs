using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    private bool isFadingOut = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFadingOut) {
            StartCoroutine(WaitFadeOut());
            isFadingOut = true;
        }
    }

    IEnumerator WaitFadeOut()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("classroom");
    }
}
