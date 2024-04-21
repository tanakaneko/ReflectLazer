using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LazerMother : MonoBehaviour
{
  [SerializeField] private GameObject lazerPrefab;
  const float MAX_DISTANCE = 20.0f;  //箱の対角線の長さ
  [SerializeField] private int REFLECT_NUM = 5;  //反射回数

  //レーザーを動かす
  private GameObject preWall;
  private GameObject[] lazers;
  private Lazer[] lazerCSs;
  public Vector3 nowOrigin;
  public Vector2 nowDirection;

  //色変更
  private Material startMaterial;
  private Material nowMaterial;

  //レーザーを停止
  private bool isReflect;
 
  /**
   * レーザー生成関数
   * @param {Vector3} origin - レーザーの原点
   * @param {Vector3} direction - レーザーの方向
   * @param {Material} m - レーザーの最初のマテリアル
  **/
  public void creat(Vector3 origin, Vector2 direction, Material m){
    lazers = new GameObject[REFLECT_NUM + 1];
    lazerCSs = new Lazer[REFLECT_NUM + 1];
    for(int n = 0; n < REFLECT_NUM + 1; n++){
      GameObject lazerChild = Instantiate(lazerPrefab, this.transform);
      lazers[n] = lazerChild;
      lazerCSs[n] = lazerChild.GetComponent<Lazer>();
      lazerCSs[n].SetUp();
    }
    lazerCSs[0].SetMaterial(m);
    startMaterial = m;
    move(origin,direction);
  }

  /**
   * レーザー移動時に実行する関数
   * @param {Vector3} origin - レーザーの原点
   * @param {Vector3} direction - レーザーの方向
  **/
  public void move(Vector3 origin, Vector2 direction){
    preWall = null;
    nowOrigin = origin;
    nowDirection = direction;
    Vector3 startPos = origin;
    Vector2 nextDirection = direction;
    nowMaterial = startMaterial;
    isReflect = true;
    for(int i = 0; i < REFLECT_NUM + 1; i++){
      if(isReflect){
        lazers[i].SetActive(true);
      }
      else{
        lazers[i].SetActive(false);
        continue;
      }
      //衝突した壁の検知
      RaycastHit2D[] hits = Physics2D.RaycastAll(startPos, nextDirection, MAX_DISTANCE);
      foreach(RaycastHit2D hit in hits){
        GameObject hitObj = hit.collider.gameObject;
        if (hitObj != preWall){
          //lineの描画
          Vector3 endPos = hit.point;
          lazerCSs[i].SetMaterial(nowMaterial);
          lazerCSs[i].SetPosition(startPos, endPos);
          //壁の種類の判別
          if(hitObj.tag == "Wall"){
            Vector2 reflectDirection = Vector2.Reflect(nextDirection, hit.normal);
            switch(hitObj.GetComponent<Wall>().type){
              case Wall.WallType.Default:
              break;
              case Wall.WallType.LazerColorChange:
              nowMaterial = hitObj.GetComponent<Renderer>().material;
              break;
              case Wall.WallType.WallColorChange:
              hitObj.GetComponent<Wall>().SetMaterial(nowMaterial);
              break;
              case Wall.WallType.NonReflect:
              isReflect = false;
              break;
              case Wall.WallType.DestroyWall:
              Destroy(hitObj);
              break;
            }
            //次の繰り返しに引き渡す変数
            startPos = endPos;
            nextDirection = reflectDirection;
            preWall = hitObj;
          }
          break;
        }
      }
    }
  }

  //getter
  public Vector3 getOrigin(){
    return nowOrigin;
  }
  public Vector2 getDirection(){
    return nowDirection;
  }
}