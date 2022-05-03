using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class GetFrom : MonoBehaviour
{
    string wordsUrl = "https://oddinstitute.com/panjharfi/read_wordsxfcgvhkjmhvg.php";
    string createUserURL = "https://oddinstitute.com/panjharfi/my_write.php";


    void Start()
    {
        StartCoroutine(GetText());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(CreateUser("Amir", "Amir Pass", "amir.jahan@gmail.com"));
    }


    public IEnumerator CreateUser(string username, string password, string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("UserNamePost", username);
        form.AddField("PasswordPost", password);
        form.AddField("EmailPost", email);

        UnityWebRequest www = UnityWebRequest.Post(createUserURL, form);
        www.chunkedTransfer = false;
        yield return www.SendWebRequest();

        if (www.error == null)
        {
            // you can place code here for handle a succesful post
            Debug.Log("Made Success");

        }
        else
        {
            // what to do on error
            Debug.Log("Failed");

        }

    }





    IEnumerator GetText()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(wordsUrl))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string foundText = www.downloadHandler.text;

                string[] words = foundText.Split(',');




                // Show results as text
                Debug.Log("Found: " + words.Length + " words");
                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;

                for (int i = 0; i< words.Length; i++)
                {
                    Debug.Log(i + " is: " + words[i]);
                }

                
            }
        }
    }
}
