using UnityEngine;
using UnityEngine.UI;

public class KeyboardEntry : MonoBehaviour
{
    public void keyPressed(Button button)
    {
        if (AppData.currentChars.Count != 5)
        {
            // add the new character to the sequence
            AppData.currentChars.Add(button.name[0]);

            gameObject.GetComponent<GameManager>().setCurrentRow();

            gameObject.GetComponent<SubmitDelManager>().refreshDelSubButtons();

        }
    }
}