using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
  public enum WallType{  //壁の種類
    Default,
    LazerColorChange,
    WallColorChange,
    NonReflect,
    DestroyWall
  }

  public WallType type;

  //壁のマテリアルを設定
  public void SetMaterial(Material m){
    gameObject.GetComponent<Renderer>().material = m;
  }
}