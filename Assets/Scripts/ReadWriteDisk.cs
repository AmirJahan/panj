using UnityEngine;
using System.IO;
using System;

public static class ReadWriteDisk
{
    // this should only be called when we press the submit
    public static void SaveSessionToJson()
    {
        // in setting the score, we set the session ID to a value
        // it means the session exists, also it has its actual score
        SaveScore();

        string sessionString = JsonUtility.ToJson(AppData.session);

        string jsonPath = $"{AppData.session.id}.json";
        string path = Application.persistentDataPath + "/" + jsonPath;


        File.WriteAllText(path, sessionString);
    }


    // we could have not started, started and finished or unfinished
    public static Session ReadSession(string id)
    {
        // when we want to read session, we check if it has a key,
        // if it does, we read the actual session
        if (PlayerPrefs.HasKey(id))
        {
//             Debug.Log("Id exists");
            string jsonPath = $"{id}.json";
            string path = Application.persistentDataPath + "/" + jsonPath;

            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                var session = JsonUtility.FromJson<Session>(jsonString);
                // Debug.Log("Fpound: " + session.ToString());

                return session;
            }
            else
                return null;
        }
        else
            return null;

    }

    // this happens after each submit
    public static void SaveScore()
    {
        // this sets the current session id to the score we got for it
        // it also determines whether this session has been played
        PlayerPrefs.SetInt(AppData.session.id, AppData.session.score);

        // here, we set the high score
        //AppData.preiodScore += AppData.session.score;

        //PlayerPrefs.SetInt("preiodScore", AppData.preiodScore);
    }

    public static void GetTenDayScore ()
    {
        AppData.periodScore = 0;
        int day = int.Parse(AppData.session.id.Split('_')[0]);
        for (int i = day; i > day - 10; i--)
        {
            AppData.periodScore += PlayerPrefs.GetInt($"{day}_AM", 0);
            AppData.periodScore += PlayerPrefs.GetInt($"{day}_PM", 0);
        }
    }




    public static void writeTextToPersistent (string text, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName;
        File.WriteAllText(path, text);
    }


    // this should happen just once at the beginning
    public static void copyAllToPersistentData ()
    {
       // Debug.Log("We are copying");

        string text_all_words = Resources.Load<TextAsset>(AppData.allWordsFileName).text;
        writeTextToPersistent(text_all_words, AppData.allWordsFileName);

        string text_words = Resources.Load<TextAsset>(AppData.wordsFileName).text;
        writeTextToPersistent(text_words, AppData.wordsFileName);

        string text_wins = Resources.Load<TextAsset>(AppData.winMessagesFileName).text;
        writeTextToPersistent(text_wins, AppData.winMessagesFileName);

        string text_lose = Resources.Load<TextAsset>(AppData.loseMessagesFileName).text;
        writeTextToPersistent(text_lose, AppData.loseMessagesFileName);
    }






    public static void ReadWordStuff()
    {
        //Debug.Log("Reading words");

        AppData.allWords.Clear();
        string allWordsAssetPath = Application.persistentDataPath + "/" + AppData.allWordsFileName;

        if (File.Exists(allWordsAssetPath))
        {
//            Debug.Log("File exists words");

            string allWordsString = File.ReadAllText(allWordsAssetPath);
            string[] all_words = allWordsString.Split(',');
            foreach (string any in all_words)
                if (any.Trim().Length == 5)
                    AppData.allWords.Add(any.Trim());

            // Debug.Log("Found " + AppData.allWords.Count);

        }
    }

    public static void ReadWinLoseStuff()
    {
        string winMessagesAssetPath = Application.persistentDataPath + "/" + AppData.winMessagesFileName;

        if (File.Exists(winMessagesAssetPath))
        {
            string winMessagesString = File.ReadAllText(winMessagesAssetPath);
            string[] win_messages = winMessagesString.Split('\n');
            foreach (string any in win_messages)
                    AppData.winMessages.Add(any.Trim());
        }

        string loseMessagesAssetPath = Application.persistentDataPath + "/" + AppData.loseMessagesFileName;

        if (File.Exists(loseMessagesAssetPath))
        {
            string loseMessagesString = File.ReadAllText(loseMessagesAssetPath);
            string[] lose_messages = loseMessagesString.Split('\n');
            foreach (string any in lose_messages)
                AppData.loseMessages.Add(any.Trim());
        }
    }



    public static (string word, string id) ReadSessionData()
    {
        DateTime startDate = new DateTime(2022, 04, 20, 10, 10, 10);

        // LOCAL
        DateTime nowDate = DateTime.Now;

        // UTC
        //DateTime nowDate = DateTime.UtcNow;

        int today = (int)((nowDate - startDate).TotalDays);

        string sign = "AM";
        int hIndicator = 0;

        int hour = nowDate.Hour;

        //Debug.Log("Hour is: " + hour);

        if (nowDate.ToString("tt") == "PM")
        {
            sign = "PM";
            hIndicator = 1;
            hour += 12;
        }



        //Debug.Log("Hour is: " + hour);


        //int hour = 0;
        //if (h >= 0 && h < 3) hour = 1;
        //else if (h >= 3 && h < 6) hour = 2;
        //else if (h >= 6 && h < 9) hour = 3;
        //else if (h >= 9 && h < 12) hour = 4;
        //else if (h >= 12 && h < 15) hour = 5;
        //else if (h >= 15 && h < 18) hour = 6;
        //else if (h >= 18 && h < 21) hour = 7;
        //else if (h >= 21 && h < 24) hour = 8;


        string wordsAssetPath = Application.persistentDataPath + "/" + AppData.wordsFileName;
        if (File.Exists(wordsAssetPath))
        {
            string wordsString = File.ReadAllText(wordsAssetPath);
            string[] words = wordsString.Split(',');
            int arrLength = words.Length;
            int index = ((today * 2) + hIndicator) % arrLength;


            string word = words[index];
            string id = today.ToString() + "_" + sign;

            return (word: word, id: id); // named tuple elements in a literal
        }

        return (word: "", id: "");
    }
}
