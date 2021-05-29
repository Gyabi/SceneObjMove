using UnityEngine;
// シーンビューのカメラ移動を再現
/// https://www.fast-system.jp/unity%EF%BC%9A%E3%83%9E%E3%82%A6%E3%82%B9%E3%81%A7scene%E3%83%93%E3%83%A5%E3%83%BC%E3%81%AE%E3%82%88%E3%81%86%E3%81%AA%E3%82%AB%E3%83%A1%E3%83%A9%E6%93%8D%E4%BD%9C%E3%82%92%E3%81%99%E3%82%8B/
// planeの定義：https://docs.unity3d.com/ja/current/ScriptReference/Plane-ctor.html
// 第一引数：を通り、第二引数を法線とする平面の生成
[RequireComponent(typeof(Camera))]
public class SceneViewCamera : MonoBehaviour
{
  [SerializeField, Range(0.1f, 10f)]
  private float wheelSpeed = 1f;

  [SerializeField, Range(0.1f, 10f)]
  private float moveSpeed = 0.3f;

  [SerializeField, Range(0.1f, 10f)]
  private float rotateSpeed = 0.3f;

  private Vector3 preMousePos;

 MainCameraManeger mainCameraManeger;

  private void Awake(){
    mainCameraManeger = MainCameraManeger.Instance;
  }

  private void Update()
  {
    if(!mainCameraManeger.ObjMove && mainCameraManeger.CameraMove && !mainCameraManeger.ObjRotate){
      MouseUpdate();
    }
    return;
  }

  private void MouseUpdate()
  {
    float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
    if(scrollWheel != 0.0f)
      MouseWheel(scrollWheel);

    if(Input.GetMouseButtonDown(0) ||　Input.GetMouseButtonDown(1) ||　Input.GetMouseButtonDown(2))
      preMousePos = Input.mousePosition;

    MouseDrag(Input.mousePosition);
  }

  private void MouseWheel(float delta)
  {
    transform.position += transform.forward * delta * wheelSpeed;
    return;
  }

  private void MouseDrag(Vector3 mousePos)
  {
    Vector3 diff = mousePos - preMousePos;

    if(diff.magnitude < Vector3.kEpsilon)
      return;

    if(Input.GetMouseButton(0)){
        if(Input.GetKey(KeyCode.LeftAlt)){
          CameraRotate(new Vector2(-diff.y, diff.x) * rotateSpeed);        
        }else{
            transform.Translate(-diff * Time.deltaTime * moveSpeed);
        }
    }
    preMousePos = mousePos;
  }

  public void CameraRotate(Vector2 angle)
  {
    transform.RotateAround(transform.position, transform.right, angle.x);
    transform.RotateAround(transform.position, Vector3.up, angle.y);
  }
}


// Rayとplaneを用いてヒットしたときに特定方向（オブジェクトの向きに合わせた）かつオブジェクトを通る平面の作成及び座標の取得を行い移動をさせる。
