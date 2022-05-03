using UnityEngine;
using UnityEngine.UI;

public class WaitScript : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<Image>().fillAmount = Time.time % 1;
    }
}
