using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beard : MonoBehaviour
{
    public int hits;
    public int hitThreshold;

    public void CountCuts()
    {
        hits++;

        if(hits > hitThreshold)
        {
            GameManager.instance.cuts++;
        }
    }
}
