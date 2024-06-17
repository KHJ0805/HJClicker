using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    void Start()
    {
        float x = Random.Range(-3.0f, 8.0f);
        float y = -10.0f;
        transform.position = new Vector2(x, y);
    }

    void Update()
    {
        Destroy(gameObject, 10.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.instance.BalloonClick();
            Destroy(gameObject);
        }
    }
}
