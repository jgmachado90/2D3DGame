using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGenerator2D3D : MonoBehaviour
{
    //A standard cube mesh
    public Mesh mesh;
    //The red cube
    public Transform cubeParent;
    public GameObject wall;
    //Cube vertex list
    public List<Vector3> points;
    //Cube vertex projection list
    public List<Vector3> pointsOnWall = new List<Vector3>();

    IList<Point> pointsHulled;




    // Start is called before the first frame update
    void Start()
    {
        FindObjectVertexPositions();
        RayCastToWall();
        List<Point> WallPoints = ConvertToPoint(pointsOnWall);
        pointsHulled = Hull.MakeHull(WallPoints);
        ShowHulledPoints(pointsHulled);
        CreateCollider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FindObjectVertexPositions()
    {
        foreach (Vector3 vertex in mesh.vertices)
        {
            Vector3 worldPt = cubeParent.TransformPoint(vertex);

            if (!points.Contains(worldPt))
            {
                points.Add(worldPt);
                // Instantiate(cube, worldPt, Quaternion.identity, cubeParent);
            }
        }
    }

    private void RayCastToWall()
    {
        foreach (Vector3 point in points)
        {
            Vector3 direction = (point - transform.position).normalized;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, 1 << 9))
            {
                //pointsOnWall.Add(wall.transform.InverseTransformPoint(hit.point));
                pointsOnWall.Add(hit.point);
                //Instantiate(cube, hit.point, Quaternion.identity, null);
            }
            else
                print("Raycast não colidiu com nada");
        }
    }

    private List<Point> ConvertToPoint(List<Vector3> pointsOnWall)
    {
        List<Point> newPoints = new List<Point>();

        foreach (Vector3 point in pointsOnWall)
        {
            Point newPoint = new Point(point.x, point.y);
            newPoints.Add(newPoint);
        }

        return newPoints;
    }

    private void ShowHulledPoints(IList<Point> points)
    {
        foreach (Point p in points)
        {
            //Instantiate(cube, new Vector3((float)p.x, (float)p.y, wall.transform.position.z), Quaternion.identity, wall.transform);
        }
    }

    private void CreateCollider()
    {
        List<Vector3> pointsToCollider = new List<Vector3>();
        foreach (Point p in pointsHulled)
        {
            Vector3 newPoint = wall.transform.InverseTransformPoint(new Vector3((float)p.x, (float)p.y, wall.transform.position.z - 0.1f));
            pointsToCollider.Add(newPoint);
            /*Vector3 newPointWithZ = wall.transform.InverseTransformPoint(new Vector3((float)p.x + 0.01f, (float)p.y + 0.01f, wall.transform.position.z + 0.1f));
            pointsToCollider.Add(newPointWithZ);*/
        }


        Triangulator tr = new Triangulator(pointsToCollider.ToArray());
        int[] indices = tr.Triangulate();


        Vector2[] pointsToCollider2D = new Vector2[pointsToCollider.Count];
        int i = 0;
        foreach(Vector3 points in pointsToCollider)
        {
            pointsToCollider2D[i] = new Vector2(points.x, points.y);
            i++;
        }

        // Create the mesh
        /*Mesh mesh = new Mesh();
        mesh.SetVertices(pointsToCollider);
        mesh.triangles = indices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();*/

        // Create the mesh
        PolygonCollider2D polygonCollider = wall.AddComponent<PolygonCollider2D>();

        //polygonCollider.pathCount = pointsToCollider2D.Count;
        //polygonCollider.SetPath(pointsToCollider2D.Count, pointsToCollider2D);

        polygonCollider.points = pointsToCollider2D;


        /*PolygonCollider2D mesh = new PolygonCollider2D();
        mesh.SetVertices(pointsToCollider);
        mesh.triangles = indices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();*/


        //MeshCollider wallMeshCollider = wall.AddComponent<MeshCollider>();
        // wallMeshCollider.sharedMesh = mesh;
        //wallMeshCollider.convex = true;
    }


}
