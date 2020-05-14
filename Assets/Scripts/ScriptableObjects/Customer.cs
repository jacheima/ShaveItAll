using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "World", menuName = "Customer")]
public class Customer : ScriptableObject
{
    public GameObject prefab; //the customer head prefab

    public float willPay; //the amount they pay up front
    public float tipModifier; //number multiplied to amount remaining to calculate tip
    public float refundThreshold; //percent remaining on face
    public float killThreshold; //amount of cuts before customer dies

    public bool isAlive;
}
