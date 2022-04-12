using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class WordsDatas : MonoBehaviour
{
    public static bool isInitialized = false;
    public static List<string> wordsDatas = new List<string>();

    private void Start()
    {
        readTextFile("Assets/Datas/WordList2.txt");
        isInitialized = true;
    }

    void readTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path, Encoding.GetEncoding("iso-8859-1"));
        if (inp_stm == null)
            Debug.LogError("File not found !");
        while (!inp_stm.EndOfStream)
        {
            string word = inp_stm.ReadLine();
            word = word.ToUpper();
            wordsDatas.Add(word);
        }
        Debug.Log($"All {wordsDatas.Count} words Loaded !");
        inp_stm.Close();
    }

    public static int GetIdByWord(string Word)
    {
        for (int i = 0; i < wordsDatas.Count; i++)
        {
            if (string.Equals(wordsDatas[i], Word, StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }
        Debug.LogError($"Word {Word} not found");
        return -1;
    }

    public static string GetRandomWordBySize(int Size)
    {
        var tmp = new List<string>();
        for (int i = 0; i < wordsDatas.Count; i++)
        {
            if (wordsDatas[i].Length == Size)
                tmp.Add(wordsDatas[i]);
        }
        int index = UnityEngine.Random.Range(0, tmp.Count);
        if (tmp[index] != null)
            return tmp[index];
        Debug.LogError($"Word with Size {Size} not found");
        return null;
    }
}
