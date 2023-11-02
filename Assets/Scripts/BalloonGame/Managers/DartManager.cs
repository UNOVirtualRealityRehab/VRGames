/**
 * The DartManager class handles the spawning and despawning of darts in the scene.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartManager : MonoBehaviour
{
    /* Singleton pattern. Holds a reference to the dart manager object. */
    public static DartManager Instance {get; private set;}
    
    [SerializeField] private GameObject leftDartSpawn;
    [SerializeField] private GameObject rightDartSpawn;
    [SerializeField] private GameObject dartPrefab;

    private GameObject                  leftDart;
    private GameObject                  rightDart;

    private void Awake()
    {
        /* Singleton pattern make sure there is only one dart manager. */
		if (Instance != null) {
			Debug.LogError("There should only be one balloon manager.");
		}
		Instance = this; 

        Debug.Log("Dart manager active.");
    }
    
    private void Start()
    {
        this.SpawnDart(leftDartSpawn);
        this.SpawnDart(rightDartSpawn);
    }

    /**
     * Destroys the dart and automatically spawns another dart in the appropriate location depending 
     * on whether the passed in dart is the left or right dart. 
     */
    public void DestroyDart(GameObject dart)
    {
        /* For debugging purposes. */
        string dartStr = (dart == leftDart ? "left" : "right");
        Debug.Log("Destroyed " + dartStr + " dart.");

        GameObject dartSpawn = this.IsLeftDart(dart) ? leftDartSpawn : rightDartSpawn;
        Destroy(dart);
        this.SpawnDart(dartSpawn);
    }

    /**
     * Spawns a dart at the given dart spawn.
     */
    private void SpawnDart(GameObject dartSpawn)
    {
        if (dartSpawn == leftDartSpawn) {
            Debug.Log("Left dart spawned.");
            this.leftDart = Instantiate(dartPrefab);
            this.leftDart.transform.position = dartSpawn.transform.position;
        } else {
            Debug.Log("Right dart spawned.");
            this.rightDart = Instantiate(dartPrefab);
            this.rightDart.transform.position = dartSpawn.transform.position;
        }
    }

    public bool IsLeftDart(GameObject dart) 
    {
        return (dart == this.leftDart);
    }

    public bool IsRightDart(GameObject dart)
    {
        return (dart == this.rightDart);
    }
}