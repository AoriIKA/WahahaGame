using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class DictationScript : MonoBehaviour
{
    [SerializeField]
    private Text m_Hypotheses;

    [SerializeField]
    private Text m_Recognitions;

    [SerializeField] private SkinnedMeshRenderer blendShapeProxy;

    private string sHypotheses;
    private string sRecognitions;

    private DictationRecognizer m_DictationRecognizer;

    void Start()
    {
        blendShapeProxy.SetBlendShapeWeight(5,100);
        m_DictationRecognizer = new DictationRecognizer();

      /* m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
            m_Recognitions.text += text + "\n";
            if (m_Hypotheses.text.Length >= 4)
            {
                m_Hypotheses.text = "";

            }
        };*/

        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
          //  Debug.LogFormat("Dictation hypothesis: {0}", text);
            m_Hypotheses.text += text;
            if (m_Hypotheses.text.Length >= 4)
            {
                m_Hypotheses.text = "";
              
            }
        };

     /*   m_DictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
        };*/

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };

        m_DictationRecognizer.Start();
    }
}