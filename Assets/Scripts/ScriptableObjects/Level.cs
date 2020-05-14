using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "World", menuName = "Level")]
public class Level : ScriptableObject
{
    public Customer[] customers; //list of scriptbale objects that holds the customers info

    public float timePerCustomer; //the time the player has to shave each customer
}
