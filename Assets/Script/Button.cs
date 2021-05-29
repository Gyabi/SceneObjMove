using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    MainCameraManeger mainCameraManeger;

    private void Awake(){
        mainCameraManeger = MainCameraManeger.Instance;
    }
    public void clickObjMove(){
        Debug.Log("click!!!");
        if(mainCameraManeger.ObjMove){
            mainCameraManeger.ObjMove = false;
            mainCameraManeger.CameraMove = true;
            mainCameraManeger.HideMoveGuide();
        }else{
            mainCameraManeger.ObjMove = true;
            mainCameraManeger.ObjRotate = false;
            mainCameraManeger.CameraMove = false;
            mainCameraManeger.ShowMoveGuide();
            mainCameraManeger.HideRotateGuide();
        }
    }
    public void clickObjRotate(){
        if(mainCameraManeger.ObjRotate){
            mainCameraManeger.ObjRotate = false;
            mainCameraManeger.CameraMove = true;
            mainCameraManeger.HideRotateGuide();
        }else{
            mainCameraManeger.ObjRotate = true;
            mainCameraManeger.ObjMove = false;
            mainCameraManeger.CameraMove = false;
            mainCameraManeger.HideMoveGuide();
            mainCameraManeger.ShowRotateGuide();
        }
    }
}
