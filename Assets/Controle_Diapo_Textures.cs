using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Controle_Diapo_Textures : MonoBehaviour
{
    public Texture[] textures; // Liste des matériaux à afficher
    public RawImage panelImage; // Référence au composant RawImage du panneau
    private int index; // Indice actuel dans la liste des matériaux
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
                //Debug.LogError("Le composant RawImage n'a pas été trouvé.");
                return;
            }
        }

        if (textures.Length == 0)
        {
            //Debug.LogError("Aucun matériau à afficher n'a été assigné.");
            return;
        }

        AfficherMaterialActuel();
    }

    // Méthode pour afficher le matériau actuel dans le panneau
    void AfficherMaterialActuel()
    {
        panelImage.texture = textures[index];
        //Debug.LogError(panelImage.texture);
        PlayerPrefs.SetInt("IndexSol", index);
    }

    // Méthode pour afficher le matériau à l'index
    public void AfficherMaterialSuivant()
    {
        index = boutonCourant.index;
        //Debug.LogError(index);
        index = (index + 1) % textures.Length;
        //Debug.LogError(index);
        boutonCourant.index = index;
        AfficherMaterialActuel();
    }

    // Méthode pour afficher le matériau précédent
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
