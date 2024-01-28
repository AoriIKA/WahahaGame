using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class ProgressUISystem : MonoBehaviour
{
    [SerializeField]
    Transform gameClearUIObject = null;
    [SerializeField]
    private obj_root_script dollyCartmanager;
    [SerializeField] private SkinnedMeshRenderer blendShapeProxy;
    [SerializeField]
    PlayerMicrophoneController playerScript;
    [SerializeField, Range(1.0f, 100.0f)]
    public float progress;

    float maxProgress;
    [SerializeField]
    private Transform progressObject;
    [SerializeField]
    private float progressSpeed;

    [SerializeField]
    GameObject bearObject;
    [SerializeField]
    GameObject playerObject;

    [SerializeField]
    private float gageStopTime = 3;

    float timer = 0;
    float second = 3;
    float remaining = 0;
    bool isStop = false;

    bool isOneShot = false;
    // Start is called before the first frame update
    void Start()
    {
        maxProgress = 32f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {
            if (progressObject.position.y <= maxProgress + 1)
            {
                GameInProgress();
            }
            else
            {
                //ƒQ[ƒ€ƒNƒŠƒAˆ—
                if (!isOneShot)
                {
                    GameClearEvent();
                    isOneShot = true;
                }
            }
        }
        else
        {
            timer +=  Time.deltaTime; ;
            remaining = second - timer;
            Debug.Log(remaining);
            if (remaining <= 0) ResetStopFlag();
        }

    }

    public void PlayerHitObstaclEvent()
    {
        isStop = true;
        timer = 0;
        remaining = gageStopTime;
       
    }

    void ResetStopFlag()
    {
        isStop = false;
    }

    void GameInProgress()
    {
        if (progressObject.position.y > maxProgress + 1) return;
        progress = progressObject.position.y / maxProgress;
        //  Debug.Log(progress * 100 + "%");
        progressObject.Translate(0, progressSpeed * Time.deltaTime, 0);
    }

    void GameClearEvent()
    {

        for (int i = 0; i < 6; i++) { blendShapeProxy.SetBlendShapeWeight(i, 0); }
        blendShapeProxy.SetBlendShapeWeight(5, 100);
        blendShapeProxy.SetBlendShapeWeight(1, 100);
        bearObject.SetActive(true);
        playerObject.transform.eulerAngles = new Vector3(0,180,0);

        playerScript.MicrophoneEnd();
        dollyCartmanager.StopDollyCart();
        gameClearUIObject.transform.DOMove(Vector3.zero, 1);
        Invoke("ReLoadMainGame", 15f);
    }

    void ReLoadMainGame()
    {
        SceneManager.LoadScene(0);
    }
}
