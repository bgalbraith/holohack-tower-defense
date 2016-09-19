using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using HoloToolkit.Unity;

/// <summary>
/// The SurfaceManager class allows applications to scan the environment for a specified amount of time 
/// and then process the Spatial Mapping Mesh (find planes, remove vertices) after that time has expired.
/// </summary>
public class PlaySpaceManager : Singleton<PlaySpaceManager>
{
    [Tooltip("When checked, the SurfaceObserver will stop running after a specified amount of time.")]
    public bool limitScanningByTime = true;

    [Tooltip("How much time (in seconds) that the SurfaceObserver will run after being started; used when 'Limit Scanning By Time' is checked.")]
    public float scanTime = 8.0f;

    [Tooltip("Material to use when rendering Spatial Mapping meshes while the observer is running.")]
    public Material defaultMaterial;

    [Tooltip("Optional Material to use when rendering Spatial Mapping meshes after the observer has been stopped.")]
    public Material secondaryMaterial;

    [Tooltip("Minimum number of floor planes required in order to exit scanning/processing mode.")]
    public uint minimumFloors = 1;

    [Tooltip("Minimum number of wall planes required in order to exit scanning/processing mode.")]
    public uint minimumWalls = 2;

    public float floorY;

    public List<GameObject> leftWalls;
    public List<GameObject> rightWalls;
    public List<GameObject> frontWalls;
    public List<GameObject> ceilings;
    public List<GameObject> allWalls;

    public GameObject prefab;

    /// <summary>
    /// Indicates if processing of the surface meshes is complete.
    /// </summary>
    private bool meshesProcessed = false;
    public  bool cubesCreated = false;

    private List<GameObject> myPlanes;

    private float meshesDoneTime;

    /// <summary>
    /// GameObject initialization.
    /// </summary>
    private void Start()
    {
        SpatialMappingManager.Instance.DrawVisualMeshes = false;

        // Update surfaceObserver and storedMeshes to use the same material during scanning.
        SpatialMappingManager.Instance.SetSurfaceMaterial(defaultMaterial);

        // Register for the MakePlanesComplete event.
        SurfaceMeshesToPlanes.Instance.MakePlanesComplete += SurfaceMeshesToPlanes_MakePlanesComplete;
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        GameObject myPlane;
        List<GameObject> myWalls;
        
        // Check to see if the spatial mapping data has been processed
        // and if we are limiting how much time the user can spend scanning.
        if (!meshesProcessed && limitScanningByTime)
        {
            // If we have not processed the spatial mapping data
            // and scanning time is limited...

            // Check to see if enough scanning time has passed
            // since starting the observer.
            if (limitScanningByTime && ((Time.time - SpatialMappingManager.Instance.StartTime) < scanTime))
            {
                // If we have a limited scanning time, then we should wait until
                // enough time has passed before processing the mesh.
            }
            else
            {
                // The user should be done scanning their environment,
                // so start processing the spatial mapping data...

                /* TODO: 3.a DEVELOPER CODING EXERCISE 3.a */

                // 3.a: Check if IsObserverRunning() is true on the
                // SpatialMappingManager.Instance.
                if (SpatialMappingManager.Instance.IsObserverRunning())
                {
                    // 3.a: If running, Stop the observer by calling
                    // StopObserver() on the SpatialMappingManager.Instance.
                    SpatialMappingManager.Instance.StopObserver();
                }

                // 3.a: Call CreatePlanes() to generate planes.
                CreatePlanes();

                // 3.a: Set meshesProcessed to true.
                meshesProcessed = true;
                meshesDoneTime = Time.time;


            }
        }
        if(meshesProcessed && !cubesCreated && ((Time.time - meshesDoneTime) > 2f))
        {
            foreach (GameObject p in myPlanes)
            {
                //GameObject myCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameObject myCube = Instantiate(prefab);
                myCube.AddComponent<Wall>();

                //TODO contrain close cubes
                if (p.GetComponent<SurfacePlane>().PlaneType == PlaneTypes.Wall && 
                    (Vector3.Distance(p.GetComponent<SurfacePlane>().Plane.Bounds.Center, Camera.main.gameObject.transform.position) > 2f ))
                {
                    if (Camera.main.gameObject.transform.position.z < p.GetComponent<SurfacePlane>().Plane.Bounds.Center.z+2)
                    {                        
                        myCube.transform.position = p.GetComponent<SurfacePlane>().Plane.Bounds.Center;
                        myCube.GetComponent<Renderer>().material.color = Color.blue;
                        //todo: sort the 3 walls by normals

                        Vector3 myWallNormal = p.GetComponent<SurfacePlane>().Plane.Plane.normal;
                         
                        if((Mathf.Abs(myWallNormal.z) > Mathf.Abs(myWallNormal.x)) && myWallNormal.z < 0) //front
                        {
                            //myCube.GetComponent<Renderer>().material.color = Color.red;
                            frontWalls.Add(myCube);
                        }
                        
                        else if(myWallNormal.x > 0) //left
                        {
                            //myCube.GetComponent<Renderer>().material.color = Color.green;
                            leftWalls.Add(myCube);
                        }
                        else //right
                        {
                            //myCube.GetComponent<Renderer>().material.color = Color.white;
                            rightWalls.Add(myCube);
                        }
                        allWalls.Add(myCube);
                        

                    }
                }
                /*
                else if(p.GetComponent<SurfacePlane>().PlaneType == PlaneTypes.Ceiling)
                {
                    ceilings.Add(myCube);
                    allWalls.Add(myCube);

                    myCube.transform.position = p.GetComponent<SurfacePlane>().Plane.Bounds.Center;
                    //myCube.GetComponent<Renderer>().material.color = Color.yellow;
                    myCube.GetComponent<Renderer>().material.color = Color.blue;

                }
                */
                else if(p.GetComponent<SurfacePlane>().PlaneType == PlaneTypes.Floor)
                {
                    floorY = p.GetComponent<SurfacePlane>().Plane.Bounds.Center.y;
                }
                else
                {
                    Destroy(myCube);
                }
            }

            cubesCreated = true;
        }

        // README
        /*
        if(meshesProcessed)
        {
            if ((Time.time - meshesDoneTime)  >= 1f)
            {
                myPlane = myPlanes[Random.Range(0, myPlanes.Count)];
                //myWalls = myPlanes.FindAll(FindWalls);
                //Destroy(myWalls[Random.Range(0, myWalls.Count)]);
                
            
                if (myPlane.GetComponent<SurfacePlane>().PlaneType == PlaneTypes.Wall)
                {
                    GameObject myCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    myCube.transform.position = myPlane.GetComponent<SurfacePlane>().Plane.Bounds.Center;
                    //Destroy(myPlane);
                }
                meshesDoneTime = Time.time;
            }
        }
        */
    }

    private static bool FindWalls (GameObject go)
    {
        //not typesafe
        return go.GetComponent<SurfacePlane>().PlaneType == PlaneTypes.Wall;
    }

    private static bool FindFloor(GameObject go)
    {
        //not typesafe
        return go.GetComponent<SurfacePlane>().PlaneType == PlaneTypes.Floor;
    }

    private static bool FindCeiling(GameObject go)
    {
        //not typesafe
        return go.GetComponent<SurfacePlane>().PlaneType == PlaneTypes.Ceiling;
    }

    /// <summary>
    /// Handler for the SurfaceMeshesToPlanes MakePlanesComplete event.
    /// </summary>
    /// <param name="source">Source of the event.</param>
    /// <param name="args">Args for the event.</param>
    private void SurfaceMeshesToPlanes_MakePlanesComplete(object source, System.EventArgs args)
    {
        // README
        List<GameObject> floor = new List<GameObject>();
        List<GameObject> ceiling = new List<GameObject>();
        List<GameObject> wall = new List<GameObject>();

        floor = SurfaceMeshesToPlanes.Instance.GetActivePlanes(PlaneTypes.Floor);
        ceiling = SurfaceMeshesToPlanes.Instance.GetActivePlanes(PlaneTypes.Ceiling);
        wall = SurfaceMeshesToPlanes.Instance.GetActivePlanes(PlaneTypes.Wall);

        // Check to see if we have enough horizontal planes (minimumFloors)
        // and vertical planes (minimumWalls), to set holograms on in the world.
        if (floor.Count >= minimumFloors && wall.Count >= minimumWalls)
        {
            // We have enough floors and walls to place our holograms on...

            // 3.a: Let's reduce our triangle count by removing triangles
            // from SpatialMapping meshes that intersect with our active planes.
            // Call RemoveVertices().
            // Pass in all activePlanes found by SurfaceMeshesToPlanes.Instance.
            RemoveVertices(SurfaceMeshesToPlanes.Instance.ActivePlanes);

            // 3.a: We can indicate to the user that scanning is over by
            // changing the material applied to the Spatial Mapping meshes.
            // Call SpatialMappingManager.Instance.SetSurfaceMaterial().
            // Pass in the secondaryMaterial.
            SpatialMappingManager.Instance.SetSurfaceMaterial(secondaryMaterial);

            // 3.a: We are all done processing the mesh, so we can now
            // initialize a collection of Placeable holograms in the world
            // and use horizontal/vertical planes to set their starting positions.
            // Call SpaceCollectionManager.Instance.GenerateItemsInWorld().
            // Pass in the lists of horizontal and vertical planes that we found earlier.
            //SpaceCollectionManager.Instance.GenerateItemsInWorld(horizontal, vertical);
        }
        else
        {
            // We do not have enough floors/walls to place our holograms on...

            // 3.a: Re-enter scanning mode so the user can find more surfaces by 
            // calling StartObserver() on the SpatialMappingManager.Instance.
            SpatialMappingManager.Instance.StartObserver();

            // 3.a: Re-process spatial data after scanning completes by
            // re-setting meshesProcessed to false.
            meshesProcessed = false;
        }
    }

    /// <summary>
    /// Creates planes from the spatial mapping surfaces.
    /// </summary>
    private void CreatePlanes()
    {
        // Generate planes based on the spatial map.
        SurfaceMeshesToPlanes surfaceToPlanes = SurfaceMeshesToPlanes.Instance;
        if (surfaceToPlanes != null && surfaceToPlanes.enabled)
        {
            surfaceToPlanes.MinArea = 2f;
            surfaceToPlanes.MakePlanes();
            myPlanes = surfaceToPlanes.ActivePlanes;
            //myPlanes = surfaceToPlanes.GetActivePlanes(PlaneTypes.Wall);
        }

    }
    

    /// <summary>
    /// Removes triangles from the spatial mapping surfaces.
    /// </summary>
    /// <param name="boundingObjects"></param>
    private void RemoveVertices(IEnumerable<GameObject> boundingObjects)
    {
        RemoveSurfaceVertices removeVerts = RemoveSurfaceVertices.Instance;
        if (removeVerts != null && removeVerts.enabled)
        {
            removeVerts.RemoveSurfaceVerticesWithinBounds(boundingObjects);
        }
    }

    /// <summary>
    /// Called when the GameObject is unloaded.
    /// </summary>
    private void OnDestroy()
    {
        if (SurfaceMeshesToPlanes.Instance != null)
        {
            SurfaceMeshesToPlanes.Instance.MakePlanesComplete -= SurfaceMeshesToPlanes_MakePlanesComplete;
        }
    }
}