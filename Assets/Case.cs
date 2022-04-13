using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Case : MonoBehaviour
{
    Grid grid;
    [SerializeField] public Image Image;
    [SerializeField] public InputField InputF;
    public int index;
    public int WordIndex;

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<Grid>();
        InputF.characterLimit = 1;
        InputF.onValidateInput +=
            delegate (string s, int i, char c) { return char.ToUpper(c); };
    }

    public void CheckCharacter()
    {
        if (InputF.text.Count() > 0)
            if (InputF.text.ToCharArray()[0] == ' ')
                InputF.text = "";
        if (InputF.text.Count() == 1)
            grid.NewSelect();
    }

    public void OnSelect()
    {
        if (index != grid.Size.x - 1)
            return;
        if (InputF.text.Count() == 1)
            grid.CheckWord();
    }

    public void ClearChar()
    {
        InputF.text = "";
    }

    public void Update()
    {
    }
}
