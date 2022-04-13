using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour
{
    [SerializeField] Case framing;
    [SerializeField] public Vector2Int Size = new Vector2Int(6, 6);
    [SerializeField] float distance = 1f;

    [HideInInspector] public int InputIndex = 0;

    [HideInInspector] public Case[,] Cases;

    int CurrentLine = 0;

    RectTransform rectTransform;
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        Cases = new Case[Size.x, Size.y];
        manager = FindObjectOfType<GameManager>();
        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                Cases[i, j] = Instantiate(framing, framing.transform.position + new Vector3(distance * j, -distance * i), framing.transform.rotation, transform);
                Cases[i, j].index = j;
                Cases[i, j].WordIndex = i;
                Cases[i, j].enabled = true;
            }
        }
        CantSelect(CurrentLine);
    }

    public void CantSelect(int currentLine)
    {
        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                if (i != currentLine)
                    Cases[i, j].InputF.interactable = false;
                else
                    Cases[i, j].InputF.interactable = true;
            }
        }
    }

    public void NewSelect()
    {
        if (InputIndex < Size.x - 1)
        {
            EventSystem.current.SetSelectedGameObject(null);
            InputIndex++;
        }
    }

    public void Deselect()
    {
        if (InputIndex > 0)
        {
            EventSystem.current.SetSelectedGameObject(null);
            InputIndex--;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            Deselect();
            Cases[CurrentLine, InputIndex].ClearChar();
        }
        Cases[CurrentLine, InputIndex].InputF.Select();
    }

    public void CheckWord()
    {
        int numberOfChar = 0;
        bool[] alreadySet = new bool[manager.CurrentWord.Length];
        for (int i = 0; i < alreadySet.Length; i++)
            alreadySet[i] = false;
        // Green Check
        for (int i = 0; i < manager.CurrentWord.Length; i++)
        {
            //Check for each char if same
            if (CheckCharacter(Cases[CurrentLine, i].InputF.text.ToCharArray()[0], manager.CurrentWord[i]))
            {
                alreadySet[i] = true;
                ChangeInputColor(ref Cases[CurrentLine, i].InputF, Color.green);
                SetCharForInput(manager.CurrentWord[i], i);
                numberOfChar++;
            }
        }
        // Yellow Check
        // For each Character
        for (int i = 0; i < manager.CurrentWord.Length; i++)
        {
            for (int j = 0; j < manager.CurrentWord.Length; j++)
            {
                if (CheckCharacter(Cases[CurrentLine, i].InputF.text.ToCharArray()[0], manager.CurrentWord[j]) && !alreadySet[j])
                {
                    alreadySet[j] = true;
                    ChangeInputColor(ref Cases[CurrentLine, i].InputF, Color.yellow);
                }
            }
        }
        CurrentLine++;
        CantSelect(CurrentLine);
        InputIndex = -1;
        if (CurrentLine != Size.y - 1)
            NewSelect();
        if (numberOfChar == Size.x)
            Restart();
    }

    public void Restart()
    {
        manager.NewWord();
        Size.x = manager.CurrentWord.Length;
        foreach (var i in Cases)
        {
            Destroy(i);
        }
        System.Array.Clear(Cases, 0, Cases.Length);
        Cases = new Case[Size.x, Size.y];
        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                Cases[i, j] = Instantiate(framing, framing.transform.position + new Vector3(distance * j, -distance * i), framing.transform.rotation, transform);
                Cases[i, j].index = j;
                Cases[i, j].WordIndex = i;
                Cases[i, j].enabled = true;
            }
        }
        CantSelect(CurrentLine);
    }

    void SetCharForInput(char a, int index)
    {
        if (CurrentLine < Size.y - 1)
            Cases[CurrentLine + 1, index].InputF.text = a.ToString();
    }

    public void ChangeInputColor(ref InputField input, Color to)
    {
        var color = input.colors;
        color.selectedColor = to;
        color.normalColor = to;
        color.disabledColor = to;
        color.highlightedColor = to;
        input.colors = color;
    }

    public bool CheckCharacter(char a, char b)
    {
        a = char.ToUpper(a);
        b = char.ToUpper(b);
        return a == b;
    }
}
