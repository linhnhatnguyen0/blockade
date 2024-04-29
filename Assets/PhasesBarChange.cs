using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhasesBarChange : MonoBehaviour
{
    private Color newcolor;
    private Color basecolor;
    public int state; //à enlever
    public Slider PawnWall;
    public Slider WallEnd;

    // Start is called before the first frame update
       public static void ChangeColor(int state){
        newcolor= new Color(255,0,0,1f);
        basecolor= new Color(255,255,255,1f);
        GameObject backgroundObj_PawnWall = PawnWall.transform.Find("Background").gameObject;
        GameObject backgroundObj_WallEnd = WallEnd.transform.Find("Background").gameObject;
        switch (state)
        {
            case 0:
            backgroundObj_PawnWall.GetComponent<Image>().color = newcolor;
                break;
            case 1:
                backgroundObj_WallEnd.GetComponent<Image>().color = newcolor;
                break;
            case 2:
                backgroundObj_PawnWall.GetComponent<Image>().color = basecolor;
                backgroundObj_WallEnd.GetComponent<Image>().color = basecolor;
                break;
            default:
                break;
        }
        
    }
    private static void ChangeStateHandler(int state){
        this.state=state;
        ChangeColor();
    } 
}
