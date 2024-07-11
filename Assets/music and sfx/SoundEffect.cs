using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfxBtn, sfxPawn, sfxWallFalse, sfxWallTrue, sfxEndphase;

    public void ButtonClickSound()
    {
        src.clip = sfxBtn;
        src.Play();
    }
    public void EndphaseSound()
    {
        src.clip = sfxEndphase;
        src.Play();
    }
    public void WallSoundTrue()
    {
        src.clip = sfxWallTrue;
        src.Play();
    }
    public void PawnSound()
    {
        src.clip = sfxPawn;
        src.Play();
    }
    public void WallSoundFalse()
    {
        src.clip = sfxWallFalse;
        src.Play();
    }
}
