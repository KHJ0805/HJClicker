using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonCreate : MonoBehaviour
{
    public GameObject balloon;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BalloonCreating", 3.0f, 5.0f);
    }

    void BalloonCreating()
    {
        Instantiate(balloon);
    }
}
