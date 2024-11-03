using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class SelectableBehaviour : MonoBehaviour
{
    public int level;
    public int levelMax;
    protected GameObject player;

    abstract protected void LevelUp();
}