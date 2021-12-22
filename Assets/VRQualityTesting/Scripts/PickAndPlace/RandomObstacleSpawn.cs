using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacleSpawn : MonoBehaviour
{

    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject obstacleToBeSpawned;
    [SerializeField] int numberOfObstacles;
    [SerializeField] Transform parent;
    private GameObject proxy;
    Collider obstacle_Collider, obj_Collider;

    // Start is called before the first frame update
    void Start()
    {
        obj_Collider = obj.GetComponent<Collider>();
        for(int i=0; i<numberOfObstacles; i++)
        {
            Vector3 position = new Vector3( Random.Range(obj.transform.position.x-0.5f,obj.transform.position.x+0.5f), 
                                            Random.Range(obj.transform.position.y-0.5f,obj.transform.position.y+0.5f), 
                                            Random.Range(obj.transform.position.z-0.5f,obj.transform.position.z+0.5f));
            
            proxy = Instantiate(obstacleToBeSpawned, position, Quaternion.identity, parent);                                
            obstacle_Collider = proxy.GetComponent<Collider>();

            while(true)
            {   
                
                if (obstacle_Collider.bounds.Intersects(obj_Collider.bounds))
                {
                    
                    Vector3 backup_position = new Vector3( Random.Range(obj.transform.position.x-10f,obj.transform.position.x+10f), 
                                                     Random.Range(obj.transform.position.y-10f,obj.transform.position.y+10f), 
                                                     Random.Range(obj.transform.position.z-10f,obj.transform.position.z+10f));

                    Destroy(proxy);
                    proxy = Instantiate(obstacleToBeSpawned, backup_position, Quaternion.identity, parent);                                

                    obstacle_Collider = proxy.GetComponent<Collider>();                                 
                }
                else
                {
                    break;
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
