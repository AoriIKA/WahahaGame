using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


public class fadeIn : MonoBehaviour
{
    [SerializeField] Fade fade;
    float time = 1f;
    private void Start()
    {
        NextScene();
        DontDestroyOnLoad(this.gameObject);
    }
    public void NextScene()
    {
        StartCoroutine(scene_change());
        //1秒間フェードしてからにシーン移動する
    }
    IEnumerator scene_change()
    {

        fade.FadeIn(time);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(0);
        fade.FadeOut(time);
    }
}
