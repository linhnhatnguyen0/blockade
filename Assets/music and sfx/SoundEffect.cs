using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfxBtn,sfxPawn,sfxWall,sfxEndphase;
   
   public void ButtonClickSound(){
        Debug.Log("in");
        src.clip=sfxBtn;
        src.Play();
   }
   public void EndphaseSound(){
        src.clip=sfxEndphase;
        src.Play();
   }
   public void WallSound(){
        src.clip=sfxWall;
        src.Play();
   }
   public void PawnSound(){
        src.clip=sfxPawn;
        src.Play();
   }
}
