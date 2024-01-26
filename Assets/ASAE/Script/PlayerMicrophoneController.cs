using System;
using System.Linq;
using UnityEngine;


public class PlayerMicrophoneController : MonoBehaviour
{

    [SerializeField] private string m_DeviceName;
    private AudioClip m_AudioClip;
    private int m_LastAudioPos;
    private float m_AudioLevel;

    [SerializeField] private GameObject m_Cube;
    [SerializeField, Range(10, 100)] private float m_AmpGain = 10;

    private Rigidbody palyerRigid;

    private int playerJumpcount = 0;
    private int playerMaxjumpCount = 3;

    [SerializeField]
    private bool isPlayOneshot = true;//��񂾂���������ۂɓs�x�X�V����
    private bool isFlagCache = false;

    //�}�C�N�f�o�C�X��ݒ��I�[�f�B�I�N���b�v�ɔ��f
    void Start()
    {
        palyerRigid = this.GetComponent<Rigidbody>();
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
    }

    private void FixedUpdate()
    {
       // if (Input.GetKeyDown(KeyCode.K)) palyerRigid.AddForce(transform.up * (3 + m_AmpGain * m_AudioLevel), ForceMode.Impulse);
    }

    void PlayerSpeakingJanp()
    {
        float[] waveData = GetUpdatedAudio();
        if (waveData.Length == 0) return;

        m_AudioLevel = waveData.Average(Mathf.Abs);

        if (isFlagCache == isPlayOneshot) return;//�t���O�ɍ��ق��Ȃ��ꍇ�͂����ŏ������I������
        
        //���̉��ʂ̋�������W�����v����
        if(1 + m_AmpGain * m_AudioLevel >= 1.6f)
        {
            Debug.Log("Jump!");
            //���̃t���[���Ŏ��s����Ȃ��悤�ɃL���b�V�����X�V
            palyerRigid.AddForce(transform.up * (5 + m_AmpGain * m_AudioLevel), ForceMode.Impulse);
            isFlagCache = !isFlagCache;
            if (playerJumpcount < playerMaxjumpCount-1)
            {
                Invoke("ResetFlag", 0.3f);
                playerJumpcount++;
            }

        }
        else
        {
            m_Cube.transform.localScale = new Vector3(1, 1 + m_AmpGain * m_AudioLevel, 1);
            Debug.Log(1 + m_AmpGain * m_AudioLevel);
        }
        
       

       
    }

    private void ResetFlag()
    {
        
        isPlayOneshot = !isPlayOneshot;
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Ground" && playerJumpcount > 0)
        {
            playerJumpcount = 0;
            isPlayOneshot = !isPlayOneshot;
        }
    }

    private float[] GetUpdatedAudio()
    {

        int nowAudioPos = Microphone.GetPosition(null);// null�Ńf�t�H���g�f�o�C�X

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