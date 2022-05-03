using System;
using System.Collections;
using UnityEngine.Networking;


public class ReadServer
{
    public static IEnumerator GetVersion(Action<string> versionIs)
    {
        string versionUrl = "https://oddinstitute.com/panjharfi/version_jhksfkj.php";
        UnityWebRequest www = UnityWebRequest.Get(versionUrl);
        
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            // network error. Try another time

            // if error, return negative one
            versionIs("negative");
            yield return null;
        }

        string foundVersion = www.downloadHandler.text;


        // otherwise, return the actual verion
        versionIs(foundVersion);
    }





    public static IEnumerator readEverything(Action<bool> callBack)
    {
        string server = "https://oddinstitute.com/panjharfi/";
        string wordsTextUrl =           server + AppData.wordsFileName + ".txt";
        string allWordsTextUrl =        server + AppData.allWordsFileName + ".txt";
        string winMessagesTextUrl =     server + AppData.winMessagesFileName +".txt";
        string loseMessagesTextUrl =    server + AppData.loseMessagesFileName + ".txt";



        // ALL WORDS
        UnityWebRequest www_all = UnityWebRequest.Get(allWordsTextUrl);
        yield return www_all.SendWebRequest();
        if (www_all.result == UnityWebRequest.Result.ConnectionError)
        {
            callBack(false);
            yield return null;
        }

        AppData.allWords_downloadHandler_text = www_all.downloadHandler.text;


        // WORDS
        UnityWebRequest www_words = UnityWebRequest.Get(wordsTextUrl);
        yield return www_words.SendWebRequest();
        if (www_words.result == UnityWebRequest.Result.ConnectionError)
        {
            callBack(false);
            yield return null;
        }
        AppData.words_downloadHandler_text = www_words.downloadHandler.text;


        // WIN MESSAGES
        UnityWebRequest www_win = UnityWebRequest.Get(winMessagesTextUrl);
        yield return www_win.SendWebRequest();
        if (www_win.result == UnityWebRequest.Result.ConnectionError)
        {
            callBack(false);
            yield return null;
        }
        AppData.wins_downloadHandler_text = www_win.downloadHandler.text;


        // LOSE MESSAGES
        UnityWebRequest www_lose = UnityWebRequest.Get(loseMessagesTextUrl);
        yield return www_lose.SendWebRequest();
        if (www_lose.result == UnityWebRequest.Result.ConnectionError)
        {
            callBack(false);
            yield return null;
        }
        AppData.lose_downloadHandler_text = www_lose.downloadHandler.text;

        callBack(true);
    }
}
