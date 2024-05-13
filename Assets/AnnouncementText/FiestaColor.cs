using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Random;
using System.Collections;
public class FiestaColor : MonoBehaviour
{
    public TMP_Text _fiest_text;
    
    
    void Start()
    {
        
        // Assurez-vous que le composant Text est assigné dans l'inspecteur
        if (_fiest_text == null)
        {
            Debug.LogError("Le composant Text n'est pas assigné.");
            return;
        }
        StartCoroutine(waitASecond());
        

        // Applique la première couleur de la liste
        
    }

    IEnumerator waitASecond(){
        while (true){
            _fiest_text.color =new Color(Range(0f,1f), Range(0f,1f), Range(0f,1f));
            yield return new WaitForSeconds(0.1f);
        }
        
        
    }

    
}
