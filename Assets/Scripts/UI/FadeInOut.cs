using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {
	
	public static bool sceneEnd;
	public float fadeSpeed = 1.5f;
	public int nextLevel;
	private Image _image;
	private bool sceneStarting;
	public UISounds uiSounds;
	void Awake ()
	{
		_image = GetComponent<Image>();
		_image.enabled = true;
		sceneStarting = true;
		sceneEnd = false;
		Cursor.visible = false;
	}

	void Update ()
	{
		if(sceneStarting) StartScene();
		if(sceneEnd) EndScene();
	}

	public void StartScene ()
	{
		_image.enabled = true;
		_image.color = Color.Lerp(_image.color, Color.clear, fadeSpeed * Time.deltaTime);

		if(_image.color.a <= 0.01f)
		{
			_image.color = Color.clear;
			_image.enabled = false;
			sceneStarting = false;
			Cursor.visible = true;
		}
	}

	public void EndScene ()
	{
		_image.enabled = true;
		_image.color = Color.Lerp(_image.color, Color.black, fadeSpeed * Time.deltaTime);

		if(_image.color.a >= 0.95f)
		{
			Cursor.visible = false;
			_image.color = Color.black;
			Application.LoadLevel(nextLevel);
		}
	}

	public void Toggle() {
		uiSounds.PlayChangeLocation();
		StartCoroutine(FadeToggle());
		StartCoroutine(FadeImage());
	}

	IEnumerator FadeToggle(){
		StartCoroutine(FadeImage());
		yield return new WaitForSeconds(1f);
		StartCoroutine(FadeImage(false));
	}

	IEnumerator FadeImage(bool fadeToBlack = true, float fadeSpeed = 6f){
        Color objectColor = _image.color;
		float fadeAmount;

		if(fadeToBlack){
			_image.enabled = true;
			while(_image.color.a < 1){
				fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
				objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
				_image.color = objectColor;
				yield return null;
			}
		}
		else{
			while(_image.color.a > 0){
				fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
				objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
				_image.color = objectColor;
				yield return null;
			}
		}
    }
}