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
    public void Message(int state, int player){
        _test_text.text="coucou";
        
        switch (state)
        {
            case PAWN:    
                _test_text.text="Déplace ton pion joueur "+player+" !"; 
                break;  

            case  WALL:
                _test_text.text="Place un mur joueur "+player+" !";
                break;  

            case ENDTURN:
                _test_text.text="ENDTURN"+player; 
                break; 

            case ERROR:
                _test_text.text="Tu ne peux pas faire ça joueur "+player+" :emojicolère:";
                break;

            case OTHERPLAYER:
                _test_text.text ="OTHERPLAYER";
                break;

            default:
                _test_text.text = "Phase non gérée";
                break;
        }
    }
}
