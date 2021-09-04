using UnityEngine;
using TMPro;

public class TextOutput : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    [SerializeField]
    private string _initialText;

    private void Awake()
    {
        _text.text = _initialText;
    }

    public void Output(string value)
    {
        _text.text = string.Format("{0} {1}", _initialText, value);
    }
}
