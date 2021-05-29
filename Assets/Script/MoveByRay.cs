using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
// https://www.ame-name.com/archives/4631
// 左クリック→rayを発射→ヒットしたものが特定のタグ付きobject→フラグを立たせる
// フラグが立っている時→オブジェクト内のcenterと特定のarrowのスクリーン座標を取得→arrowの向きを特定→マウスの差分がarrowとあっていれば正方向、逆なら不方向に移動させる
public class MoveByRay : MonoBehaviour
{ 
    Transform arrow; 
    string arrowTag;

    bool moving;

    Vector3 preMousePoint;
    Vector3 arrowScreenPoint;
    Vector3 centerScreenPoint;
    Vector3 mousePoint;

    // arrowScreenとceterScreenの差分
    Vector3 ACsub;

    MainCameraManeger mainCameraManeger;

    private void Awake(){
        mainCameraManeger = MainCameraManeger.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
    }
 
    // Update is called once per frame
    void Update()
    {
        if(mainCameraManeger.ObjMove && !mainCameraManeger.CameraMove && !mainCameraManeger.ObjRotate){
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit, Mathf.Infinity))
                {
                    if (hit.collider.tag == "Y" || hit.collider.tag == "X" || hit.collider.tag =="Z")
                    {
                        arrowTag = hit.collider.tag;
                        arrow = hit.transform;
                        preMousePoint = Input.mousePosition;
                        moving = true;
                        arrowScreenPoint = Camera.main.WorldToScreenPoint(arrow.position);
                        centerScreenPoint = Camera.main.WorldToScreenPoint(arrow.parent.gameObject.transform.Find("CenterPoint").gameObject.transform.position);
                        ACsub = arrowScreenPoint - centerScreenPoint;
                    }
                }
            }
    
    
            if (moving)
            {
                Vector3 mousePoint = Input.mousePosition;
                Vector3 Msub = mousePoint - preMousePoint;
                float x = 0.0f;

                if((ACsub.x < 0 && Msub.x < 0) || (ACsub.x >= 0 && Msub.x >= 0)){  
                    x += Mathf.Abs(Msub.x);
                }else{
                    x -= Mathf.Abs(Msub.x);
                }
                Debug.Log(Msub);
                
                if((ACsub.y < 0 && Msub.y < 0) || (ACsub.y >= 0 && Msub.y >= 0)){  
                    x += Mathf.Abs(Msub.y);
                }else{
                    x -= Mathf.Abs(Msub.y);
                }

                if(arrowTag == "Y"){
                    arrow.parent.transform.position += arrow.parent.transform.up*0.01f*x;
                }else if(arrowTag == "X"){
                    arrow.parent.transform.position += arrow.parent.transform.right*0.01f*x;
                }else if(arrowTag == "Z"){
                    arrow.parent.transform.position += arrow.parent.transform.forward*0.01f*x;
                }
                preMousePoint = mousePoint;
                
                if (Input.GetMouseButtonUp(0))
                {
                    moving = false;
                }
            }
        }
    }
}