using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicScript : MonoBehaviour
{
    public AudioSource src;
    public AudioClip menu_music;
    
    public static MusicScript instance;
    public  GameObject go;
    private void Awake(){
        if (instance == null){   
            instance=this;
            DontDestroyOnLoad(go);
        }  
        
    }
    private void Start(){
     
        src.clip=menu_music;
        src.Play();   
    }
}
