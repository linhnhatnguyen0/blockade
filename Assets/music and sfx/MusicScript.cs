using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicScript : MonoBehaviour
{
    public AudioSource src;
    public AudioClip menu_music,game_music;
    private AudioClip current_music;
    public static MusicScript instance;
    private void Awake(){
        if (instance == null){
            Debug.Log("in =null");
            current_music=menu_music;
            instance=this;
            stopit();
            DontDestroyOnLoad(gameObject);
        }else
        {
            Debug.Log("in =else");
            current_music=game_music;
            //stopit();
            //Destroy(gameObject);
            //src.clip=game_music;
            //src.Play();
            //Debug.Log(""+src.clip.name);
        }
        
        
    }
    private void Start(){
        src.clip=current_music;
        
        src.Play();
        //src.clip=game_music;
        
    }
    private void stopit(){
        src.Stop();
    }
}
