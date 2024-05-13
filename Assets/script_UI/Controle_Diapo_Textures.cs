using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Controle_Diapo_Textures : MonoBehaviour
{
    public Texture[] textures; // Liste des mat�riaux � afficher
    public RawImage panelImage; // R�f�rence au composant RawImage du panneau
    private int index; // Indice actuel dans la liste des mat�riaux
    public Controle_Diapo_Textures boutonCourant;
    public Controle_Diapo_Textures boutonAutre;

    void Start()
    {
        index = 0;
        //Debug.LogError(index);
        if (panelImage == null)
        {
            panelImage = GetComponentInChildren<RawImage>();
            if (panelImage == null)
            {
                //Debug.LogError("Le composant RawImage n'a pas �t� trouv�.");
                return;
            }
        }

        if (textures.Length == 0)
        {
            //Debug.LogError("Aucun mat�riau � afficher n'a �t� assign�.");
            return;
        }

        AfficherMaterialActuel();
    }

    // M�thode pour afficher le mat�riau actuel dans le panneau
    void AfficherMaterialActuel()
    {
        panelImage.texture = textures[index];
        //Debug.LogError(panelImage.texture);
        PlayerPrefs.SetInt("IndexSol", index);
    }

    // M�thode pour afficher le mat�riau � l'index
    public void AfficherMaterialSuivant()
    {
        index = boutonCourant.index;
        //Debug.LogError(index);
        index = (index + 1) % textures.Length;
        //Debug.LogError(index);
        boutonCourant.index = index;
        AfficherMaterialActuel();
    }

    // M�thode pour afficher le mat�riau pr�c�dent
    public void AfficherMaterialPrecedent()
    {
        index = boutonAutre.index;
        //Debug.LogError(index);
        index = (index - 1 + textures.Length) % textures.Length;
        //Debug.LogError(index);
        boutonAutre.index = index;
        AfficherMaterialActuel();
    }
}
