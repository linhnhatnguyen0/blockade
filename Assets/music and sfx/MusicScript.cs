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
            Debug.Log(menu_music);
            current_music=menu_music;
            instance=this;
            //stopit();
            DontDestroyOnLoad(gameObject);
        }else
        {
            /*
            Debug.Log("in =else");
            Debug.Log(game_music);
            current_music=game_music;
            stopit();
            */
            Debug.Log(this);
            Destroy(gameObject);
            //src.clip=game_music;
            //src.Play();
            //Debug.Log(""+src.clip.name);
        }
        
        
    }
    private void Start(){
        //Debug.Log(menu_music);
        src.clip=current_music;
        
        src.Play();
        //src.clip=game_music;
        
    }
    private void stopit(){
        Debug.Log("We are in function");
        current_music=game_music;
        src.clip=current_music;
    }
}
