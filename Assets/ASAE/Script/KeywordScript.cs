using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class KeywordScript : MonoBehaviour
{
    [SerializeField]
    private string[] m_Keywords;

    [SerializeField]
    private Text playerSeyKeywordsText;

    private KeywordRecognizer m_Recognizer;

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
        if (playerSeyKeywordsText.text == "‚¬‚Ô" || playerSeyKeywordsText.text == "‚¬‚Ô‚ ‚Á‚Õ")
        {
           
            SceneManager.LoadScene(0);
        }
    }
}