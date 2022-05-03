using System.Collections.Generic;



public static class AppData
{
    public static char[] alphabet = { 'ض', 'ذ', 'ق', 'ظ', 'غ', 'ع', 'پ', 'خ', 'ح', 'چ', 'گ', 'ش', 'ث', 'ی', 'ل', 'ط', 'ژ', 'ص', 'ن', 'م', 'ک', 'ج', 'ف', 'ب', 'ا', 'ز', 'ر', 'ت', 'د', 'ه', 'و', 'س' };
    public static int numberOfRows = 6;

    public static int periodScore = 0;


    public static string wordsFileName = "words_xhcgjgkhlfchgjvbjk";
    public static string allWordsFileName = "all_words_srgsbsrtb";
    public static string winMessagesFileName = "win_messages_jckhvad";
    public static string loseMessagesFileName = "lose_messages_ajkhrvfkrv";


    public static string words_downloadHandler_text;
    public static string allWords_downloadHandler_text;
    public static string wins_downloadHandler_text;
    public static string lose_downloadHandler_text;



    public static Session session;


    // this has to be fetched only if the version of the local
    // words is different than the server ones
    public static List<string> allWords = new List<string>();
    public static List<string> winMessages = new List<string>();
    public static List<string> loseMessages = new List<string>();


    public static List<char> currentChars = new List<char>();

    // i think this is not re-build properly
    // it makes the samw set of characters all times
    public static List<Letter> currentLetters = new List<Letter>();





    public static bool isThisRealWord()
    {
        string curWord = "";
        foreach (char c in currentChars)
            curWord = curWord + c;


        bool wordIsCorrect = false;

        foreach (string any in AppData.allWords)
        {
            // Debug.Log("Testing " + any + " that is " + any.Length);
            if (any == curWord)
            {
                wordIsCorrect = true;
                break;
            }
        }

        return wordIsCorrect;
    }


    public static void calculateScore ()
    {
        session.score = 0;
        foreach (Letter let in session.keyboardLetters)
            session.score += let.score;
    }
}