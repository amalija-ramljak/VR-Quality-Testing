using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRQualityTesting.Scripts.PickAndPlace;

public class NewScript : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    //[SerializeField] private GameObject obj2;
    [SerializeField] private GameObject obstacleToBeSpawned;
    [SerializeField] int numberOfObstacles;
    [SerializeField] Transform parent;
    private GameObject proxy;
    private List<GameObject> ProxyList = new List<GameObject>();
    private GameObject proxy_obj;
    //private GameObject proxy_obj2;
    Collider obstacle_Collider, obj_Collider;

    private List<Vector3> obstacle_pocetneKordinate = new List<Vector3>();
    private List<Vector3> obstacle_trenutneKordinate = new List<Vector3>();
    int broj_Dotaknutih_Kocaka = 0;

    private List<PAPObject> objects;

    int global = 0;

    public void spawnObject()
    {
        Vector3 obj_spawn_position = new Vector3(Random.Range(0f, 10f), Random.Range(0f, 10f), Random.Range(0f, 10f));

        objects.Add(new PAPObject(obj_spawn_position));
        //obj.transform.position.x = Random.Range(0f, 10f);
        //obj.transform.position.y = Random.Range(0f, 10f);
        //obj.transform.position.z = Random.Range(0f, 10f);
        proxy_obj = Instantiate(obj, obj_spawn_position, Quaternion.identity, parent);
    }

    public void spawnObstacles()
    {
        var clutter = new List<PAPObstacle>();
        obj_Collider = obj.GetComponent<Collider>();
        for (int i = 0; i < numberOfObstacles; i++)
        {
            Vector3 position = new Vector3(Random.Range(obj.transform.position.x - 0.5f, obj.transform.position.x + 0.5f),
                                            Random.Range(obj.transform.position.y - 0.5f, obj.transform.position.y + 0.5f),
                                            Random.Range(obj.transform.position.z - 0.5f, obj.transform.position.z + 0.5f));
            obstacle_pocetneKordinate.Add(position);

            proxy = Instantiate(obstacleToBeSpawned, position, Quaternion.identity, parent);
            ProxyList.Add(proxy);
            obstacle_Collider = proxy.GetComponent<Collider>();

            while (true)
            {

                if (obstacle_Collider.bounds.Intersects(obj_Collider.bounds))
                {
                    obstacle_pocetneKordinate.RemoveAt(obstacle_pocetneKordinate.Count - 1);
                    Vector3 backup_position = new Vector3(Random.Range(obj.transform.position.x - 5f, obj.transform.position.x + 5f),
                                                           Random.Range(obj.transform.position.y - 5f, obj.transform.position.y + 5f),
                                                           Random.Range(obj.transform.position.z - 5f, obj.transform.position.z + 5f));
                    obstacle_pocetneKordinate.Add(backup_position);

                    Destroy(proxy);
                    proxy = Instantiate(obstacleToBeSpawned, backup_position, Quaternion.identity, parent);
                    ProxyList.Add(proxy);

                    obstacle_Collider = proxy.GetComponent<Collider>();
                }
                else
                {
                    break;
                }
            }

            clutter.Add(new PAPObstacle(obstacle_pocetneKordinate[i], obstacle_pocetneKordinate[i]));
        }
        objects[objects.Count - 1].setClutter(clutter);
    }

    void Awake()
    {
        spawnObject();
    }


    // Start is called before the first frame update
    void Start()
    {
        spawnObstacles();
    }

    // Update is called once per frame
    void Update()
    {
        if (global == 1)
        {
            var childrenTransforms = parent.GetComponentInChildren<Transform>();
            int skip = 0;
            foreach (Transform t in childrenTransforms)
            {
                if (skip == 0) { skip++; continue; }
                obstacle_trenutneKordinate.Add(t.position);
                //Debug.Log("transform position" + t.position.x); skip++;
            }

            for (int element = 0; element < ProxyList.Count; element++)
            {
                //obstacle_trenutneKordinate.Add(obstacle_pocetneKordinate[obstacle_p ocetneKordinate.Count-numberOfObstacles]);
                //obstacle_trenutneKordinate.Add(ProxyList[element].transform.position);
                //obstacle_trenutneKordinate.Add(GameObject.Find("obstacle(Clone)").transform.position);
                //Debug.Log("transform position" + GameObject.Find("obstacle(Clone)").transform.position.x);
                //Debug.Log("transform position" + ProxyList[element].transform.position.x); ne radi

                Destroy(ProxyList[element]);
            }
            Destroy(proxy_obj);
            obstacle_ukupnaUdaljenostOdPocPozicije(obstacle_pocetneKordinate, obstacle_trenutneKordinate);
            obstacle_kordinatnaUdaljenostOdPocPozicije(obstacle_pocetneKordinate, obstacle_trenutneKordinate);
            //Debug.Log(broj_Dotaknutih_Kocaka);

            var clutter = new List<PAPObstacle>();
            for (int i = 0; i < obstacle_pocetneKordinate.Count; i++)
            {
                clutter.Add(new PAPObstacle(obstacle_pocetneKordinate[i], obstacle_trenutneKordinate[i]));
            }
            objects[objects.Count - 1].setClutter(clutter);
            objects[objects.Count - 1].setPlaced(true);

            spawnObject();
            spawnObstacles();
            global = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (obj)
        {
            objects[objects.Count - 1].DeathTimestamp = DateTime.Now;
            Debug.Log("ISPIS");
            float dist = Vector3.Distance(collision.transform.position, transform.position);
            print("Distance to other: " + dist);
            //Destroy(proxy_obj);
            global = 1;
        }
    }

    /* void OnCollisionExit(Collision collision) {
        if (other)
        {
            float dist = Vector3.Distance(other.position, transform.position);
            print("Ćao");
        }
    } */

    void obstacle_ukupnaUdaljenostOdPocPozicije(List<Vector3> poc_kord, List<Vector3> tren_kord)
    {
        broj_Dotaknutih_Kocaka = 0;
        for (int i = 0; i < poc_kord.Count; i++)
        {
            float dist = Vector3.Distance(poc_kord[i], tren_kord[i]);
            //Debug.Log("OBS UDALJENOST" + dist);
            if (dist != 0) { broj_Dotaknutih_Kocaka++; }
        }
    }
    void obstacle_kordinatnaUdaljenostOdPocPozicije(List<Vector3> poc_kord, List<Vector3> tren_kord)
    {
        broj_Dotaknutih_Kocaka = 0;
        for (int i = 0; i < poc_kord.Count; i++)
        {
            float x = poc_kord[i].x - tren_kord[i].x;
            //Debug.Log("XXX" + x);
            float y = poc_kord[i].y - tren_kord[i].y;
            //Debug.Log("YYY" + y);
            float z = poc_kord[i].z - tren_kord[i].z;
            //Debug.Log("ZZZ" + z);
            if (x != 0 || y != 0 || z != 0) { broj_Dotaknutih_Kocaka++; }
        }
    }

    public void PublishReport() => SessionPublisher.Publish(new Session(objects));
}
