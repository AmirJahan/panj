using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SubmitDelManager : MonoBehaviour
{
    [SerializeField] Button submitButton, deleteButton;
    [SerializeField] Sprite deleteActive, deleteDisabled;


    private void Start()
    {
        // At the very beginning
        refreshDelSubButtons();
    }


    public void delChar()
    {
        if (AppData.currentChars.Count == 0)
            return;




        int last = AppData.currentChars.Count - 1;
        // add the new character to the sequence
        AppData.currentChars.RemoveAt(last);
        gameObject.GetComponent<GameManager>().setCurrentRow();


        refreshDelSubButtons();
    }

    public void submit()
    {
        gameObject.GetComponent<GameManager>().submit();

        refreshDelSubButtons();
    }

    public void refreshDelSubButtons()
    {
        // DELETE BUTTON
        if (AppData.currentChars.Count > 0) // there are some characters
        {
            deleteButton.GetComponent<Image>().sprite = deleteActive;
            deleteButton.interactable = true;
        }
        else
        {
            deleteButton.GetComponent<Image>().sprite = deleteDisabled;
            deleteButton.interactable = false;
        }
  

        // SUBMIT BUTTON
        submitButton.interactable = false;
        TextMeshProUGUI text = submitButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        

        if (AppData.currentChars.Count == 0)
        {
            submitButton.GetComponent<Image>().color = Color.white;
            text.text = "بنویس";
        }
        else if (AppData.currentChars.Count < 5)
        {
            submitButton.GetComponent<Image>().color = MyColors.myorange;
            text.text = "کوتاهه";
        }
        else if (AppData.currentChars.Count == 5)
        {
            if (AppData.isThisRealWord())
            {
                submitButton.GetComponent<Image>().color = Color.white;
                text.text = "امتحان کن";
                submitButton.interactable = true;
            }

            else
            {
                submitButton.GetComponent<Image>().color = MyColors.myyellow;
                text.text = "کلمه نیست";
            }
        }
    }

}
