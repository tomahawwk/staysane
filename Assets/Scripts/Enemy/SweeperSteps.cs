using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SweeperSteps : MonoBehaviour
{
    public AudioSource audioS;
    public CinemachineVirtualCamera _camera;
    public AudioClip[]
        footsteps_asphalt;

    private float shakeTimer;

    public void PlayLeft(){
        
        audioS.clip = footsteps_asphalt[0];
        audioS.Play();
        StartCoroutine(ShakeTimeout());
    }
    public void PlayRight(){
        audioS.clip = footsteps_asphalt[1];
        audioS.Play();
        StartCoroutine(ShakeTimeout());
    }

    public void ShakeCamera(float intensity, float time){
        CinemachineBasicMultiChannelPerlin perlin = 
        _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

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

    IEnumerator ShakeTimeout() {
		yield return new WaitForSeconds(.0f);
        ShakeCamera(1.3f, .2f);
	}
}