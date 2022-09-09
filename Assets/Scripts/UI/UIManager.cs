using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    [Header("Модальные окна")]
    public GameObject _pauseMenu;
    public AudioMixer audioMixer;
    bool modalActive = false;
    bool paused = false;
    public CharacterController _controller;

    [Header("Настройки")]
    bool isFullScreen = false;
    Resolution[] rsl;
    List<string> resolutions;
    public Dropdown resolutionDropdown;
    public Volume postprocessing;
    private FilmGrain grain;
    public Slider grainSlider;
    public Slider sfxSlider;

    [Header("Триггеры")]
    public GameObject trigger;
    public Text triggerText;
    public Text triggerButtonText;

    private void Start(){
        VolumeProfile profile = postprocessing.sharedProfile;
        if (!profile.TryGet<FilmGrain>(out grain))
        {
            grain = profile.Add<FilmGrain>(false);
        }

        //Загрузка сохраненных настроек, если такие были
        
        if(PlayerPrefs.HasKey("Grain")){
            float saveGrain = PlayerPrefs.GetFloat("Grain");
            grain.intensity.Override(saveGrain);
            grainSlider.value = saveGrain;
        }

        if(PlayerPrefs.HasKey("Quality")){
            int saveQuality = PlayerPrefs.GetInt("Quality");
            Quality(saveQuality);
        }

        if(PlayerPrefs.HasKey("SFXVolume")){
            float saveSFX = PlayerPrefs.GetFloat("SFXVolume");
            SFXVolume(saveSFX);
            sfxSlider.value = saveSFX;
        }

        // if(PlayerPrefs.HasKey("Resolution")){
        //     int saveResolution = PlayerPrefs.GetInt("Resolution");
        //     Resolution(saveResolution);
        //     resolutionDropdown.value = saveResolution;
        // }
    }

    private void OpenGamePause(){
        _pauseMenu.SetActive(true);
        OpenModal();
    }

    private void CloseGamePause(){
        _pauseMenu.SetActive(false);
        CloseModal();
    }

    private void OpenModal(){
        modalActive = true;
        Time.timeScale = 0;
        _controller.enabled = false;
    }

    private void CloseModal(){
        modalActive = false;
        Time.timeScale = 1;
        _controller.enabled = true;
    }

    private void Update(){
        if (Input.GetKeyDown (KeyCode.Escape)) {
			if (!paused) {
                paused = true;
				OpenGamePause();
			} else {
                paused = false;
				CloseGamePause();
			}
		}
    }

    public void Awake(){
        resolutions = new List<string>();
        rsl = Screen.resolutions;
        foreach (var i in rsl)
        {
            resolutions.Add(i.width +"x" + i.height);
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutions);
    }

    public void FullScreenToggle(){
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void SFXVolume(float sliderValue){
        audioMixer.SetFloat("sfxVolume", sliderValue);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }

    public void MusicVolume(float sliderValue){
        audioMixer.SetFloat("musicVolume", sliderValue);
    }

    public void Quality(int q){
        QualitySettings.SetQualityLevel(q);
        PlayerPrefs.SetInt("Quality", q);
    }

    // public void Resolution(int r){
    //     Screen.SetResolution(rsl[r].width, rsl[r].height, isFullScreen);
    //     PlayerPrefs.SetInt("Resolution", r);
    // }

    public void Grain(float g){
        grain.intensity.Override(g);
        PlayerPrefs.SetFloat("Grain", g);
    }

    // Trigger
    public void Trigger(bool active, string text, string button){
        trigger.SetActive(active);
        triggerText.text = text;
        triggerButtonText.text = button;
    }
}