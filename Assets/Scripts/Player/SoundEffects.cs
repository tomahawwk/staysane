using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SoundEffects : MonoBehaviour
{
    public CinemachineVirtualCamera _camera;
    private float shakeTimer;
    public AudioSource audioS;
    public AudioClip[] steps;
    public AudioClip[] runSteps;
    public AudioClip[] climbUpSteps;
    public AudioClip hangUp;
    public AudioClip hangDown;
    public AudioClip[] crouchSteps;
    public AudioClip[] jumpSounds;
    public AudioClip[] landSounds;
    public AudioClip anxientSound;
    public PlayerController controller;

    private void Update(){
        if(shakeTimer > 0){
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f){
                CinemachineBasicMultiChannelPerlin perlin = 
                _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                perlin.m_AmplitudeGain = 0f;
            }
        }
    }

    // Шаги
    public void PlayLeft(){
        audioS.clip = steps[Random.Range(0, (steps.Length / 2) - 1)];
        audioS.Play();
    }

    public void PlayRight(){
        audioS.clip = steps[Random.Range(steps.Length / 2, steps.Length - 1)];
        audioS.Play();
    }

    // Крадемся
    public void PlayCrouchLeft(){
        audioS.clip = crouchSteps[Random.Range(0, (crouchSteps.Length / 2) - 1)];
        audioS.Play();
    }

    public void PlayCrouchRight(){
        audioS.clip = crouchSteps[Random.Range(crouchSteps.Length / 2, crouchSteps.Length - 1)];
        audioS.Play();
    }

    // Лезем вверх
    public void PlayClimbLeft(){
        audioS.clip = climbUpSteps[Random.Range(0, (climbUpSteps.Length / 2) - 1)];
        audioS.Play();
    }

    public void PlayClimbRight(){
        audioS.clip = climbUpSteps[Random.Range(climbUpSteps.Length / 2, climbUpSteps.Length - 1)];
        audioS.Play();
    }

    // Прыгаем
    int test = 0;
    public void PlayJump(){
        audioS.Stop(); 
        audioS.clip = jumpSounds[Random.Range(0, jumpSounds.Length - 1)];
        audioS.Play();
        test++;
        Debug.Log("Jump - " + test);
    }

    // Падаем
    public void PlayLanding(){
        audioS.UnPause();
        audioS.clip = landSounds[Random.Range(0, landSounds.Length - 1)];
        audioS.Play();
        StartCoroutine(AfterFall());
    }

    public void ShakeCamera(float intensity, float time){
        CinemachineBasicMultiChannelPerlin perlin = 
        _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    IEnumerator AfterFall() {
        yield return new WaitForSeconds(.1f);
        ShakeCamera(3.5f, .1f);
	}

    // Залезаем на лестницу
    public void PlayHangUp(){  
        if(controller.ClimbMode){
            audioS.clip = hangUp;
            audioS.Play();
        }
    }

    // Спрыгиваем с лестницы
    public void PlayHangDown(){    
        audioS.clip = hangDown;
        audioS.Play();
        ShakeCamera(1f, .1f);
    }

    //Тревожный ах
    public void PlayAnxientAh(){
        audioS.clip = anxientSound;
        audioS.Play();
    }
}