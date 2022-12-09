using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public Sprite[] dagomSpriteList;
  public string[] dagomNameList;
  public int[] dagomExpList;
  public int[] dagomCoinList;

  public Vector3[] PointList;

  public RuntimeAnimatorController[] LevelAc;
  public void ChangeAc(Animator anim, int level)
  {
    anim.runtimeAnimatorController = LevelAc[level-1];
  }
  
}
