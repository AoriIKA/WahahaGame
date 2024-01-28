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
        DontDestroyOnLoad(this.gameObject);
    }
    public void NextScene()
    {
        StartCoroutine(scene_change());
        //1�b�ԃt�F�[�h���Ă���ɃV�[���ړ�����
    }
    IEnumerator scene_change()
    {

        fade.FadeIn(time);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Test_marbas_2");
        fade.FadeOut(time);
    }
}
