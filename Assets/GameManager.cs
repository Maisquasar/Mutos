using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Grid grid;
    public string CurrentWord;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        StartCoroutine(WaitForStart());
    }

    public void NewWord()
    {
        CurrentWord = WordsDatas.GetRandomWordBySize((int)grid.Size.x);
        Debug.Log(CurrentWord);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitForStart()
    {
        yield return new WaitUntil(() => WordsDatas.isInitialized);
        NewWord();
    }
}
