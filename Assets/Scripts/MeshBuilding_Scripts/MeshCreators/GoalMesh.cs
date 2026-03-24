using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Handout {
    public class GoalMesh : MonoBehaviour
    {
        public float width = 3;
        public float height = 1;
        public float depth = 1;

        [Space(20)]
        public float SideBarSize = .6f;
        private float TopBarSize;

        [Space(20)]
        public float delayRebuilding = 1;

        MeshBuilder builder;

        void Start()
        {
            TopBarSize = SideBarSize / 2;
            builder = new MeshBuilder();
            CreateShape ();
            //StartCoroutine(Rebuild());
        }

        private void OnEnable()
        {
            TopBarSize = SideBarSize / 2;
            builder = new MeshBuilder();
            CreateShape ();
            //StartCoroutine(Rebuild());
        }
        /// <summary>
        /// Creates a stairway shape in [builder].
        /// </summary>
        void CreateShape()
        {
            Debug.Log("create the Goal");

            builder.Clear();

            //Debug.Log(GetPoints(Vector3.zero, -2));
            //Debug.Log(GetPoints(new Vector3(0,1,1), -2));
            //Debug.Log(GetPoints(new Vector3(0,2,2), -2));

            Vector3 savedV5 = new(0, 0, 0);
            Vector3 savedV6 = new(0, 0, 0);

            Vector3 offsetX = new(1, 0, 0);
            Vector3 offsetY = new(0, 1, 0);
            Vector3 offsetZ = new(0, 0, 1);

            Vector3[] vertices = {
                //back bottom
                /*v1*/    new(width, 0, 0),
                /*v2*/    new (-width, 0, 0),
                // top back:
                /*v3*/    new(width, height, 0),
                /*v4*/    new(-width, height, 0),
                // top front:
                /*v5*/    new(width, height, depth),
                /*v6*/    new(-width, height, depth),
                // R side
                /*v7*/    new(width, height, 0),
                /*v8*/    new(width, height, depth),
                /*v9*/    new(width, 0, 0),
                /*v10*/   new(width, 0, depth),

                // L side
                /*v11*/   new(-width, height, 0),
                /*v12*/   new(-width, height, depth),
                /*v13*/   new(-width, 0, 0),
                /*v14*/   new(-width, 0, depth),

                // front
                //R
                /*v15*/   new(width, 0, depth),
                /*v16*/   new(width, height, depth),
                /*v17*/   new(width - SideBarSize, 0, depth),
                /*v18*/   new(width - SideBarSize, height, depth),
                //L
                /*v19*/   new(-width, 0, depth),
                /*v20*/   new(-width, height, depth),
                /*v21*/   new(-width + SideBarSize, 0, depth),
                /*v22*/   new(-width + SideBarSize, height, depth),

                //Top Bar
                /*v23*/   new(width - SideBarSize, height - TopBarSize, depth),
                /*v24*/   new(-width + SideBarSize, height - TopBarSize, depth)
                };

            int v1 = new();
            int v2 = new();
            int v3 = new();
            int v4 = new();
            int v5 = new();
            int v6 = new();
            int v7 = new();
            int v8 = new();
            int v9 = new();
            int v10 = new();
            int v11 = new();
            int v12 = new();
            int v13 = new();
            int v14 = new();
            int v15 = new();
            int v16 = new();
            int v17 = new();
            int v18 = new();
            int v19 = new();
            int v20 = new();
            int v21 = new();
            int v22 = new();
            int v23 = new();
            int v24 = new();

            //back bottom
            v1 = builder.AddVertex(vertices[0], new Vector2(.91f, 0));
            v2 = builder.AddVertex(vertices[1], new Vector2(.1f, 0));
            // top back:
            v3 = builder.AddVertex(vertices[2], new Vector2(.91f, 0.5f));
            v4 = builder.AddVertex(vertices[3], new Vector2(.1f, 0.5f));

            // top front:
            v5 = builder.AddVertex(vertices[4], new Vector2(.91f, .9f));
            v6 = builder.AddVertex(vertices[5], new Vector2(.1f, .9f));

            // R side
            v7 = builder.AddVertex(vertices[6], new Vector2(.605f, .505f));
            v8 = builder.AddVertex(vertices[7], new Vector2(.605f, .9f));
            v9 = builder.AddVertex(vertices[8], new Vector2(.1f, .505f));
            v10 = builder.AddVertex(vertices[9], new Vector2(.1f, .9f));
            // L side
            v11 = builder.AddVertex(vertices[10], new Vector2(.605f, .505f));
            v12 = builder.AddVertex(vertices[11], new Vector2(.605f, .9f));
            v13 = builder.AddVertex(vertices[12], new Vector2(.1f, .505f));
            v14 = builder.AddVertex(vertices[13], new Vector2(.1f, .9f));

            // front
            //R
            v15 = builder.AddVertex(vertices[14], new Vector2(1, 1));
            v16 = builder.AddVertex(vertices[15], new Vector2(1, 1));
            v17 = builder.AddVertex(vertices[16], new Vector2(0, 1));
            v18 = builder.AddVertex(vertices[17], new Vector2(0, 1));
            //L
            v19 = builder.AddVertex(vertices[18], new Vector2(0, 0));
            v20 = builder.AddVertex(vertices[19], new Vector2(0, 1));
            v21 = builder.AddVertex(vertices[20], new Vector2(0, 0));
            v22 = builder.AddVertex(vertices[21], new Vector2(0, 1));
            v23 = builder.AddVertex(vertices[22], new Vector2(0, 0));
            v24 = builder.AddVertex(vertices[23], new Vector2(0, 1));

            //back triangles
            //out 
            builder.AddTriangle(v1, v2, v3);
            builder.AddTriangle(v4, v3, v2);
            //in
            builder.AddTriangle(v3, v2, v1);
            builder.AddTriangle(v2, v3, v4);

            //top triangles
            //out
            builder.AddTriangle(v3, v4, v5);
            builder.AddTriangle(v6, v5, v4);
            //in
            builder.AddTriangle(v5, v4, v3);
            builder.AddTriangle(v4, v5, v6);

            //side
            //R
            //out
            builder.AddTriangle(v7, v8, v9);
            builder.AddTriangle(v10, v9, v8);
            //in
            builder.AddTriangle(v9, v8, v7);
            builder.AddTriangle(v8, v9, v10);

            //L
            //out
            builder.AddTriangle(v13, v12, v11);
            builder.AddTriangle(v12, v13, v14);
            //in
            builder.AddTriangle(v11, v12, v13);
            builder.AddTriangle(v14, v13, v12);

            //front
            //builder.AddTriangle(v16, v19, v15);
            //L
            builder.AddTriangle(v16, v17, v15);
            builder.AddTriangle(v18, v17, v16);
            //R
            builder.AddTriangle(v19, v21, v20);
            builder.AddTriangle(v20, v21, v22);
            //builder.AddTriangle(v19, v16, v20);

            //TopBar
            builder.AddTriangle(v22, v23, v18);
            builder.AddTriangle(v24, v23, v22);
            /**/
            GetComponent<MeshFilter>().mesh = builder.CreateMesh(true);
        }

        public IEnumerator Rebuild()
        {
            yield return new WaitForSeconds(delayRebuilding);
            CreateShape();
            StartCoroutine(Rebuild());
        }

        Vector3 GetPoints(Vector3 Offset, float x)
        {
            float CenterX = 0;
            float radius = CenterX - x;
            float angleRad = Offset.z;
            Offset = new Vector3(CenterX - Mathf.Cos(angleRad) * radius, Offset.y, Mathf.Sin(angleRad) * radius);
            return Offset;
        }

    }
}