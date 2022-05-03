using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] Image[] rows;

    [SerializeField] GameObject winLossPanel;
    [SerializeField] TextMeshProUGUI winLossText, shareButtonText;
    [SerializeField] TextMeshProUGUI scoreText;





    void Start()
    {
        // we need to invoke something that regularly checks if we ran out of time
        // if so, we've lost, we should see the score window


        SetScore();


        clearAll();
        // this test is needed for the very beginning
        // AppData.isThisRealWord();

        // this sets the number of rows dynamically.
        AppData.numberOfRows = rows.Length;

        setRowsBySessions();


        winLossPanel.SetActive(false);




        gameObject.GetComponent<KeyboardManager>().RefreshKeyboard();

    }






    void setRowsBySessions()
    {
        if (AppData.session.curRow > 4)
            rows[4].GetComponent<SetRow>().setWithLetters(AppData.session.row_4);
        if (AppData.session.curRow > 3)
            rows[3].GetComponent<SetRow>().setWithLetters(AppData.session.row_3);
        if (AppData.session.curRow > 2)
            rows[2].GetComponent<SetRow>().setWithLetters(AppData.session.row_2);
        if (AppData.session.curRow > 1)
            rows[1].GetComponent<SetRow>().setWithLetters(AppData.session.row_1);
        if (AppData.session.curRow > 0)
            rows[0].GetComponent<SetRow>().setWithLetters(AppData.session.row_0);
    }


    


    public void setCurrentRow()
    {
        rows[AppData.session.curRow].GetComponent<SetRow>().setWithChars(AppData.currentChars);
    }


 

    public void clearAll()
    {
        foreach (Image row in rows)
            row.GetComponent<SetRow>().clearRow();
    }



    public void submit()
    {
        int foundCount = 0;

        for (int i = 0; i < AppData.currentChars.Count; i++)
        {
            LetterStatus status = LetterStatus.Useless;

            if (AppData.currentChars[i] == AppData.session.word[i])
            {
                status = LetterStatus.Found;
                foundCount++;
            }
            else if (AppData.session.word.Contains(AppData.currentChars[i].ToString()))
                status = LetterStatus.Misplaced;

            // let's first make a Letter
            Letter letter = new Letter(AppData.currentChars[i], status, 0);
            AppData.currentLetters.Add(letter);
        }


        rows[AppData.session.curRow].GetComponent<SetRow>().setWithLetters(AppData.currentLetters);
        AppData.currentChars.Clear();
        gameObject.GetComponent<KeyboardManager>().RefreshKeyboard();




        SetSessionRowLetters();





        if (foundCount == 5)
        {
            AppData.session.finished = true;
            didWeWin(true);
        }
        else
        {
            AppData.session.curRow++;

            if (AppData.session.curRow == 6) // we lost. This was the last row. 6 will not be active 
            {
                AppData.session.finished = true;
                didWeWin(false);
            }
        }

        SetScore();
        // this will also save the score
        ReadWriteDisk.SaveSessionToJson();


        // we do this at the end of each submit. Then, there is always
        // a clear current letters
        AppData.currentLetters.Clear();


    }

    public void SetScore()
    {
        AppData.calculateScore();

        int point = Mathf.Abs(AppData.session.score);
        string sign = AppData.session.score > 0 ? "مثبت" : "منفی";

        if (AppData.session.curRow == 0)
        {
            if (AppData.session.score == 0)
                scoreText.text = $"تا الان که امتیاز هیچ";
            else
                scoreText.text = $"تا الان {point} امتیاز {sign} گرفتی";
        }
        else
        {
            if (AppData.session.score == 0)
                scoreText.text = $"شدیم صفر امتیازی";
            else
                scoreText.text = $"شدیم {point} امتیاز {sign}";
        }

    }



    void SetSessionRowLetters()
    {
        void CopyLettersToRow(List<Letter> row)
        {
            foreach (Letter any in AppData.currentLetters)
                row.Add(any);
        }

        switch (AppData.session.curRow)
        {
            case 0:
                CopyLettersToRow(AppData.session.row_0);
                //AppData.session.row_0 = AppData.curLetters;
                break;
            case 1:
                CopyLettersToRow(AppData.session.row_1);
                //AppData.session.row_1 = AppData.curLetters;
                break;
            case 2:
                CopyLettersToRow(AppData.session.row_2);
                //AppData.session.row_2 = AppData.curLetters;
                break;
            case 3:
                CopyLettersToRow(AppData.session.row_3);
                //AppData.session.row_3 = AppData.curLetters;
                break;
            case 4:
                CopyLettersToRow(AppData.session.row_4);
                //AppData.session.row_4 = AppData.curLetters;
                break;
            case 5:
                CopyLettersToRow(AppData.session.row_5);
                //AppData.session.row_5 = AppData.curLetters;
                break;
            default:
                break;
        }
    }






    void didWeWin(bool win, bool timedOut = false)
    {
        winLossPanel.SetActive(true);
        // disable keyboard
        gameObject.GetComponent<KeyboardManager>().ToggleKeyboardTo(false);

        if (timedOut)
        {
            winLossText.text =  "لفتش دادی، وقت تموم شد";
            shareButtonText.text = "شیر کنم این پیروزی رو؟";
        }
        else
        {
            if (win)
            {
                winLossText.text = AppData.winMessages[Random.Range(0, AppData.winMessages.Count)];
                shareButtonText.text = "شیر کنم حماسه رو؟";

            }
            else
            {
                winLossText.text = AppData.loseMessages[Random.Range(0, AppData.loseMessages.Count)];
                shareButtonText.text = "این دستاورد رو شیر کنم؟";
                

            }
        }



    }
}
