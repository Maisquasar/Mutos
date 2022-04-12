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
    }

    public void CheckCharacter()
    {
        if (InputF.text.Count() > 1)
            InputF.text.Remove(InputF.text.Count() - 1);
        if (InputF.text.Count() == 1)
            grid.NewSelect();
    }

    public void OnSelect()
    {
        grid.InputIndex = index;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
