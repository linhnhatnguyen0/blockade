using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class settings_menu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetMusicVolume (float volume){
        audioMixer.SetFloat("MusicVolume", volume);
    }
    public void SetSFXVolume (float volume){
        audioMixer.SetFloat("SFXVolume", volume);
    }
}
