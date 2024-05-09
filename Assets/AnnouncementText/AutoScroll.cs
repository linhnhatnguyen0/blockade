using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AutoScroll : MonoBehaviour
{
    float speed=200.0f ;
    float boundaryTextEnd=1480.0f;
    Vector3 restart;
    RectTransform myGorectTransform;
    [SerializeField]
    TextMeshProUGUI mainText;
    bool isLooping=true;
    void Start()
    {
        myGorectTransform=gameObject.GetComponent<RectTransform>();
        StartCoroutine(AutoScrollText());
    }

    IEnumerator AutoScrollText(){
        restart=myGorectTransform.localPosition;

        //Boucle faisant déplacer la banderole
        while(myGorectTransform.localPosition.x <boundaryTextEnd){
            //déplacement du text
            myGorectTransform.Translate(Vector3.right * speed * Time.deltaTime);
            if(myGorectTransform.localPosition.x>boundaryTextEnd){
                if(isLooping){
                    //repositionnement de la banderole à la bordure gauche et attente de 3 secondes
                    myGorectTransform.localPosition=restart;  
                    yield return new WaitForSeconds(3);
                }else{
                    break;
                }
            }
            yield return null;
        }
        
    }

 
    
}
