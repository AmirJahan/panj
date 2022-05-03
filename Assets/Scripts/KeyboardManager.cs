using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KeyboardManager : MonoBehaviour
{
    public List<Button> keyboardButtons;

    void drawKeyboard()
    {
        for (int i = 0; i < keyboardButtons.Count; i++)
        {

            Letter thisKeyLetter = AppData.session.keyboardLetters[i];

            keyboardButtons[i].name = thisKeyLetter.character.ToString();


            Color keyColor = MyColors.defKeyboard;

            switch (thisKeyLetter.status)
            {
                case LetterStatus.Unknown:
                    keyColor = MyColors.defKeyboard;
                    break;

                case LetterStatus.Found:
                    keyColor = MyColors.found;
                    break;

                case LetterStatus.Useless:
                    keyColor = MyColors.useless;
                    break;

                case LetterStatus.Misplaced:
                    keyColor = MyColors.misplaced;
                    break;

                default:
                    break;
            }


            keyboardButtons[i].GetComponent<Image>().color = keyColor;

            TextMeshProUGUI text = keyboardButtons[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

            text.text = AppData.session.keyboardLetters[i].character.ToString();
        }
    }


    // this gets a set of 5 letters from AppData CurLetter Set and re-draws the keyboard
    public void RefreshKeyboard()
    {
        // re-produce the keyboardLetters
        List<Letter> newKeyboardLetters = new List<Letter>();

        foreach (Letter keyboardLetter in AppData.session.keyboardLetters)
        {
            Letter newLetter = new Letter(keyboardLetter.character, keyboardLetter.status, keyboardLetter.score);

            foreach (Letter letter in AppData.currentLetters)
            {
                if (newLetter.character == letter.character)
                {
                    if (newLetter.status == LetterStatus.Unknown || newLetter.status == LetterStatus.Misplaced)
                        newLetter.status = letter.status;

                    break;
                }
            }

            newKeyboardLetters.Add(newLetter);
        }


        List<Letter> pointedKeyboard = assignKeyboardPoints(newKeyboardLetters);


        AppData.session.keyboardLetters.Clear();

        foreach (Letter let in pointedKeyboard)
            AppData.session.keyboardLetters.Add(let);

        drawKeyboard();
    }

    public void ToggleKeyboardTo(bool enabled)
    {
        foreach (Button key in gameObject.GetComponent<KeyboardManager>().keyboardButtons)
            key.interactable = enabled;
    }


    static List<Letter> assignKeyboardPoints(List<Letter> curKeyboardLetters)
    {
        List<Letter> resKeyboardLetters = new List<Letter>();

        foreach (Letter let in curKeyboardLetters)
        {
            int letterPoint = 0;
            if (let.score == 0)
            {
                switch (let.status)
                {
                    case LetterStatus.Found:
                        letterPoint = 5 * (AppData.numberOfRows - AppData.session.curRow); // 30
                        break;

                    case LetterStatus.Misplaced:
                        letterPoint = 1 * (AppData.numberOfRows - AppData.session.curRow);
                        break;

                    case LetterStatus.Useless:
                        letterPoint = -4;
                        break;

                    case LetterStatus.Unknown:
                        letterPoint = 0;
                        break;

                    default:
                        break;
                }
            }
            else
                letterPoint = let.score;


            Letter newLetter = new Letter(let.character,
                                            let.status,
                                            letterPoint);

            resKeyboardLetters.Add(newLetter);
        }
        return resKeyboardLetters;
    }

}