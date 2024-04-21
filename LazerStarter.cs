using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerStarter : MonoBehaviour
{
  [SerializeField] private GameObject lazerMother;
  [SerializeField] private Vector2 INITIAL_DIRECTION = new Vector2(0.3f,1); //最初の方向
  [SerializeField] private  float ROTATE_SPEED = 0.04f;  //回転速度
  [SerializeField] private Material START_COLOR;
  private GameObject newLazer;
  private LazerMother lazerMotherCS;

  public void Start(){
    newLazer = Instantiate(lazerMother, this.gameObject.transform.position, Quaternion.identity);
    lazerMotherCS = newLazer.GetComponent<LazerMother>();
    lazerMotherCS.creat(newLazer.transform.position, INITIAL_DIRECTION, START_COLOR);
  }

  public void Update(){
    // 左回転
    if (Input.GetKey (KeyCode.LeftArrow)) {
      lazerMotherCS.move(lazerMotherCS.getOrigin(), Quaternion.Euler(0f, 0f, ROTATE_SPEED) * lazerMotherCS.getDirection());
    }
    // 右回転
    if (Input.GetKey (KeyCode.RightArrow)) {
      lazerMotherCS.move(lazerMotherCS.getOrigin(), Quaternion.Euler(0f, 0f, -ROTATE_SPEED) * lazerMotherCS.getDirection());
    }
  }
}