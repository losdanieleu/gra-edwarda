using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    Mesh mesh;
    public float fov;
    Vector3 origin;
    public float startingAngle;
    public float viewRange;
    public Vector3 bar;
    public Vector3 AimDirection;
    // Start is called before the first frame update
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = fov * 1f;
        origin = Vector3.zero;
		//bar = transform.position;
    
	}
	int i = 0;
    public void Update()
    {
		i++;
        int rayCount = 360;
        float angle = startingAngle + viewRange * fovrotate(i);
        float angleIncrese = fov / rayCount;
        float viewDistance = 5f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
			SetOrigin(bar);
            SetAimDirection(AimDirection);
            Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask); ;

            if (raycastHit2D.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else { vertex = raycastHit2D.point; }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;

            }

            vertexIndex++;
            angle -= angleIncrese;


        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        void SetOrigin(Vector3 origin)  {
            this.origin = origin;
        }

        void SetAimDirection(Vector3 aimDirection) {
            startingAngle = GetVectorFromAngleFloat(aimDirection) - fov / 2f;
        }


        static Vector3 GetVectorFromAngle(float angle)
        {

            float angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }
        static float GetVectorFromAngleFloat(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }
		//obracanie fov na okolo
        float fovrotate(float dofov)
        {
        return Mathf.Sin(dofov / 500);
        }
			
    }
}
