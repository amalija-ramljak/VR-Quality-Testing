using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRQualityTesting.Scripts.Core;
using VRQualityTesting.Scripts.PickAndPlace;
using VRQualityTesting.Scripts.PickAndPlaceMenu;
using VRQualityTesting.Scripts.Utility;

public class NewScript : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private GameObject cylinderPrefab;
    [SerializeField] private GameObject spherePrefab;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] Transform objectParent;
    private List<GameObject> ProxyList = new List<GameObject>();
    private GameObject proxy_obj;

    private List<Vector3> obstacle_pocetneKordinate = new List<Vector3>();
    private List<Vector3> obstacle_trenutneKordinate = new List<Vector3>();
    int broj_Dotaknutih_Kocaka = 0;

    private List<PAPObject> objects = new List<PAPObject>();

    private float objectMinDistance;
    private float objectMaxDistance;
    private float objectMinHeight;
    private float objectMaxHeight;
    private float objectMaxRotationOffset;
    private float objectMinSize;
    private float objectMaxSize;
    private bool useObjectTypeSquare;
    private bool useObjectTypeCylinder;
    private bool useObjectTypeSphere;
    private float obstacleMinDistance;
    private float obstacleMaxDistance;
    private float obstacleMinSize;
    private float obstacleMaxSize;
    private float obstacleMinCount;
    private float obstacleMaxCount;
    private float goalDistance;
    private float goalRotationOffset;
    private float goalSize;
    private float goalHeight;

    void Awake()
    {
        objectMinDistance = Settings.GetFloat(PickAndPlaceKeys.ObjectMinDistance, defaultValue: 1f);
        objectMaxDistance = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxDistance, defaultValue: 2f);
        objectMinHeight = Settings.GetFloat(PickAndPlaceKeys.ObjectMinHeight, defaultValue: 1f);
        objectMaxHeight = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxHeight, defaultValue: 2f);
        objectMaxRotationOffset = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxRotationOffset, defaultValue: 180f);
        objectMinSize = Settings.GetFloat(PickAndPlaceKeys.ObjectMinSize, defaultValue: 1f);
        objectMaxSize = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxSize, defaultValue: 2f);

        useObjectTypeSquare = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeSquare, defaultValue: true);
        useObjectTypeCylinder = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeCylinder, defaultValue: true);
        useObjectTypeSphere = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeSphere, defaultValue: true);

        obstacleMinDistance = Settings.GetFloat(PickAndPlaceKeys.ObstacleMinDistance, defaultValue: 1f);
        obstacleMaxDistance = Settings.GetFloat(PickAndPlaceKeys.ObstacleMaxDistance, defaultValue: 2f);
        obstacleMinSize = Settings.GetFloat(PickAndPlaceKeys.ObstacleMinSize, defaultValue: 1f);
        obstacleMaxSize = Settings.GetFloat(PickAndPlaceKeys.ObstacleMaxSize, defaultValue: 2f);
        obstacleMinCount = Settings.GetInt(PickAndPlaceKeys.ObstacleMinCount, defaultValue: 0);
        obstacleMaxCount = Settings.GetInt(PickAndPlaceKeys.ObstacleMaxCount, defaultValue: 0);

        goalDistance = Settings.GetFloat(PickAndPlaceKeys.GoalDistance, defaultValue: 1f);
        goalRotationOffset = Settings.GetFloat(PickAndPlaceKeys.GoalRotationOffset, defaultValue: 180f);
        goalSize = Settings.GetFloat(PickAndPlaceKeys.GoalSize, defaultValue: 1f);
        goalHeight = Settings.GetFloat(PickAndPlaceKeys.GoalHeight, defaultValue: 2f);
    }


    void Start()
    {
        spawnNewPlacement();
    }

    public void spawnNewPlacement()
    {
        objectParent.rotation = Quaternion.identity;
        spawnObject();
        spawnObstacles();
        objectParent.Rotate(new Vector3(0f, UnityEngine.Random.Range(-objectMaxRotationOffset, objectMaxRotationOffset), 0f));
    }

    public void spawnObject()
    {
        // distance
        float x = UnityEngine.Random.Range(objectMinDistance, objectMaxDistance);

        // x distance, y height, z should be automatic with rotation
        // TODO: test z/rotation
        Vector3 obj_spawn_position = new Vector3(x, UnityEngine.Random.Range(objectMinHeight, objectMaxHeight), 0f);

        objects.Add(new PAPObject(obj_spawn_position));
        //obj.transform.position.x = UnityEngine.Random.Range(0f, 10f);
        //obj.transform.position.y = UnityEngine.Random.Range(0f, 10f);
        //obj.transform.position.z = UnityEngine.Random.Range(0f, 10f);
        proxy_obj = Instantiate(obj, obj_spawn_position, Quaternion.identity, objectParent);
    }

    public void spawnObstacles()
    {
        var clutter = new List<PAPObstacle>();
        var obj_Collider = obj.GetComponent<Collider>();
        var obstacle_Collider, proxy;

        for (int i = 0; i < UnityEngine.Random.Range(obstacleMinCount, obstacleMaxCount); i++)
        {
            Vector3 position = new Vector3(UnityEngine.Random.Range(obj.transform.position.x - 0.5f, obj.transform.position.x + 0.5f),
                                            UnityEngine.Random.Range(obj.transform.position.y - 0.5f, obj.transform.position.y + 0.5f),
                                            UnityEngine.Random.Range(obj.transform.position.z - 0.5f, obj.transform.position.z + 0.5f));
            obstacle_pocetneKordinate.Add(position);

            proxy = Instantiate(obstaclePrefab, position, Quaternion.identity, objectParent);
            ProxyList.Add(proxy);
            obstacle_Collider = proxy.GetComponent<Collider>();

            while (obstacle_Collider.bounds.Intersects(obj_Collider.bounds))
            {
                obstacle_pocetneKordinate.RemoveAt(obstacle_pocetneKordinate.Count - 1);
                Vector3 backup_position = new Vector3(UnityEngine.Random.Range(obj.transform.position.x - 5f, obj.transform.position.x + 5f),
                                                       UnityEngine.Random.Range(obj.transform.position.y - 5f, obj.transform.position.y + 5f),
                                                       UnityEngine.Random.Range(obj.transform.position.z - 5f, obj.transform.position.z + 5f));
                obstacle_pocetneKordinate.Add(backup_position);

                Destroy(proxy);
                proxy = Instantiate(obstaclePrefab, backup_position, Quaternion.identity, objectParent);
                ProxyList.Add(proxy);

                obstacle_Collider = proxy.GetComponent<Collider>();
            }

            clutter.Add(new PAPObstacle(obstacle_pocetneKordinate[i], obstacle_pocetneKordinate[i]));
        }
        objects[objects.Count - 1].setClutter(clutter);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (obj) // huh?
        {
            objects[objects.Count - 1].DeathTimestamp = DateTime.Now;
            float dist = Vector3.Distance(collision.transform.position, transform.position);
            Debug.Log("Distance to other: " + dist);
            handlePlacement();
        }
    }

    void handlePlacement()
    {
        foreach (Transform t in objectParent.GetComponentsInChildren(typeof(Transform)).Skip(1))
        {
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

        var clutter = new List<PAPObstacle>();
        for (int i = 0; i < obstacle_pocetneKordinate.Count; i++)
        {
            clutter.Add(new PAPObstacle(obstacle_pocetneKordinate[i], obstacle_trenutneKordinate[i]));
        }
        objects[objects.Count - 1].setClutter(clutter);
        objects[objects.Count - 1].setPlaced(true);

        spawnNewPlacement();
    }

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

    public void PublishReport() => SessionPublisher.Publish(new Session(objects), ".txt", ".txt");
}
