using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Announcer : MonoBehaviour
{
    //public int state;
    public TMP_Text _test_text;
    const int PAWN=1;
    const int WALL=2;
    const int ENDTURN=3;
    const int ERROR=4;
    const int OTHERPLAYER=5;
    public void Message(int state){
        _test_text.text="coucou";
        
        switch (state)
        {
            case PAWN:    
                _test_text.text="PAWN"; 
                break;  

            case  WALL:
                _test_text.text="WALL";
                break;  

            case ENDTURN:
                _test_text.text="ENDTURN"; 
                break; 

            case ERROR:
                _test_text.text="ERROR";
                break;

            case OTHERPLAYER:
                _test_text.text ="OTHERPLAYER";
                break;

            default:
                _test_text.text = "UHOH";
                break;
        }
    }
}
