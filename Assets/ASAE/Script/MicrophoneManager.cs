//using UnityEngine;




///// <summary>

///// 口パクを行うクラス

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

//        // Audio Source の Audio Clip をマイク入力に設定

//        // 引数は、デバイス名（null ならデフォルト）、ループ、何秒取るか、サンプリング周波数

//        audioSource.clip = Microphone.Start(null, true, 1, 44100);

//        // マイクが Ready になるまで待機（一瞬）

//        while (Microphone.GetPosition(null) <= 0) { }

//        // 再生開始（録った先から再生、スピーカーから出力するとハウリングします）

//        audioSource.Play();

//        audioSource.loop = true;

//    }

//    private void LateUpdate()

//    {

//        float targetVolume = GetAveragedVolume() * Power;

//        //Threshold以下だったらtargetVolumeを0にする

//        targetVolume = targetVolume < Threshold ? 0 : targetVolume;

//        //currentVolumeからtargetVolumeへ緩やかに変化

//        currentVolume = Mathf.SmoothDamp(currentVolume, targetVolume, ref velocity, 0.05f);



//        if (blendShapeProxy == null)

//        {

//            Debug.LogError("blendShapeProxyが設定されていません");

//            return;

//        }


//        blendShapeProxy.GetBlendShapeWeight
//        //音量の値をBlendShapeのAの口に入れている

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