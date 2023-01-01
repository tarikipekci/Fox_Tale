using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
  public int gems;
  public Vector3 playerPosition;
  public int currentHealth;
  public bool direction;
  public Vector3[] frogPos;

  public GameData()
  {
      gems = 0;
      playerPosition = Vector3.zero;
      currentHealth = 6;
      direction = false;
      frogPos = new Vector3[4];
  }
}
