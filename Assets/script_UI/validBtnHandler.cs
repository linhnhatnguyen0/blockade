using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class validBtnHandler : MonoBehaviour
{
    public GameObject phaseHandler;
    // Start is called before the first frame update
    public void ChangePhase()
    {
        phaseHandler.GetComponent<PhaseHandler>().changePhaseHandler();
    }
}
