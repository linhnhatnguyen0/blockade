using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controle_Diapo_Icon_Joueurs : MonoBehaviour
{
    public Texture2D[] imageList;
    public RawImage panelImageJ1;
    public RawImage panelImageJ2;
    private int index;
    public int numeroJoueur;
    public Controle_Diapo_Icon_Joueurs boutonCourant;
    public Controle_Diapo_Icon_Joueurs boutonAutre;


    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        AfficherMaterialActuel();
        //Debug.LogError(index);
    }

    void AfficherMaterialActuel()
    {
        if (numeroJoueur == 1)
        {
            panelImageJ1.texture = imageList[index];
            //Debug.LogError(index);
            PlayerPrefs.SetInt("IndexIconeJ1", index);
        }
        else
        {
            panelImageJ2.texture = imageList[index];
            //Debug.LogError(index);
            PlayerPrefs.SetInt("IndexIconeJ2", index);
        }
        //Debug.Log(PlayerPrefs.GetInt("IndexIconeJ1") + "-" + PlayerPrefs.GetInt("IndexIconeJ1"));
    }

    // M�thode pour afficher le mat�riau � l'index
    public void AfficherMaterialSuivant()
    {
        if (numeroJoueur == 1)
        {
            index = boutonCourant.index;

            index = (index + 1) % imageList.Length;
            //Debug.LogError(index
            AfficherMaterialActuel();
            //PlayerPrefs.SetInt("IndexIconeJ1", index);
        }
        else
        {
            index = boutonCourant.index = index;
            //Debug.LogError(index);
            index = (index + 1) % imageList.Length;
            //Debug.LogError(index
            AfficherMaterialActuel();
            //PlayerPrefs.SetInt("IndexIconeJ2", index);
        }
        //Debug.LogError(index);
    }

    // M�thode pour afficher le mat�riau pr�c�dent
    public void AfficherMaterialPrecedent()
    {
        if (numeroJoueur == 1)
        {
            index = boutonAutre.index;
            //Debug.LogError(index);
            index = (index - 1 + imageList.Length) % imageList.Length;
            //Debug.LogError(index
            AfficherMaterialActuel();
            boutonAutre.index = index;
        }
        else
        {
            index = boutonAutre.index;
            //Debug.LogError(index);
            index = (index - 1 + imageList.Length) % imageList.Length;
            //Debug.LogError(index
            AfficherMaterialActuel();
            boutonAutre.index = index;
        }
        //Debug.LogError(index);
    }
}
