using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGenerator2D3D : MonoBehaviour
{
    //A standard cube mesh
    public Mesh mesh;

    public GameObject debugCube; 

    public Transform cubeParent;
    public GameObject wall;
    //Cube vertex list
    public List<Vector3> points;
    //Cube vertex projection list
    public List<Vector3> pointsOnWall = new List<Vector3>();

    public Transform Real2DBackground;

    IList<Point> pointsHulled;




    // Start is called before the first frame update
    void Start()
    {
 
    }

    private void CreateInteraction2D()
    {
        List<Point> WallPoints = ConvertToPoint(pointsOnWall);
        pointsHulled = Hull.MakeHull(WallPoints);
        ShowHulledPoints(pointsHulled);
        CreateCollider();
    }


    private void OnEnable()
    {
        UpdatePolygonCollider2D();
    }
    public void UpdatePolygonCollider2D()
    {
        points.Clear();
        pointsOnWall.Clear();
        FindObjectVertexPositions();
        RayCastToWall();
        if (pointsOnWall.Count > 3)
            CreateInteraction2D();
        
        else  
            Clear2D();
        
    }

    private void Clear2D()
    {
        Destroy(wall.GetComponent<PolygonCollider2D>());
        Destroy(Real2DBackground.GetComponent<PolygonCollider2D>());
        foreach (Transform Child in Real2DBackground)
        {
            Destroy(Child.gameObject);
        }
    }

    private void OnDisable()
    {
        Clear2D();
    }

    private void FindObjectVertexPositions()
    {
        foreach (Vector3 vertex in mesh.vertices)
        {
            Vector3 worldPt = cubeParent.TransformPoint(vertex);

            if (!points.Contains(worldPt))
            {
                points.Add(worldPt);
                //Instantiate(debugCube, worldPt, Quaternion.identity, cubeParent);
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
                if (hit.collider.transform.parent.parent == transform.parent)
                {
                    //Instantiate(debugCube, hit.point, Quaternion.identity, cubeParent);
                    pointsOnWall.Add(hit.point);
                }
            }
            else { print("Raycast não colidiu com nada"); }
                //print("Raycast não colidiu com nada");
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

        // Create PolygonCollider2D
        /*PolygonCollider2D polygonCollider = wall.AddComponent<PolygonCollider2D>();
        polygonCollider.points = pointsToCollider2D;
        */

        //FOR TEEST ONLY
        if (wall.gameObject.GetComponent<PolygonCollider2D>() == null)
            wall.gameObject.AddComponent<PolygonCollider2D>().points = pointsToCollider2D;

        else
            wall.gameObject.GetComponent<PolygonCollider2D>().points = pointsToCollider2D;


        if (Real2DBackground.gameObject.GetComponent<PolygonCollider2D>() == null)
            Real2DBackground.gameObject.AddComponent<PolygonCollider2D>().points = pointsToCollider2D;
        
        else
            Real2DBackground.gameObject.GetComponent<PolygonCollider2D>().points = pointsToCollider2D;

        //Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = pointsToCollider.ToArray();
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        if(Real2DBackground.childCount > 0)
        {
            foreach(Transform Child in Real2DBackground)
            {
                Destroy(Child.gameObject);
            }
        }


          //Set up game object with mesh;
          GameObject objectMesh = new GameObject();
          objectMesh.transform.parent = Real2DBackground.transform;
          //objectMesh.transform.position = new Vector3(wall.transform.position.x, wall.transform.position.y, wall.transform.position.z);
          objectMesh.transform.position = Real2DBackground.gameObject.GetComponent<PolygonCollider2D>().bounds.center;


          objectMesh.transform.localScale = new Vector3(1, 1, 1);
          objectMesh.gameObject.AddComponent(typeof(MeshRenderer));
          MeshFilter filter = objectMesh.AddComponent(typeof(MeshFilter)) as MeshFilter;
          filter.mesh = msh;

          float difX = objectMesh.transform.position.x - objectMesh.GetComponent<Renderer>().bounds.center.x;
          float difY = objectMesh.transform.position.y - objectMesh.GetComponent<Renderer>().bounds.center.y;
          float difZ = objectMesh.transform.position.z - objectMesh.GetComponent<Renderer>().bounds.center.z;

          objectMesh.transform.position = new Vector3(objectMesh.transform.position.x + difX, objectMesh.transform.position.y + difY, objectMesh.transform.position.z + difZ - 0.8f);


    }

    private void Update()
    {
        /*foreach (Vector3 point in points)
        {
            //Vector3 direction = (point - transform.position).normalized;
   
            //Debug.DrawRay(transform.position, direction * 100f, Color.green, 0.01f);
           
        }*/
    }


}
