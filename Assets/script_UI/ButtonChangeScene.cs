using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonChangeScene : MonoBehaviour
{
    public void ChargerNouvelleScene(string nomScene)
    {
        if (nomScene == "PlayNewGame")
        {
            GameObject musicManager = GameObject.Find("MusicAudioSource");
            Destroy(musicManager);
        }
        SceneManager.LoadScene(nomScene);
    }
}
