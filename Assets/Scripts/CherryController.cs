using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{

    public GameObject cherryPrefab;
    public Camera main;
    private GameObject cherry;
    private Tween tween;
    private float timer = 0;
    private float count = 0;
    private float randSpawn;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(main.scaledPixelWidth);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer-count > 10)
        {
            SpawnCherry();
            Fly();
            count = timer;
        }
    }

    void SpawnCherry()
    {
        randSpawn = Random.Range(-20, 20);
        cherry = Instantiate(cherryPrefab, new Vector3(randSpawn, 17, 0), Quaternion.identity);
    }

    private void Fly()
    {
        float timeFraction = (Time.time - count) / 10;
        cherry.transform.position = Vector2.Lerp(new Vector3(randSpawn, 17, 0), new Vector3(-randSpawn, -17, 0), timeFraction);
    }
}
