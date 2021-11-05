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
    private Vector3 startPos, endPos;
    private int dir;
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
        ChooseSpawn();
        cherry = Instantiate(cherryPrefab, startPos, Quaternion.identity);
        count = timer;
        cherry.name = "Cherry";
    }

    private void Fly()
    {
        float timeFraction = (timer - count) / 8;
        cherry.transform.position = Vector2.Lerp(startPos, endPos, timeFraction);
    }

    private void Destroy()
    {
        if (cherry.transform.position == endPos)
            Destroy(cherry);
    }

    void ChooseSpawn()
    {
        dir = Random.Range(1, 4);
        switch(dir)
        {
            case 1:
                randSpawn = Random.Range(-23, 23);
                startPos = new Vector3(randSpawn, 17, 0);
                endPos = new Vector3(-randSpawn, -17, 0);
                break;
            case 2:
                randSpawn = Random.Range(-17, 17);
                startPos = new Vector3(30, randSpawn, 0);
                endPos = new Vector3(-30, -randSpawn, 0);
                break;
            case 3:
                randSpawn = Random.Range(-23, 23);
                startPos = new Vector3(-randSpawn, -17, 0);
                endPos = new Vector3(randSpawn, 17, 0);
                break;
            case 4:
                randSpawn = Random.Range(-17, 17);
                startPos = new Vector3(-30, -randSpawn, 0);
                endPos = new Vector3(30, randSpawn, 0);
                break;
        }
    }
}
