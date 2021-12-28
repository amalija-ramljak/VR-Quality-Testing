using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    int global=0;

    public void spawnObject()
    {
        Vector3 obj_spawn_position = new Vector3(Random.Range(0f, 10f), Random.Range(0f, 10f), Random.Range(0f, 10f));
        //obj.transform.position.x = Random.Range(0f, 10f);
        //obj.transform.position.y = Random.Range(0f, 10f);
        //obj.transform.position.z = Random.Range(0f, 10f);
        proxy_obj = Instantiate(obj, obj_spawn_position, Quaternion.identity, parent);
    }

    public void spawnObstacles()
    {
        obj_Collider = obj.GetComponent<Collider>();
            for(int i=0; i<numberOfObstacles; i++)
            {
                Vector3 position = new Vector3( Random.Range(obj.transform.position.x-0.5f,obj.transform.position.x+0.5f), 
                                                Random.Range(obj.transform.position.y-0.5f,obj.transform.position.y+0.5f), 
                                                Random.Range(obj.transform.position.z-0.5f,obj.transform.position.z+0.5f));
                
                proxy = Instantiate(obstacleToBeSpawned, position, Quaternion.identity, parent);
                ProxyList.Add(proxy);                               
                obstacle_Collider = proxy.GetComponent<Collider>();

                while(true)
                {   
                    
                    if (obstacle_Collider.bounds.Intersects(obj_Collider.bounds))
                    {
                        
                        Vector3 backup_position = new Vector3( Random.Range(obj.transform.position.x-5f,obj.transform.position.x+5f), 
                                                               Random.Range(obj.transform.position.y-5f,obj.transform.position.y+5f), 
                                                               Random.Range(obj.transform.position.z-5f,obj.transform.position.z+5f));

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

            }
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
        if( global==1 )
        {
            for(int element=0; element<ProxyList.Count; element++){
                Destroy(ProxyList[element]);
            }
            Destroy(proxy_obj);
            spawnObject();
            spawnObstacles();
            global = 0;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (obj)
        {
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
}
