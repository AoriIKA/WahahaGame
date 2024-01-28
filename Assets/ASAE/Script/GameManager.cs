using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] titleUIImages = new SpriteRenderer[0];
    [SerializeField]
    private GameObject progressObject = null;
    [SerializeField]
    private obj_root_script dollyCartmanager;

    private int tutorialJumpCount = 0;

   
    private bool isPlayOneshot = true;//ˆê‰ñ‚¾‚¯ˆ—‚·‚éÛ‚É“s“xXV‚·‚é
   
    private bool isFlagCache = false;

    public void SetTutorialJumpCount()
    {
        tutorialJumpCount++;
        if(tutorialJumpCount >= 3)
        {
            for (int i=0;i<titleUIImages.Length-1;i++) {
                titleUIImages[i].gameObject.SetActive(false);
            }
            dollyCartmanager.StartDollyCaet();
            progressObject.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        progressObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float sayimagealpha;
   
   public void OpenCreditUIEvent()
    {
        float creditimagealpha1=0;

        sayimagealpha = titleUIImages[1].color.a;
        DOTween.To(() => sayimagealpha, num => sayimagealpha = num, 0, 1.0f).OnUpdate(() => 
        {
            titleUIImages[1].color = new Color(titleUIImages[1].color.r, titleUIImages[1].color.g, titleUIImages[1].color.b, sayimagealpha);
            });

        DOTween.To(() => creditimagealpha1, num => creditimagealpha1 = num, 1, 2.0f).OnUpdate(() =>
        {
            titleUIImages[2].color = new Color(titleUIImages[2].color.r, titleUIImages[2].color.g, titleUIImages[2].color.b, creditimagealpha1);
        });

        DOTween.To(() => creditimagealpha1, num => creditimagealpha1 = num, 1, 2.0f).OnUpdate(() =>
        {
            titleUIImages[3].color = new Color(titleUIImages[3].color.r, titleUIImages[2].color.g, titleUIImages[3].color.b, creditimagealpha1);
        });

        DOTween.To(() => creditimagealpha1, num => creditimagealpha1 = num, 1, 2.0f).OnUpdate(() =>
        {
            titleUIImages[4].color = new Color(titleUIImages[4].color.r, titleUIImages[4].color.g, titleUIImages[4].color.b, creditimagealpha1);
        });

        Invoke("CloseCreditUIEvent",6);
    }

    void CloseCreditUIEvent()
    {
        sayimagealpha = 0;
        float creditimagealpha1 = 1;

        DOTween.To(() => sayimagealpha, num => sayimagealpha = num, 1, 2).OnUpdate(() =>
        {
            titleUIImages[1].color = new Color(titleUIImages[1].color.r, titleUIImages[1].color.g, titleUIImages[1].color.b, sayimagealpha);
        });

        DOTween.To(() => creditimagealpha1, num => creditimagealpha1 = num, 0, 2.0f).OnUpdate(() =>
        {
            titleUIImages[2].color = new Color(titleUIImages[2].color.r, titleUIImages[2].color.g, titleUIImages[2].color.b, creditimagealpha1);
        });

        DOTween.To(() => creditimagealpha1, num => creditimagealpha1 = num, 0, 2.0f).OnUpdate(() =>
        {
            titleUIImages[3].color = new Color(titleUIImages[3].color.r, titleUIImages[2].color.g, titleUIImages[3].color.b, creditimagealpha1);
        });

        DOTween.To(() => creditimagealpha1, num => creditimagealpha1 = num, 0, 2.0f).OnUpdate(() =>
        {
            titleUIImages[4].color = new Color(titleUIImages[4].color.r, titleUIImages[4].color.g, titleUIImages[4].color.b, creditimagealpha1);
        });
    }
 }
