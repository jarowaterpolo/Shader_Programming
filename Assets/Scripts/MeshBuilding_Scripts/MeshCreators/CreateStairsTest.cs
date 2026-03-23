using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Handout {
	public class CreateStairsTest : MonoBehaviour {
		public int numberOfSteps = 10;
		// The dimensions of a single step of the staircase:
		public float width=3;
		public float height=1;
		public float depth=1;
        public bool RotateStairs;
        public float deg;
        private float rad;

        [Space(20)]
        public float delayStairsRebuilding = 1;

		MeshBuilder builder;

		void Start () {
			builder = new MeshBuilder ();
			//CreateShape ();
            StartCoroutine(StairRebuild());
		}

        /// <summary>
        /// Creates a stairway shape in [builder].
        /// </summary>
        void CreateShape()
        {
            Debug.Log("create the stair");
            rad = deg * Mathf.Deg2Rad;

            builder.Clear();

            Debug.Log(GetPoints(Vector3.zero, -2));
            Debug.Log(GetPoints(new Vector3(0,1,1), -2));
            Debug.Log(GetPoints(new Vector3(0,2,2), -2));

            /*
            float v5depthChange = 0;
            float v6depthChange = 0;

            // V1: single step, hard coded:
            if (RotateStairs)
                {
                    //width = CosFunc(width, rad) - SinFunc(height, rad);
                    //height = SinFunc(width, rad) + CosFunc(height, rad);

                    v5depthChange = (width * Mathf.Tan(rad)) / 2;
                    v6depthChange = (-width * Mathf.Tan(rad)) / 2;

                    Debug.Log("DcV5 = " +  v5depthChange);
                    Debug.Log("DcV6 = " + v6depthChange);
                }

            //Vertices

            /*
            // front bottom:
            int v1 = builder.AddVertex (new Vector3 (width, 0, 0), new Vector2 (1, 0));	
			int v2 = builder.AddVertex (new Vector3 (-width, 0, 0), new Vector2 (0, 0));
            // top front:
            int v3 = builder.AddVertex(new Vector3(width, height, 0), new Vector2(1, 0.5f));
                Vector3 NormalV3 = Vector3.Cross(new(0,1,1), new(width, height, 0));
                Debug.Log(NormalV3);
                //-1,2,-2
                Vector2 RotNormalV3 =  new(CosFunc(NormalV3.x, rad) - SinFunc(NormalV3.y, rad), SinFunc(NormalV3.x, rad) + CosFunc(NormalV3.y, rad));
            Debug.Log(RotNormalV3);
            //-2,-1
                Debug.DrawLine(new Vector3(width, height, 0), NormalV3, Color.blue);

                int v4 = builder.AddVertex(new Vector3(-width, height, 0), new Vector2(0, 0.5f));
                Vector3 NormalV4 = Vector3.Cross(new(0,1,1), new(-width, height, 0));
                Debug.Log(NormalV4);
                Vector2 RotNormalV4 = new(CosFunc(-width, rad) - SinFunc(height, rad), SinFunc(-width, rad) + CosFunc(height, rad));
                Debug.DrawLine(new Vector3(-width, height, 0), NormalV4, Color.blue);
			// top back
			int v5 = builder.AddVertex(new Vector3(width, height, depth), new Vector2(1, 1));
            int v6 = builder.AddVertex(new Vector3(-width, height, depth), new Vector2(0, 1));
            // right side
            int v7 = builder.AddVertex(new Vector3(width, height, 0), new Vector2(0, 1));
            int v8 = builder.AddVertex(new Vector3(width, height, depth), new Vector2(1, 1));
            int v9 = builder.AddVertex(new Vector3(width, 0, 0), new Vector2(0, 0));
            //left side
            int v10 = builder.AddVertex(new Vector3(-width, height, 0), new Vector2(0, 1));
            int v11 = builder.AddVertex(new Vector3(-width, height, depth), new Vector2(1, 1));
            int v12 = builder.AddVertex(new Vector3(-width, 0, 0), new Vector2(0, 0));
            // back
            int v13 = builder.AddVertex(new Vector3(width, 0, 0), new Vector2(1, 0));
            int v14 = builder.AddVertex(new Vector3(-width, 0, 0), new Vector2(0, 0));
            int v15 = builder.AddVertex(new Vector3(width, height, depth), new Vector2(1, 1));
            int v16 = builder.AddVertex(new Vector3(-width, height, depth), new Vector2(0, 1));

            */

            /*
            // bottom:
                Vector3 offset = Vector3.zero;
                int v1 = builder.AddVertex(offset + new Vector3(width, 0, 0), new Vector2(1, 0));
                int v2 = builder.AddVertex(offset + new Vector3(-width, 0, 0), new Vector2(0, 0));
                // top front:
                int v3 = builder.AddVertex(offset + new Vector3(width, height, 0), new Vector2(1, 0.5f));
                //Vector3 NormalV3 = Vector3.Cross(offset, new(width, height, 0));
                //Debug.Log(NormalV3);
                //Vector2 RotNormalV3 =  new(CosFunc(NormalV3.x, rad) - SinFunc(NormalV3.y, rad), SinFunc(NormalV3.x, rad) + CosFunc(NormalV3.y, rad));
                //Debug.DrawLine(new Vector3(width, height, 0), NormalV3, Color.blue);

                int v4 = builder.AddVertex(offset + new Vector3(-width, height, 0), new Vector2(0, 0.5f));
                //Vector3 NormalV4 = Vector3.Cross(offset, new(-width, height, 0));
                //Debug.Log(NormalV4);
                //Vector2 RotNormalV4 = new(CosFunc(-width, rad) - SinFunc(height, rad), SinFunc(-width, rad) + CosFunc(height, rad));
                //Debug.DrawLine(new Vector3(-width, height, 0), NormalV4, Color.blue);

                // top back:
                int v5 = builder.AddVertex(offset + new Vector3(width, height, depth - v5depthChange), new Vector2(1, 1));
                int v6 = builder.AddVertex(offset + new Vector3(-width, height, depth - v6depthChange), new Vector2(0, 1));
                // R side
                int v7 = builder.AddVertex(offset + new Vector3(width, height, 0), new Vector2(0, 1));
                int v8 = builder.AddVertex(offset + new Vector3(width, height, depth - v5depthChange), new Vector2(1, 1));
                int v9 = builder.AddVertex(offset + new Vector3(width, 0, 0), new Vector2(0, 0));
                // L side
                int v10 = builder.AddVertex(offset + new Vector3(-width, height, 0), new Vector2(0, 1));
                int v11 = builder.AddVertex(offset + new Vector3(-width, height, depth - v6depthChange), new Vector2(1, 1));
                int v12 = builder.AddVertex(offset + new Vector3(-width, 0, 0), new Vector2(0, 0));
                // back
                int v13 = builder.AddVertex(offset + new Vector3(width, 0, 0), new Vector2(1, 0));
                int v14 = builder.AddVertex(offset + new Vector3(-width, 0, 0), new Vector2(0, 0));
                int v15 = builder.AddVertex(offset + new Vector3(width, height, depth - v5depthChange), new Vector2(1, 1));
                int v16 = builder.AddVertex(offset + new Vector3(-width, height, depth - v6depthChange), new Vector2(0, 1));

            //front triangles
            builder.AddTriangle (v1, v2, v3);
            builder.AddTriangle (v4, v3, v2);

            //top triangles
            builder.AddTriangle (v3, v4, v5);

            var posvec3 = new Vector3(width, height, 0);
            var posvec4 = new Vector3(-width, height, 0);
            var posvec5 = new Vector3(width, height, depth - v5depthChange);

            string VsP1 = string.Format("v3 = {0}, v4 = {1}, v5 = {2}", posvec3, posvec4, posvec5);
            string VsP2 = string.Format(" Depth v5 == {0} - {1} = {2}", depth, v5depthChange, depth - v5depthChange);
            Debug.Log(VsP1 + VsP2);

            builder.AddTriangle (v6, v5, v4);

            //side
            //builder.AddTriangle(v7, v8, v9);
            //builder.AddTriangle(v12, v11, v10);

            //back
            //builder.AddTriangle(v15, v14, v13);
            //builder.AddTriangle(v14, v15, v16);

            /**/
            // V2, with for loop:

            Vector3 savedV5 = new(0, 0, 0);
            Vector3 savedV6 = new(0, 0, 0);

            for (int i = 0; i < numberOfSteps; i++)
            {
                Vector3 offsetX = new(1, 0, 0);
                Vector3 offsetY = new(0, 1, 0);
                Vector3 offsetZ = new(0, 0, 1);

                Vector3[] vertices = {
                /*v1*/    new(width, 0, 0),
                /*v2*/    new (-width, 0, 0),
                /*v3*/    new(width, height, 0),
                /*v4*/    new(-width, height, 0),
                /*v5*/    new(width, height, depth),
                /*v6*/    new(-width, height, depth),
                /*v7*/    new(width, height, 0),
                /*v8*/    new(width, height, depth),
                /*v9*/    new(width, 0, 0),
                /*v10*/   new(-width, height, 0),
                /*v11*/   new(-width, height, depth),
                /*v12*/   new(-width, 0, 0),
                /*v13*/   new(width, 0, 0),
                /*v14*/   new(-width, 0, 0),
                /*v15*/   new(width, height, depth),
                /*v16*/   new(-width, height, depth)
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

                if (RotateStairs)
                {
                    //MyRotateFunc(savedV5, savedV6, vertices, i, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16);
                    NewRotateFunc(vertices, i, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16);
                }
                else
                {
                    Vector3 offset = new Vector3(0, height * i, depth * i);

                    v1 = builder.AddVertex(offset + vertices[0], new Vector2(1, 0));
                    v2 = builder.AddVertex(offset + vertices[1], new Vector2(0, 0));
                    // top front:
                    v3 = builder.AddVertex(offset + vertices[2], new Vector2(1, 0.5f));
                    v4 = builder.AddVertex(offset + vertices[3], new Vector2(0, 0.5f));
                    // top back:
                    v5 = builder.AddVertex(offset + vertices[4], new Vector2(1, 1));
                    v6 = builder.AddVertex(offset + vertices[5], new Vector2(0, 1));
                    // R side
                    v7 = builder.AddVertex(offset + vertices[6], new Vector2(0, 1));
                    v8 = builder.AddVertex(offset + vertices[7], new Vector2(1, 1));
                    v9 = builder.AddVertex(offset + vertices[8], new Vector2(0, 0));
                    // L side
                    v10 = builder.AddVertex(offset + vertices[9], new Vector2(0, 1));
                    v11 = builder.AddVertex(offset + vertices[10], new Vector2(1, 1));
                    v12 = builder.AddVertex(offset + vertices[11], new Vector2(0, 0));
                    // back
                    v13 = builder.AddVertex(offset + vertices[12], new Vector2(1, 0));
                    v14 = builder.AddVertex(offset + vertices[13], new Vector2(0, 0));
                    v15 = builder.AddVertex(offset + vertices[14], new Vector2(1, 1));
                    v16 = builder.AddVertex(offset + vertices[15], new Vector2(0, 1));

                    //front triangles
                    builder.AddTriangle(v1, v2, v3);
                    builder.AddTriangle(v4, v3, v2);

                    //top triangles
                    builder.AddTriangle(v3, v4, v5);
                    builder.AddTriangle(v6, v5, v4);

                    //side
                    builder.AddTriangle(v7, v8, v9);
                    builder.AddTriangle(v12, v11, v10);

                    //back
                    builder.AddTriangle(v15, v14, v13);
                    builder.AddTriangle(v14, v15, v16);
                }
            }
            /**/
            GetComponent<MeshFilter>().mesh = builder.CreateMesh(true);
        }

        public IEnumerator StairRebuild()
        {
            yield return new WaitForSeconds(delayStairsRebuilding);
            CreateShape();
            StartCoroutine(StairRebuild());
        }
        float SinFunc(float XOrY, float a)
        {
            XOrY = XOrY * (Mathf.Sin(a));
            return XOrY;
        }
        float CosFunc(float XOrY, float a)
        {
            XOrY = XOrY * (Mathf.Cos(a));
            return XOrY;
        }

        Vector3 GetPoints(Vector3 Offset, float x)
        {
            float CenterX = 0;
            float radius = CenterX - x;
            float angleRad = Offset.z;
            Offset = new Vector3(CenterX - Mathf.Cos(angleRad) * radius, Offset.y, Mathf.Sin(angleRad) * radius);
            return Offset;
        }

        void NewRotateFunc(Vector3[] vertices, int i, int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15, int v16) 
        {
            Vector3 Offset = new(0, height * i, depth * i);

            v1 = builder.AddVertex(GetPoints(Offset, vertices[0].x) + vertices[0], new Vector2(1, 0));
            v2 = builder.AddVertex(GetPoints(Offset, vertices[1].x) + vertices[1], new Vector2(0, 0));
            // top front:
            v3 = builder.AddVertex(GetPoints(Offset, vertices[2].x) + vertices[2], new Vector2(1, 0.5f));
            v4 = builder.AddVertex(GetPoints(Offset, vertices[3].x) + vertices[3], new Vector2(0, 0.5f));
            // top back:
            v5 = builder.AddVertex(GetPoints(Offset, vertices[4].x) + vertices[4], new Vector2(1, 1));
            v6 = builder.AddVertex(GetPoints(Offset, vertices[5].x) + vertices[5], new Vector2(0, 1));
            // R side
            v7 = builder.AddVertex(GetPoints(Offset, vertices[6].x) + vertices[6], new Vector2(0, 1));
            v8 = builder.AddVertex(GetPoints(Offset, vertices[7].x) + vertices[7], new Vector2(1, 1));
            v9 = builder.AddVertex(GetPoints(Offset, vertices[8].x) + vertices[8], new Vector2(0, 0));
            // L side
            v10 = builder.AddVertex(GetPoints(Offset, vertices[9].x) + vertices[9], new Vector2(0, 1));
            v11 = builder.AddVertex(GetPoints(Offset, vertices[10].x) + vertices[10], new Vector2(1, 1));
            v12 = builder.AddVertex(GetPoints(Offset, vertices[11].x) + vertices[11], new Vector2(0, 0));
            // back
            v13 = builder.AddVertex(GetPoints(Offset, vertices[12].x) + vertices[12], new Vector2(1, 0));
            v14 = builder.AddVertex(GetPoints(Offset, vertices[13].x) + vertices[13], new Vector2(0, 0));
            v15 = builder.AddVertex(GetPoints(Offset, vertices[14].x) + vertices[14], new Vector2(1, 1));
            v16 = builder.AddVertex(GetPoints(Offset, vertices[15].x) + vertices[15], new Vector2(0, 1));

            //front triangles
            builder.AddTriangle(v1, v2, v3);
            builder.AddTriangle(v4, v3, v2);

            //top triangles
            builder.AddTriangle(v3, v4, v5);
            builder.AddTriangle(v6, v5, v4);

            //side
            builder.AddTriangle(v7, v8, v9);
            builder.AddTriangle(v12, v11, v10);

            //back
            builder.AddTriangle(v15, v14, v13);
            builder.AddTriangle(v14, v15, v16);
        }

        void MyRotateFunc(Vector3 savedV5, Vector3 savedV6, Vector3[] vertices, int i, int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15, int v16)
        {
            float x;
            float z;

            Vector3 offsetL = new(savedV6.x, height * i, savedV6.z);
            Vector3 offsetR = new(savedV5.x, height * i, savedV5.z);

            Vector3 Offset = new(0, height * i, depth * i);

            //offsetL = new(CosFunc(offsetL.x, rad) - SinFunc(offsetL.z, rad), offsetL.y, SinFunc(offsetL.x, rad) + CosFunc(offsetL.z, rad));
            //offsetR = new(CosFunc(offsetR.x, rad) - SinFunc(offsetR.z, rad), offsetR.y, SinFunc(offsetR.x, rad) + CosFunc(offsetR.z, rad));


            //V5
            x = vertices[4].x; z = vertices[4].z;

            Debug.Log("Vertex 5 before change  x=" + x + " z=" + z);

            x = CosFunc(vertices[4].x, rad) - SinFunc(vertices[4].z, rad);
            z = SinFunc(vertices[4].x, rad) + CosFunc(vertices[4].z, rad);

            Debug.Log("Vertex 5  x=" + x + " z=" + z);

            vertices[4].x = x;
            vertices[4].z = z;

            //V6
            x = vertices[5].x; z = vertices[5].z;

            Debug.Log("Vertex 6 before change  x=" + x + " z=" + z);

            x = CosFunc(vertices[5].x, rad) - SinFunc(vertices[5].z, rad);
            z = SinFunc(vertices[5].x, rad) + CosFunc(vertices[5].z, rad);

            Debug.Log("Vertex 6  x=" + x + " z=" + z);

            vertices[5].x = x;
            vertices[5].z = z;

            //V8
            x = vertices[7].x; z = vertices[7].z;

            Debug.Log("Vertex 8 before change  x=" + x + " z=" + z);

            x = CosFunc(vertices[7].x, rad) - SinFunc(vertices[7].z, rad);
            z = SinFunc(vertices[7].x, rad) + CosFunc(vertices[7].z, rad);

            Debug.Log("Vertex 8  x=" + x + " z=" + z);

            vertices[7].x = x;
            vertices[7].z = z;

            //V11
            x = vertices[10].x; z = vertices[10].z;

            Debug.Log("Vertex 11 before change  x=" + x + " z=" + z);

            x = CosFunc(vertices[10].x, rad) - SinFunc(vertices[10].z, rad);
            z = SinFunc(vertices[10].x, rad) + CosFunc(vertices[10].z, rad);

            Debug.Log("Vertex 11  x=" + x + " z=" + z);

            vertices[10].x = x;
            vertices[10].z = z;

            //V15
            x = vertices[14].x; z = vertices[14].z;

            Debug.Log("Vertex 15 before change  x=" + x + " z=" + z);

            x = CosFunc(vertices[14].x, rad) - SinFunc(vertices[14].z, rad);
            z = SinFunc(vertices[14].x, rad) + CosFunc(vertices[14].z, rad);

            Debug.Log("Vertex 15  x=" + x + " z=" + z);

            vertices[14].x = x;
            vertices[14].z = z;

            //V16
            x = vertices[15].x; z = vertices[15].z;

            Debug.Log("Vertex 16 before change  x=" + x + " z=" + z);

            x = CosFunc(vertices[15].x, rad) - SinFunc(vertices[15].z, rad);
            z = SinFunc(vertices[15].x, rad) + CosFunc(vertices[15].z, rad);

            Debug.Log("Vertex 16  x=" + x + " z=" + z);

            vertices[15].x = x;
            vertices[15].z = z;

            //vertices[3] -= vertices[2];
            //vertices[4] -= vertices[2];
            //vertices[5] -= vertices[2];

            //Debug.Log("vertices 4 na de - V3" + vertices[3] + "vertices 5 na de - V3" + vertices[4] + "vertices 6 na de - V3" + vertices[5]);

            v1 = builder.AddVertex(offsetR + vertices[0], new Vector2(1, 0));
            v2 = builder.AddVertex(offsetL + vertices[1], new Vector2(0, 0));
            // top front:
            v3 = builder.AddVertex(offsetR + vertices[2], new Vector2(1, 0.5f));
            v4 = builder.AddVertex(offsetL + vertices[3], new Vector2(0, 0.5f));
            // top back:
            v5 = builder.AddVertex(offsetR + vertices[4], new Vector2(1, 1));
            v6 = builder.AddVertex(offsetL + vertices[5], new Vector2(0, 1));
            // R side
            v7 = builder.AddVertex(offsetR + vertices[6], new Vector2(0, 1));
            v8 = builder.AddVertex(offsetR + vertices[7], new Vector2(1, 1));
            v9 = builder.AddVertex(offsetR + vertices[8], new Vector2(0, 0));
            // L side
            v10 = builder.AddVertex(offsetL + vertices[9], new Vector2(0, 1));
            v11 = builder.AddVertex(offsetL + vertices[10], new Vector2(1, 1));
            v12 = builder.AddVertex(offsetL + vertices[11], new Vector2(0, 0));
            // back
            v13 = builder.AddVertex(offsetR + vertices[12], new Vector2(1, 0));
            v14 = builder.AddVertex(offsetL + vertices[13], new Vector2(0, 0));
            v15 = builder.AddVertex(offsetR + vertices[14], new Vector2(1, 1));
            v16 = builder.AddVertex(offsetL + vertices[15], new Vector2(0, 1));

            //front triangles
            builder.AddTriangle(v1, v2, v3);
            builder.AddTriangle(v4, v3, v2);

            //top triangles
            builder.AddTriangle(v3, v4, v5);
            builder.AddTriangle(v6, v5, v4);

            //side
            builder.AddTriangle(v7, v8, v9);
            builder.AddTriangle(v12, v11, v10);

            //back
            builder.AddTriangle(v15, v14, v13);
            builder.AddTriangle(v14, v15, v16);

            savedV5 = vertices[4] - vertices[0] + offsetR;
            savedV6 = vertices[5] - vertices[1] + offsetL;

            Debug.Log("saved 5 = " + savedV5 + " 6 = " + savedV6);
        }
    }
}