using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{

    public GameObject cherryPrefab;
    private GameObject cherry = null;
    private float timer = 0;
    private float count = 0;
    private float randSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer-count > 10)
        {
            SpawnCherry();
            
            count = timer;
        }
        if (cherry != null)
        {
            Fly();
            Destroy();
        }
    }

    void SpawnCherry()
    {
        randSpawn = Random.Range(-20, 20);
        cherry = Instantiate(cherryPrefab, new Vector3(randSpawn, 17, 0), Quaternion.identity);
        cherry.name = "Cherry";
        Debug.Log(cherry.transform.position);
    }

    private void Fly()
    {
        float timeFraction = (Time.time - count) / 8;
        cherry.transform.position = Vector2.Lerp(new Vector3(randSpawn, 17, 0), new Vector3(-randSpawn, -17, 0), timeFraction);
    }

    private void Destroy()
    {
        if (cherry.transform.position == new Vector3(-randSpawn, -17, 0))
            Destroy(cherry);
    }
}
