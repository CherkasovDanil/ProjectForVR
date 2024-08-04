using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestVRKeyBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private KeyboardKey[] keyboardKeys;
    [SerializeField] private Toggle toggle;
    [SerializeField] private SecondModeKeyboard secondModeKeyboard;
    
    private void Awake()
    {
        foreach (var key in keyboardKeys)
        {
            key.OnClickButtonEvent += UpdField;
        }

        toggle.onValueChanged.AddListener(ModeChange);
    }

    private void ModeChange(bool arg0)
    {
        foreach (var key in keyboardKeys)
        {
            key.PointerEvents.Enabled = arg0;
            secondModeKeyboard.enabled = !arg0;
            if (!arg0)
            {
                secondModeKeyboard.StartFocus();
            }
            else
            {
                secondModeKeyboard.StopFocus();
            }
        }
    }

    private void UpdField(object sender, string e)
    {
        if (e == "space")
        {
            text.text += " ";
        }
        else if (e == "backspace")
        {
            if (text.text.Length >= 1)
            {
                 text.text= text.text.Remove(text.text.Length - 1, 1);
            }
        }
        else
        {
            text.text += e;
        }
    }
}