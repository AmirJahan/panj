using System.Collections;
using System.IO;
using UnityEngine;
using TMPro;

public class ShareScript : MonoBehaviour
{
	[SerializeField] GameObject topLeftCloseButton;
	[SerializeField] TextMeshProUGUI scoreText;
	[SerializeField] GameObject winLossPanel;

	public void Share ()
    {
		winLossPanel.SetActive(false);
		StartCoroutine(TakeScreenshotAndShare());
	}

	private IEnumerator TakeScreenshotAndShare()
	{

		// change the score text to whatever
		string morningAfternoon = "صبح";

		if (AppData.session.id.Contains("PM"))
			morningAfternoon = "عصر";

		string[] sessionPieces = AppData.session.id.Split('_');
		string day = sessionPieces[0];

		int point = Mathf.Abs(AppData.session.score);
		string sign = AppData.session.score > 0 ? "مثبت" : "منفی";

		string panj = "#پنج_حرفی";
		string id = $"#پنج_حرفی_{morningAfternoon}_{day}_ام";

		scoreText.text = $"{point} امتیاز {sign} در پنج حرفی {morningAfternoon} روز {day} ام";

		// hide the close button
		topLeftCloseButton.SetActive(false);



		yield return new WaitForEndOfFrame();



		// let's make a square image in the middle half and share it
		// inc point, wordle id
		int width = (int)(Screen.width);
		int height = (int)(Screen.height * 0.57f);
		int y = (int)(Screen.height * .4);


		Rect regionToReadFrom = new Rect(0, y, width, height);

		

		Texture2D ss = new Texture2D(width, height, TextureFormat.RGB24, false);
		ss.ReadPixels(regionToReadFrom, 0, 0);
		ss.Apply();



		string filePath = Path.Combine(Application.temporaryCachePath, "shared_img.png");
		File.WriteAllBytes(filePath, ss.EncodeToPNG());


		// To avoid memory leaks
		Destroy(ss);


		// show the close button
		topLeftCloseButton.SetActive(true);
		gameObject.GetComponent<GameManager>().SetScore();








		new NativeShare().AddFile(filePath)
			//.SetSubject("Hello!")
			.SetText(panj + " " + id)
            .SetUrl("https://oddinstitute.com/panjharfi")
            .SetCallback((result, shareTarget) => gameObject.GetComponent<SceneNavManager>().GoToHome())
			.Share();

		// Share on WhatsApp only, if installed (Android only)
		//if( NativeShare.TargetExists( "com.whatsapp" ) )
		//	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();




	}
}
