using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class test_script : MonoBehaviour
{
    private Color newcolor;
    public Slider transi;
    // Start is called before the first frame update
       public void ChangeColor(){
        newcolor= new Color(255,0,0,1f);
        GameObject backgroundObj = transi.transform.Find("Background").gameObject;
        backgroundObj.GetComponent<Image>().color = newcolor;
        Debug.Log("Color in?");
        //transi.fillRect.GetComponent<Image>().color = newcolor;
        
         
    }
}
