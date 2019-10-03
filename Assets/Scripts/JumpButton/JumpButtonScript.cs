using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData data){
        if(PlayerJumpScript.instance != null){
            PlayerJumpScript.instance.SetPower(true);
        }
    }

    public void OnPointerUp(PointerEventData data){
        PlayerJumpScript.instance.SetPower(false);
    }
}
