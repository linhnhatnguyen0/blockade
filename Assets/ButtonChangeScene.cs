using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonChangeScene : MonoBehaviour
{
    public void ChargerNouvelleScene(string nomScene)
    {
        SceneManager.LoadScene(nomScene);
    }
}
