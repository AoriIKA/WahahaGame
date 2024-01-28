using System;
using System.Linq;
using UnityEngine;


public class PlayerMicrophoneController : MonoBehaviour
{

    [SerializeField]
    GameManager gamemScript=null;
    [SerializeField] private string m_DeviceName;
    private AudioClip m_AudioClip;
    private int m_LastAudioPos;
    private float m_AudioLevel;

    [SerializeField] private GameObject m_Cube;
    [SerializeField, Range(10, 100)] private float m_AmpGain = 10;

    private Rigidbody palyerRigid;
    private Animator playerAnimator;

    private int playerJumpcount = 0;
    private int playerMaxjumpCount = 3;

    [SerializeField]
    private bool isPlayOneshot = true;//一回だけ処理する際に都度更新する
    [SerializeField]
    private bool isFlagCache = false;

    [SerializeField]
    bool isDebug = false;
    [SerializeField]
    float  DebugSpeed = 15;

    //マイクデバイスを設定語オーディオクリップに反映
    void Start()
    {
        palyerRigid = this.GetComponent<Rigidbody>();
        playerAnimator = this.GetComponent<Animator>();

       


        //ゲーム中は走りモーション
        playerAnimator.SetTrigger("run");

        string targetDevice = "";

        foreach (var device in Microphone.devices)
        {
            Debug.Log($"Device Name: {device}");
            m_DeviceName = device;
            if (device.Contains(m_DeviceName))
            {
                targetDevice = device;
            }
        }

        Debug.Log($"=== Device Set: {targetDevice} ===");
        m_AudioClip = Microphone.Start(targetDevice, true, 10, 48000);
    }

    void Update()
    {
        PlayerSpeakingJanp();
        if (Input.GetKeyDown(KeyCode.Space) && isDebug)
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
            palyerRigid.AddForce(transform.up * DebugSpeed, ForceMode.Impulse);
            Invoke("ChangeFallPlayerGravity", 1f);

        }

        Debug.Log(playerJumpcount);

    }



    void PlayerSpeakingJanp()
    {
        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);

        float[] waveData = GetUpdatedAudio();
        if (waveData.Length == 0) return;

        m_AudioLevel = waveData.Average(Mathf.Abs);


   
        if (isFlagCache == isPlayOneshot) return;//フラグに差異がない場合はここで処理を終了する

        if (!isDebug)
        {
            //一定の音量の強さからジャンプする
            if (1 + m_AmpGain * m_AudioLevel >= 1.6f )
            {
                palyerRigid.velocity = Vector3.zero;

                Physics.gravity = new Vector3(0, -9.81f, 0);
                palyerRigid.AddForce(transform.up * (12 + m_AmpGain * m_AudioLevel), ForceMode.Impulse);
                //次のフレームで実行されないようにキャッシュを更新
                isFlagCache = !isFlagCache;

                if (palyerRigid.velocity.y < 0)
                {
                    ChangeFallPlayerGravity();
                }


                if (playerJumpcount < playerMaxjumpCount)
                {
                    Physics.gravity = new Vector3(0, -9.81f, 0);

                    playerAnimator.SetTrigger("jamp");
                    Invoke("ResetFlag", 0.2f);
                    Invoke("ChangeFallPlayerGravity", 1f);
                    playerJumpcount++;
                    gamemScript.SetTutorialJumpCount();
                }

            }
            else
            {
                m_Cube.transform.localScale = new Vector3(1, 1 + m_AmpGain * m_AudioLevel, 1);

            }
        }


       
        
       

       
    }

    private void ResetFlag()
    {

        isFlagCache = !isFlagCache;
       
    }

    void ChangeFallPlayerGravity()
    {
        
        Physics.gravity = new Vector3(0, -100, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Ground")
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
            playerAnimator.SetTrigger("randing");
            playerJumpcount = 0;
            isFlagCache = false;
        }
    }

    private float[] GetUpdatedAudio()
    {

        int nowAudioPos = Microphone.GetPosition(null);// nullでデフォルトデバイス

        float[] waveData = Array.Empty<float>();

        if (m_LastAudioPos < nowAudioPos)
        {
            int audioCount = nowAudioPos - m_LastAudioPos;
            waveData = new float[audioCount];
            m_AudioClip.GetData(waveData, m_LastAudioPos);
        }
        else if (m_LastAudioPos > nowAudioPos)
        {
            int audioBuffer = m_AudioClip.samples * m_AudioClip.channels;
            int audioCount = audioBuffer - m_LastAudioPos;

            float[] wave1 = new float[audioCount];
            m_AudioClip.GetData(wave1, m_LastAudioPos);

            float[] wave2 = new float[nowAudioPos];
            if (nowAudioPos != 0)
            {
                m_AudioClip.GetData(wave2, 0);
            }

            waveData = new float[audioCount + nowAudioPos];
            wave1.CopyTo(waveData, 0);
            wave2.CopyTo(waveData, audioCount);
        }

        m_LastAudioPos = nowAudioPos;

        return waveData;
    }
}