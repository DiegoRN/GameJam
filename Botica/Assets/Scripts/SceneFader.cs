using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneFader : MonoBehaviour
{
	public Image fadeUIImage;
	public bool isOut = true;
	public float fadeSpeed = 0.8f;
	public bool changeSceneAfterFadeIn = false;
	public string sceneName;
	public enum FadeDirection
	{
		In, //Alpha = 1
		Out // Alpha = 0
	}
	void OnEnable()
	{
		if(changeSceneAfterFadeIn){
			StartCoroutine(FadeAndLoadScene(FadeDirection.In, sceneName));
		} else {
			if (isOut)
			{
				StartCoroutine(Fade(FadeDirection.Out));
			}
			else
			{
				StartCoroutine(Fade(FadeDirection.In));
			}
		}
		
	}

	private IEnumerator Fade(FadeDirection fadeDirection)
	{
		float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
		float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;
		if (fadeDirection == FadeDirection.Out)
		{
			while (alpha >= fadeEndValue)
			{
				SetColorImage(ref alpha, fadeDirection);
				yield return null;
			}
			fadeUIImage.enabled = false;
		}
		else
		{
			fadeUIImage.enabled = true;
			while (alpha <= fadeEndValue)
			{
				SetColorImage(ref alpha, fadeDirection);
				yield return null;
			}
		}
	}
	public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection, string sceneToLoad)
	{
		yield return Fade(fadeDirection);
		SceneManager.LoadScene(sceneToLoad);
	}

	private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
	{
		fadeUIImage.color = new Color(fadeUIImage.color.r, fadeUIImage.color.g, fadeUIImage.color.b, alpha);
		alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
	}
}
