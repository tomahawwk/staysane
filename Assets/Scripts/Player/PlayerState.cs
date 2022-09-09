using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerState : MonoBehaviour {
    public bool anxienty = false;
    public PostProcessVolume volume;
    ColorGrading colorGrading;
    Animator animator;
    PlayerController playerController;
    bool move = false;
    public TriggersManager triggersManager;
    Vignette vignette;
    public ParticleSystem HealAxient;
    public AmbientManager ambientManager;
    public void Start(){
        animator = GetComponent<PlayerController>()._anim;
        playerController = GetComponent<PlayerController>();

        volume.profile.TryGetSettings(out colorGrading);
        volume.profile.TryGetSettings(out vignette);
        if(anxienty){
            anxienty = false;
            AnxientyEnable();
        }
    }
    public void AnxientyEnable(){
        if(!anxienty){
            anxienty = true;
            AnxientyToggle(anxienty);
            StartCoroutine(AnxientyMove());
            playerController.controller.enabled = false;
            animator.Rebind();
            animator.SetTrigger("Anxienty");
            triggersManager.AnxientTrigger();
            ambientManager.AnxientOn();
        }
    }
    public void AnxientyDisable(){
        if(anxienty){
            anxienty = false;
            AnxientyToggle(anxienty);
            StartCoroutine(AnxientDisableTimeout());
            ambientManager.AnxientOff();
            triggersManager.AnxientHealTrigger();
            animator.ResetTrigger("AnxientIdle");
        }
    }

     public void AnxientyToggle(bool anxienty){
        StartCoroutine(AnxientyTimeout(anxienty));
        StartCoroutine(VignetteTimeout(anxienty));
        StartCoroutine(AmbientTimeout(anxienty));
    }

    void OnTriggerEnter(Collider other){
        if(other.GetComponent<AnxientyHeal>()){
            AnxientyDisable();
            other.GetComponent<AnxientyHeal>().Activated();
        }

        if(other.GetComponent<AnxientTrigger>()){
            other.GetComponent<AnxientTrigger>().UseTrigger();
        }
    }

    IEnumerator AxientAnimationTimeout()
    {
        for (; ; )
        {
            if(anxienty){
                AxientAnimationToggle();
            }
            yield return new WaitForSeconds(15f);
        }
    }
    void AxientAnimationToggle(){
        animator.SetTrigger("AnxientIdle");
    }

    IEnumerator AnxientyMove(){
        yield return new WaitForSeconds(.7f);
        if(playerController.rotateDirection == 0)
            StartCoroutine(c_Move(-2.5f, 1f));
        else if(playerController.rotateDirection == 1)
            StartCoroutine(c_Move(2.5f, 1f));
        //StartCoroutine(AxientAnimationTimeout());
    }
    IEnumerator AnxientyTimeout(bool active = true, float fadeSpeed = 30f){
		if(active){
			while(colorGrading.saturation.value > -100f){
				colorGrading.saturation.value = colorGrading.saturation.value - (fadeSpeed * Time.deltaTime);
				yield return null;
			}
		}
        else{
			while(colorGrading.saturation.value < 0f){
				colorGrading.saturation.value = colorGrading.saturation.value + (fadeSpeed * Time.deltaTime);
				yield return null;
			}
		}
        playerController.controller.enabled = true;
    }

    IEnumerator VignetteTimeout(bool active = true, float fadeSpeed = .03f){
        if(active){
            while(vignette.intensity.value < .3f){
				vignette.intensity.value = vignette.intensity.value + (fadeSpeed * Time.deltaTime);
				yield return null;
			}
		}
        else{
            while(vignette.intensity.value > .2f){
				vignette.intensity.value = vignette.intensity.value - (fadeSpeed * Time.deltaTime);
				yield return null;
			}
		}
    }

    IEnumerator AmbientTimeout(bool active = true, float fadeSpeed = .5f){
        if(active){
            while(RenderSettings.ambientIntensity > 3.7f){
				RenderSettings.ambientIntensity = RenderSettings.ambientIntensity - (fadeSpeed * Time.deltaTime);
				yield return null;
			}
		}
        else{
            while(RenderSettings.ambientIntensity < 4.5f){
				RenderSettings.ambientIntensity = RenderSettings.ambientIntensity + (fadeSpeed * Time.deltaTime);
				yield return null;
			}
		}
    }
    IEnumerator AnxientDisableTimeout(){
        yield return new WaitForSeconds(.2f);
        HealAxient.Play();
    }
    IEnumerator c_Move(float offsetX, float speed){
        if (move)
            yield break;
        else
            move = true;
    
        var p = transform.position;
        p.x += offsetX;
    
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, p, speed * Time.deltaTime);
            
            if (Mathf.Abs(transform.position.x - p.x) < 0.01f)
            {
                move = false;
                break;
            }
    
            yield return null;
        }
    }
}