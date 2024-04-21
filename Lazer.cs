using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lazer : MonoBehaviour{
  private LineRenderer line;

  //初期設定
  public void SetUp(){
    line = gameObject.GetComponent<LineRenderer>();
  }

  //マテリアルの設定
  public void SetMaterial(Material material){
    List<Material> materials = new List<Material>(){material};
    line.SetMaterials(materials);
  }

  //レーザーの位置を設定
  public void SetPosition(Vector3 startPos, Vector3 endPos){
    line.SetPosition(0, startPos);
    line.SetPosition(1, endPos);
  }

  //レーザーのマテリアルを返す
  public Material GetMaterial(){
    return line.material;
  }
}