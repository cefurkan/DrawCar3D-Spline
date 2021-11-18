using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SplineMesh;

public class DrawLine : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    public delegate void NewBodyCreated(DrawnBody NewDrawnBody);
    public static event NewBodyCreated newBodyCreated;

    [SerializeField] Material lineMaterial;
    [SerializeField] Camera cam;

    Spline spline;
    SplineMeshTiling splineMeshTiling;
    SplineSmoother smoother;

    public Vector3 lastCarMeshPartPos, firstCarMeshPartPos;

    public GameObject carMesh;
    public Vector3 oldCar;

    GameObject line;
    LineRenderer lineRenderer;
    DrawnBody drawnCar;
    GameObject carParent;

    SlowMotion slowMotion;

    bool startDrawing;
    int currentIndex;

    [SerializeField] float distanceBetweenMeshParts;

    Vector3 mousePos;

    [SerializeField] List<Vector3> deltaPos = new List<Vector3>();
    [SerializeField] float lowest, biggest, magnitude;

    private void Awake()
    {
        slowMotion = new SlowMotion();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startDrawing = true;

        if (carMesh)
        {
            oldCar = carParent.transform.position;
            Destroy(carParent);

            DrawMeshWithSpline();
            deltaPos.Clear();
        }
        else
        {
            DrawMeshWithSpline();
        }

        line = new GameObject();
        line.name = "Line";

        slowMotion.StartSlowMotionEffect();

        // TODO: fix
        spline.nodes.Clear();
        spline.RefreshCurves();

        drawnCar = carParent.AddComponent<DrawnBody>();

        mousePos = Input.mousePosition;

        lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.startWidth = .15f;
        lineRenderer.material = lineMaterial;
    }

    private void DrawMeshWithSpline()
    {
        GameObject car = new GameObject(name = "Car");
        carParent = new GameObject(name = "carParent");

        car.transform.parent = carParent.transform;

        spline = car.AddComponent<Spline>();
        smoother = car.AddComponent<SplineSmoother>();
        splineMeshTiling = car.AddComponent<SplineMeshTiling>();

        var mesh = Resources.Load<Mesh>("Cylinder");
        splineMeshTiling.mesh = mesh;

        var material = Resources.Load<Material>("m_red");
        splineMeshTiling.material = material;

        splineMeshTiling.rotation = new Vector3(0, 90, 0);
        splineMeshTiling.scale = new Vector3(0.15f, 0.15f, 0.15f);
        splineMeshTiling.generateCollider = true;
        splineMeshTiling.updateInPlayMode = true;
        splineMeshTiling.curveSpace = true;
        splineMeshTiling.mode = MeshBender.FillingMode.StretchToInterval;

        smoother.curvature = .3f;

        carMesh = car.gameObject.transform.GetChild(0).gameObject;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        slowMotion.StopSlowMotionEffect();

        lineRenderer.useWorldSpace = false;


        // for pivot point 
        firstCarMeshPartPos = lineRenderer.GetPosition(currentIndex / 2);

        for (int i = 0; i < currentIndex; i++)
        {
            deltaPos.Add(firstCarMeshPartPos - lineRenderer.GetPosition(i));
            spline.AddNode(new SplineNode(deltaPos[i], deltaPos[i]));
        }

        if (deltaPos.Count > 1)
        {
            if (line)
            {
                firstCarMeshPartPos = deltaPos[0];
                lastCarMeshPartPos = deltaPos[currentIndex - 1];


                Destroy(line);
            }

            foreach (Vector3 meshPos in deltaPos)
            {
                if (meshPos.y > biggest)
                {
                    biggest = meshPos.y;
                }
                if (meshPos.y < lowest)
                {
                    lowest = meshPos.y;
                }
            }

            if (newBodyCreated != null)
            {
                newBodyCreated(drawnCar);
            }

            Rigidbody meshRB = carParent.AddComponent<Rigidbody>();
            meshRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
            meshRB.mass = 400f;

            drawnCar.ActivateBodyParts();

            currentIndex = 0;


            if (oldCar != Vector3.zero)
            {
                magnitude = biggest - lowest;
                carParent.transform.position = new Vector3(oldCar.x, oldCar.y + magnitude, oldCar.z);
            }
            else
            {
                // for start position
                carParent.transform.position = new Vector3(0, 10, 0);
            }
        }
        startDrawing = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        startDrawing = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // startDrawing = true;
    }

    void Update()
    {
        StartDrawing();
    }

    private void StartDrawing()
    {
        if (startDrawing)
        {
            Vector3 distance = mousePos - Input.mousePosition;
            float distanceSqrMagnitude = distance.sqrMagnitude;

            if (distanceSqrMagnitude > distanceBetweenMeshParts)
            {
                lineRenderer.SetPosition(currentIndex, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10)));

                mousePos = Input.mousePosition;

                currentIndex++;

                lineRenderer.positionCount = currentIndex + 1;

                lineRenderer.SetPosition(currentIndex, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10)));
            }
        }
    }

}
