using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfx;
   
   public void ButtonClickSound(){
        src.clip=sfx;
        src.Play();
   }
}
