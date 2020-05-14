using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "World", menuName = "World")]
public class World : ScriptableObject
{
    public Level[] levels; //an array of all the levels that exist in the world
}
