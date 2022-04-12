using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour
{
    [SerializeField] Case framing;
    [SerializeField] public Vector2 Size = new Vector2(6,6);
    [SerializeField] float distance = 1f;

    [HideInInspector] public int InputIndex = 0;

    [HideInInspector] public List<Case> Cases = new List<Case>();

    int ActualWordY = 0;

    RectTransform rectTransform;
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        manager = FindObjectOfType<GameManager>();
        for (int i = 0; i < Size.y; i++)
        {
            for (int j = 0; j < Size.x; j++)
            {
                Cases.Add(Instantiate(framing, framing.transform.position + new Vector3(distance * j, -distance * i), framing.transform.rotation, transform));
                Cases.Last().index = i * (int)(Size.y) + j;
                Cases.Last().WordIndex = i;
                Cases.Last().enabled = true;
            }
        }
    }

    public void NewSelect()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (InputIndex % (int)Size.x == Size.x - 1)
            CheckWord();
        if (InputIndex < Size.x * Size.y - 1)
            InputIndex += 1;
        Cases[InputIndex].InputF.Select();
    }

    public void SetWord()
    {

    }

    public void CheckWord()
    {
        int numberOfChar = 0;
        for (int i = 0; i < manager.CurrentWord.Length; i++)
        {
            int index = ActualWordY * (int)Size.x + i;
            if (CheckCharacter(Cases[index].InputF.text.ToCharArray()[0], manager.CurrentWord[i]))
            {
                ChangeInputColor(ref Cases[index].InputF, Color.green);
                numberOfChar++;
            }
        }
        ActualWordY++;
        if (numberOfChar == Size.x)
            manager.NewWord();
    }

    public void ChangeInputColor(ref InputField input, Color to)
    {
        var color = input.colors;
        color.selectedColor = Color.green;
        color.normalColor = Color.green;
        color.disabledColor = Color.green;
        color.highlightedColor = Color.green;
        input.colors = color;
    }

    public bool CheckCharacter(char a, char b)
    {
        Debug.Log($"{a} : {b}");
        a = char.ToUpper(a);
        b = char.ToUpper(b);
        return a == b;
    }
}
