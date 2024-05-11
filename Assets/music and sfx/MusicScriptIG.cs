using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicScriptIG : MonoBehaviour
{
    public AudioSource src;
    public AudioClip game_music;
    private AudioClip current_music;
    public static MusicScriptIG instance;
    public  GameObject go;
    private void Awake(){
        src.clip=game_music;
        
        src.Play();
        
        
    }
}
