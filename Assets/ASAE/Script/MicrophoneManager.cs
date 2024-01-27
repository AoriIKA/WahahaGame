//using UnityEngine;




///// <summary>

///// ���p�N���s���N���X

///// </summary>

//public class MicrophoneManager : MonoBehaviour

//{

//    [SerializeField] private AudioSource audioSource = null;



//    [SerializeField] private SkinnedMeshRenderer blendShapeProxy;



//    float velocity = 0.0f;

//    float currentVolume = 0.0f;



//    [SerializeField] private float Power = 50f;

//    [SerializeField, Range(0f, 1f)] private float Threshold = 0.1f;



//    void Start()

//    {

//        // Audio Source �� Audio Clip ���}�C�N���͂ɐݒ�

//        // �����́A�f�o�C�X���inull �Ȃ�f�t�H���g�j�A���[�v�A���b��邩�A�T���v�����O���g��

//        audioSource.clip = Microphone.Start(null, true, 1, 44100);

//        // �}�C�N�� Ready �ɂȂ�܂őҋ@�i��u�j

//        while (Microphone.GetPosition(null) <= 0) { }

//        // �Đ��J�n�i�^�����悩��Đ��A�X�s�[�J�[����o�͂���ƃn�E�����O���܂��j

//        audioSource.Play();

//        audioSource.loop = true;

//    }

//    private void LateUpdate()

//    {

//        float targetVolume = GetAveragedVolume() * Power;

//        //Threshold�ȉ���������targetVolume��0�ɂ���

//        targetVolume = targetVolume < Threshold ? 0 : targetVolume;

//        //currentVolume����targetVolume�֊ɂ₩�ɕω�

//        currentVolume = Mathf.SmoothDamp(currentVolume, targetVolume, ref velocity, 0.05f);



//        if (blendShapeProxy == null)

//        {

//            Debug.LogError("blendShapeProxy���ݒ肳��Ă��܂���");

//            return;

//        }


//        blendShapeProxy.GetBlendShapeWeight
//        //���ʂ̒l��BlendShape��A�̌��ɓ���Ă���

//        blendShapeProxy.ImmediatelySetValue(BlendShapePreset.A, Mathf.Clamp01(currentVolume));

//    }



//    float GetAveragedVolume()

//    {

//        float[] data = new float[256];

//        float a = 0;

//        audioSource.GetOutputData(data, 0);

//        foreach (float s in data)

//        {

//            a += Mathf.Abs(s);

//        }

//        return a / 255.0f;

//    }

//}