using System.Collections.Generic;
using System;


[Serializable]
public class Session
{
    public int score = 0;



    public bool finished = false;
    public int curRow = 0;

    public string id = "";
    public string word = "";
    public List<Letter> keyboardLetters = new List<Letter>();


    public List<Letter> row_0 = new List<Letter>();
    public List<Letter> row_1 = new List<Letter>();
    public List<Letter> row_2 = new List<Letter>();
    public List<Letter> row_3 = new List<Letter>();
    public List<Letter> row_4 = new List<Letter>();
    public List<Letter> row_5 = new List<Letter>();



    public Session()
    {
        this.keyboardLetters.Clear();

        foreach (char i in AppData.alphabet)
        {
            Letter newLet = new Letter(i, LetterStatus.Unknown, 0);
            this.keyboardLetters.Add(newLet);
        }
    }
}
