using UnityEngine;
using UnityEngine.EventSystems;

public class CubePlacer : MonoBehaviour
{
    private Grid grid;
    private Transform Cube;
    public Transform house;
    public Material Opaque;
    public Material standaard;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        GetNewCube();
    }

    private void Update()
    {
        MoveCubeByMouse();

        if (Input.GetMouseButtonDown(0))
        {
            PlaceANewCube();
        }        
    }

    private void PlaceANewCube()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //Check if mouse pointer is over menu
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                PlaceCubeNear(hitInfo.point);
                GetNewCube();
                MoveCubeByMouse();
            }
        }
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        Cube.transform.position = finalPosition;
        Cube.GetComponent<Renderer>().material = standaard;
        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;
    }

    private void MoveCubeByMouse()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            MoveCube(hitInfo.point);
        }

    }

    private void MoveCube(Vector3 clickPoint)
    {
        var newPosition = grid.GetNearestPointOnGrid(clickPoint);
        Cube.transform.position = newPosition;
    }

    private void GetNewCube()
    {
        //Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Cube = Instantiate(house, new Vector3(10f, 10f, 10f), Quaternion.identity); 

        /*Renderer rend = Cube.GetComponent<Renderer>();
        rend.material =*/

        Cube.GetComponent<Renderer>().material = Opaque;

    }
}