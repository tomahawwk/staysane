using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioSource anxientSource;
    private float audio1Volume = 0.1f;
    private float audio2Volume = 0.0f;
    public float _transition = 1.0f;
    public AudioClip anxientTheme;
    public void Start(){
        audioSource = GetComponent<AudioSource>();
        anxientSource.volume = 0f;
    }

    public void AnxientOn(){
        StartCoroutine(AnxientOnTimeout());
    }
    public void AnxientOff(){
        StartCoroutine(AnxientOffTimeout());
    }

    IEnumerator FadeOut(){
		while(anxientSource.volume >= audio2Volume){
			yield return anxientSource.volume -= 0.007f * Time.deltaTime * _transition;
		}
	}

    IEnumerator FadeIn(){
		while(anxientSource.volume <= audio1Volume){
			yield return anxientSource.volume += 0.007f * Time.deltaTime * _transition;
		}
	}

    IEnumerator AnxientOnTimeout(){
        anxientSource.clip = anxientTheme;
        anxientSource.Play();
        yield return new WaitForSeconds(.5f);
        StartCoroutine(FadeIn());
	}

    IEnumerator AnxientOffTimeout(){
		StartCoroutine(FadeOut());
        yield return new WaitForSeconds(6f);
        anxientSource.Stop();
	}
}
