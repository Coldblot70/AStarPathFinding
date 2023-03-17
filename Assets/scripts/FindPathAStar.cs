using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathMarker
{
    public MapLocation location;
    public float G;
    public float H;
    public float F;
    public GameObject marker;
    public PathMarker parent;
    public PathMarker(MapLocation location,float g,float h,float f,GameObject marker, PathMarker p)
    {
        this.location = location;
        G = g;
        H = h;
        F = f;
        this.marker = marker;
        parent = p;
    }
    public override bool Equals(object obj)
    {
        if (obj == null || !this.GetType().Equals(obj.GetType()))
            return false;
        else
            return location==((PathMarker)(obj)).location;
    }
    public override int GetHashCode()
    {
        return 0;
    }
}
public class FindPathAStar : MonoBehaviour
{
    public Maze maze;
    public Material closedMaterial;
    public Material openMaterial;

    public List<PathMarker> open = new List<PathMarker>();
    public List<PathMarker> closed = new List<PathMarker>();

    public GameObject start;
    public GameObject end;
    public GameObject pathP;
    PathMarker lasPos;
    bool done = false;

    PathMarker startNode;
    PathMarker endNode;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) BeginSearch();
    }
    void RemoveMarkers()
    {
        GameObject[] markers = GameObject.FindGameObjectsWithTag("marker");
        System.Array.ForEach(markers, x => Destroy(x));
    }
    void BeginSearch()
    {
        done = false;
        RemoveMarkers();
        List<MapLocation> locations = new List<MapLocation>();
        for (int j = 0; j < maze.depth-1; j++)
        {
            for (int x = 0; x < maze.width-1; x++)
            {
                if (maze.map[x, j] != 1)
                    locations.Add(new MapLocation(x, j));
            }
        }
        Shuffle(locations);
        Vector3 startLocation = new Vector3(locations[0].x*maze.scale,0,locations[0].z * maze.scale);
        startNode = new PathMarker(new MapLocation(locations[0].x,locations[0].z),0,0,0,Instantiate(start,startLocation,Quaternion.identity),null);

        Vector3 goalLocation = new Vector3(locations[1].x * maze.scale, 0, locations[1].z * maze.scale);
        endNode = new PathMarker(new MapLocation(locations[1].x, locations[1].z), 0, 0, 0, Instantiate(end, goalLocation, Quaternion.identity), null);
    }
    public void Shuffle(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int ran = Random.Range(0, array.Length - i);
            int temp = array[ran];
            array[ran] = array[i];
            array[i] = temp;
        }
    }
    public void Shuffle<T>(List<T> array)
    {
        for (int i = 0; i < array.Count; i++)
        {
            int ran = Random.Range(0, array.Count - i);
            T temp = array[ran];
            array[ran] = array[i];
            array[i] = temp;
        }
    }
}
