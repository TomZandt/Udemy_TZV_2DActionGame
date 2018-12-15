using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    private Animator transitionAnimator;

    //****************************************************************************************************
    private void Start()
    {
        transitionAnimator = GetComponent<Animator>();
    }

    //****************************************************************************************************
    public void LoadScene(string _sceneName)
    {
        StartCoroutine(Transition(_sceneName));
    }

    //****************************************************************************************************
    IEnumerator Transition(string sceneName)
    {
        transitionAnimator.SetTrigger("Menu_End");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
    }
}
