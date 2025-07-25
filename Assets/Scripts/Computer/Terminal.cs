using System.Collections;
using System.Linq;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    // References
    [SerializeField] private TMPro.TMP_Text screenText;

    //State
    string phrase = "";


    void Start()
    {
        //WriteOnScreen(TEXT_1);
    }

    void Update()
    {
        
    }

    public void WriteOnScreen(string text)
    {
        StartCoroutine(StartWriting(text));
    }

    IEnumerator StartWriting(string text)
    {
        ClearText();
        char[] words = text.ToCharArray();
        

        for (int i = 0; i < words.Length; i++)
        {
            phrase += words[i];
            SetTerminalText(phrase);

            // Emit event to sound
            yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));
        }
    }

    private void SetTerminalText(string text) => screenText.SetText($"$ {text}_");
    private void ClearText() => screenText.SetText("$  _");
}
