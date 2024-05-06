using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controle_Diapo_Icon_Joueurs : MonoBehaviour
{
    //public Sprite[] imageList;
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
            PlayerPrefs.SetInt("IndexIconeJ1", index);
        }
        else
        {
            panelImageJ2.texture = imageList[index];
            PlayerPrefs.SetInt("IndexIconeJ2", index);
        }
    }

    // Méthode pour afficher le matériau à l'index
    public void AfficherMaterialSuivant()
    {
        if (numeroJoueur == 1)
        {
            index = boutonCourant.index;

            index = (index + 1) % imageList.Length;
            //Debug.LogError(index
            AfficherMaterialActuel();
            PlayerPrefs.SetInt("IndexIconeJ1", index);
        }
        else
        {
            index = boutonCourant.index = index;
            //Debug.LogError(index);
            index = (index + 1) % imageList.Length;
            //Debug.LogError(index
            AfficherMaterialActuel();
            PlayerPrefs.SetInt("IndexIconeJ2", index);
        }
        //Debug.LogError(index);
    }

    // Méthode pour afficher le matériau précédent
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
