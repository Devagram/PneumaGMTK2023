using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "levelLoaderScriptableObject", menuName = "ScriptableObjects/Level")]
public class LevelSO : ScriptableObject
{
    public string levelName;
    public int pitCount;
    //public Chest chest;
    public int archerCount;
    public int spikeCount;
    public int springCount;
    public int skeleCount;
    public int spiderCount;
    public int chestCount;
    public float buildPhaseLimit;
    public int numHeroes;
    //public Object[] spawnPoints;
    //public Scene nextScene;
}
