using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections.Generic;


public class SetRow : MonoBehaviour
{
    [SerializeField]
    Image[] row_letters;


    public void clearRow()
    {
        foreach (Image any in row_letters)
        {
            any.color = new Color(.7f, .7f, .7f) ;

            TextMeshProUGUI text = any.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

            text.text = "";
        }
    }

    public void setWithChars(List<char> chars)
    {
        clearRow();

        for (int i = 0; i < chars.Count; i++)
        {
            row_letters[i].color = new Color(.6f, .6f, .6f);
            TextMeshProUGUI thisText = row_letters[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            thisText.text = chars[i].ToString();
        }
    }



    public void setWithLetters(List<Letter> letters)
    {
        clearRow();


        for (int i = 0; i < letters.Count; i++)
        {
            // Debug.Log("Setgting some");
            Color finalColor = Color.gray;
            TextMeshProUGUI thisText = row_letters[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();


            if (letters[i].status == LetterStatus.Found)
                finalColor = MyColors.mygreen;
            else if (letters[i].status == LetterStatus.Useless)
                finalColor = MyColors.useless;
            else if (letters[i].status == LetterStatus.Misplaced)
                finalColor = MyColors.misplaced;



            row_letters[i].color = finalColor;

            thisText.text = letters[i].character.ToString();
        }
    }
}
