using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject[] platforms;
    
    public int zOffset = 0;
    public int platformLength = 12;
    public int numberOfPlatforms = 7;
    public Transform playerTransform;
    private List<GameObject> activePlatforms = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            if (i == 0)
            {
                spawnPlatform(i);
            }
            spawnPlatform(Random.Range(0, platforms.Length));
        }
    }

    void Update() 
    {
        if (playerTransform.position.z > zOffset - (numberOfPlatforms * platformLength))
        {
            spawnPlatform(Random.Range(0, platforms.Length));
            deletePlatform();
        }
    }

    public void spawnPlatform(int platformIndex)
    {
        
        GameObject platform = Instantiate(platforms[platformIndex], transform.forward * zOffset, transform.rotation);
        activePlatforms.Add(platform);
        zOffset += platformLength;
    }

    private void deletePlatform()
    {
        Destroy(activePlatforms[0]);
        activePlatforms.RemoveAt(0);
    }
}
