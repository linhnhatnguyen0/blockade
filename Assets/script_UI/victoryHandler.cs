using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class victoryHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void returnHome()
    {
        SceneManager.LoadScene("PlayNewGame");
    }

    public void restartGame()
    {
        SceneManager.LoadScene("PlayGameTexture");
    }

    public void restartGameBot()
    {
        SceneManager.LoadScene("PlayGameTextureBOT");
    }
}
