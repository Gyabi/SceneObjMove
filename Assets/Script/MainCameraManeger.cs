using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManeger : MonoBehaviour
{
    private static MainCameraManeger instance;

    private bool objmove;
    private bool objrotate;
    private bool cameramove;

    GameObject[] XGuides;
    GameObject[] YGuides;
    GameObject[] ZGuides;
    GameObject[] RotateGuides;
    public bool ObjMove{
        get{
            return this.objmove;
        }
        set{
            this.objmove = value;
        }
    }

    public bool ObjRotate{
        get{
            return this.objrotate;
        }
        set{
            this.objrotate = value;
        }
    }

    public bool CameraMove{
        get{
            return this.cameramove;
        }
        set{
            this.cameramove = value;
        }
    }
    public static MainCameraManeger Instance{
        get{
            if(instance == null){
                GameObject obj = new GameObject("MainCameraManeger");
                instance = obj.AddComponent<MainCameraManeger>();
            }
            return instance;
        }
        set{

        }
    }

    void Awake(){
        SetGuides();
        HideMoveGuide();
        HideRotateGuide();
    }
    // Start is called before the first frame update
    void Start()
    {
        this.cameramove = true;
        this.objmove = false;
        this.objrotate = false;
    }
    public void SetGuides(){
        XGuides = GameObject.FindGameObjectsWithTag("X");
        YGuides = GameObject.FindGameObjectsWithTag("Y");
        ZGuides = GameObject.FindGameObjectsWithTag("Z");
        RotateGuides = GameObject.FindGameObjectsWithTag("Rotate");
    }
    public void HideMoveGuide(){
        List<GameObject[]> a = new List<GameObject[]>();
        a.Add(XGuides);
        a.Add(YGuides);
        a.Add(ZGuides);
        foreach(GameObject[] guides in a ){
            foreach(GameObject guide in guides){
                guide.SetActive(false);
            }
        }
    }
    public void HideRotateGuide(){
        foreach(GameObject guide in RotateGuides){
            guide.SetActive(false);
        }
    }
    public void ShowMoveGuide(){
        List<GameObject[]> a = new List<GameObject[]>();
        a.Add(XGuides);
        a.Add(YGuides);
        a.Add(ZGuides);
        foreach(GameObject[] guides in a ){
            foreach(GameObject guide in guides){
                guide.SetActive(true);
                Debug.Log("SetActive!!!!!!!");   
            }
        }
    }
    public void ShowRotateGuide(){
        foreach(GameObject guide in RotateGuides){
            guide.SetActive(true);
        }
    }
}
