using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HomeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;

    [SerializeField] GameObject waitPanel;

    [SerializeField] GameObject notificationText;
    [SerializeField] GameObject startButton;
    [SerializeField] TextMeshProUGUI startButtonText;

    void FetchContent()
    {
        // firs thing, read the version
        waitPanel.SetActive(true);
        StartCoroutine(ReadServer.GetVersion(version =>
        {
            // we found version from server and it is different than the current one
            if (version != "negative" && PlayerPrefs.GetString("version", "one") != version)
            {
                 // Debug.Log("Version CLOUD is: " + version);

                StartCoroutine(ReadServer.readEverything(everythingWasRead =>
                {
                    if (everythingWasRead)
                    {
                        // Everything is now set properly
                        ReadWriteDisk.writeTextToPersistent(AppData.allWords_downloadHandler_text,
                                                            AppData.allWordsFileName);

                        ReadWriteDisk.writeTextToPersistent(AppData.words_downloadHandler_text,
                                    AppData.wordsFileName);

                        ReadWriteDisk.writeTextToPersistent(AppData.wins_downloadHandler_text,
                                    AppData.winMessagesFileName);

                        ReadWriteDisk.writeTextToPersistent(AppData.lose_downloadHandler_text,
                                    AppData.loseMessagesFileName);

                        PlayerPrefs.SetString("version", version);
                    }
                    else
                    {
                        // some error happened
                    }

                }));
            }
            waitPanel.SetActive(false);

        }));

        // if we reached here, NOW....
        // read all the words and prepare the AppData.All_Words
        ReadWriteDisk.ReadWordStuff();
        ReadWriteDisk.ReadWinLoseStuff();
    }

    void RefreshTheGame()
    {
        FetchContent();

        

        // read session word and the ID
        var sessionData = ReadWriteDisk.ReadSessionData();
        string word = sessionData.word;
        string id = sessionData.id;


        var foundSession = ReadWriteDisk.ReadSession(id);

        // session can only ever be written after at least one submit
        if (foundSession != null)
            ContinueSession(foundSession);
        else
            BeginSession(word, id);

        // read my scores and show them
        DisplayMyPoints();
    }

    void ContinueSession(Session session)
    {
        AppData.session = session;

        startButton.GetComponent<Button>().interactable = true;
        startButton.GetComponent<Image>().color = Color.white;

        startButtonText.text = "ادامه بده";

        notificationText.SetActive(false);


        IsGameFinished();
    }

    void BeginSession(string word, string id)
    {
        // at the very beginning, let's make a session
        AppData.session = new Session();
        AppData.session.word = word;
        AppData.session.id = id;

        startButton.GetComponent<Button>().interactable = true;
        startButton.GetComponent<Image>().color = Color.white;

        startButtonText.text = "شروع کن";

        notificationText.SetActive(false);
    }

    void Start()
    {
          // PlayerPrefs.DeleteAll();


        //calculate the pints properly
        //    find ourt how the app goes to background
        //    what happnes when it comes to foreground
        //    remove those invokes




        // nothing has ever been copied. This is the first play
        if (!PlayerPrefs.HasKey("version"))
        {
            // Debug.Log("Doesn't have Key, let's copy?");
            ReadWriteDisk.copyAllToPersistentData();
            PlayerPrefs.SetString("version", "one");
        }

        RefreshTheGame();
    }

    void IsGameFinished()
    {
        if (AppData.session.finished)
        {
            string nextRound = "ساعت دوازده شب";
            if (AppData.session.id.Split('_')[1].Contains("AM"))
                nextRound = "بعد از ظهر";


            notificationText.SetActive(true);
            TextMeshProUGUI notifText = notificationText.GetComponent<TextMeshProUGUI>();
            notifText.text = "دست بعدی رفت " + nextRound + ". تو این فاصله به یه کار واجب برس.";

            startButtonText.text = "صب کن";

            startButton.GetComponent<Button>().interactable = false;
            startButton.GetComponent<Image>().color = MyColors.myorange;
        }
    }

    void DisplayMyPoints()
    {
        //AppData.preiodScore = PlayerPrefs.GetInt("preiodScore", 0);

        ReadWriteDisk.GetTenDayScore();
        highScoreText.text = AppData.periodScore.ToString();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        RefreshTheGame();
    }
}
