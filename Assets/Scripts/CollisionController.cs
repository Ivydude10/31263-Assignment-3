using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        switch(other.name)
        {
            case "Pellet":
            case "Power Pellet":
            case "Cherry":
                GameManager.Instance.EatItem(other.name);
                Destroy(other.gameObject);
                break;

        }
    }
}
