using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
using DG.Tweening;
public class KeywordScript : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManagerScript=null;
    [SerializeField] private SkinnedMeshRenderer blendShapeProxy;
    [SerializeField]
    private obj_root_script dollyCartmanager;
    [SerializeField]
    private string[] m_Keywords;

    [SerializeField]
    private Text playerSeyKeywordsText;

    private KeywordRecognizer m_Recognizer;

    [SerializeField]
    GameObject gameOverImageObject = null;

    bool isOneShot=false;
    bool isOneShot2 = false;

    void Start()
    {
        m_Recognizer = new KeywordRecognizer(m_Keywords);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();
    }

    void OnDestroy()
    {
        if (m_Recognizer != null)
        {
            m_Recognizer.Stop();
            m_Recognizer.Dispose();
        }
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0}{2}", args.text, args.confidence, Environment.NewLine);
        
        Debug.Log(builder.ToString());
        playerSeyKeywordsText.text = builder.ToString().Trim() ;

       
    }

    private void Update()
    {
        if (gameManagerScript.isGamePlay)
        {
            if (playerSeyKeywordsText.text == "‚¬‚Ô" || playerSeyKeywordsText.text == "‚¬‚Ô‚ ‚Á‚Õ")
            {
                if (!isOneShot)
                {
                    GameOverEvent();
                    isOneShot = true;
                }

            }
        }

        if (playerSeyKeywordsText.text == "‚­‚ê‚¶‚Á‚Æ")
        {
            if (!isOneShot2)
            {
                gameManagerScript.OpenCreditUIEvent();
                Invoke("ResetOpenCreditFlag",1);
                isOneShot2 = true;
            }

        }
    }

    void ResetOpenCreditFlag()
    {
        playerSeyKeywordsText.text = "";
        isOneShot2 = false;
    }

    void GameOverEvent()
    {
        for (int i = 0; i < 6; i++) { blendShapeProxy.SetBlendShapeWeight(i, 0); }
        blendShapeProxy.SetBlendShapeWeight(6, 100);

        dollyCartmanager.StopDollyCart();
        gameOverImageObject.transform.DOMove(Vector3.zero,1);
        Invoke("ReLoadMainGame",2f);
    }

    void ReLoadMainGame()
    {
        SceneManager.LoadScene(0);
    }
}