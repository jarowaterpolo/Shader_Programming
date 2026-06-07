using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Handout {
	public class CreatePoolMatOld : MonoBehaviour {
		public float width=3;
		public float height=1;
		public float depth=1;
        public float deg;
        private float rad;

        [Space(20)]
        public float delayStairsRebuilding = 1;

		MeshBuilder builder;

		void Start () {
			builder = new MeshBuilder ();
			//CreateShape ();
            StartCoroutine(Rebuild());
		}

        void CreateShape()
        {
            Debug.Log("create the pool mat");
            rad = deg * Mathf.Deg2Rad;

            builder.Clear();

            Vector3 offsetX = new(1, 0, 0);
            Vector3 offsetY = new(0, 1, 0);
            Vector3 offsetZ = new(0, 0, 1);

            Vector3[] vertices = {
                /*v1*/    new(width, 0, 0),
                /*v2*/    new (-width, 0, 0),
                /*v3*/    new(width, height, 0),
                /*v4*/    new(-width, height, 0),
                /*v5*/    new(width, 0, depth),
                /*v6*/    new(-width, 0, depth),
                /*v7*/    new(width, height, depth),
                /*v8*/    new(-width, height, depth),

                /*v9*/    new(width, height, depth), /*v7*/
                /*v10*/    new (-width, height, depth), /*v8*/
                /*v11*/    new(width, 0, depth), /*v5*/
                /*v12*/    new(-width, 0, depth), /*v6*/
                /*v13*/    new(width, height, 0), /*v3*/
                /*v14*/    new(-width, height, 0), /*v4*/
                /*v15*/    new(width, 0, 0), /*v1*/
                /*v16*/    new(-width, 0, 0), /*v2*/
                };

            Vector2[] uvs =
            {
                /*uv1*/  new (0,0),
                /*uv2*/  new (1,0),
                /*uv3*/  new (0,1),
                /*uv4*/  new (1,1),
                /*uv5*/  new (1,0),
                /*uv6*/  new (0,0),
                /*uv7*/  new (0,0),
                /*uv8*/  new (1,0),

                /*v9*/    new(0,1),
                /*v10*/    new(1,1),
                /*v11*/    new(0,0),
                /*v12*/    new(1,0),

                /*v13*/    new(1,1),
                /*v14*/    new(0,1),

                /*v15*/    new(0,1),
                /*v16*/    new(1,1),
            };

            int[] v =
            {
                builder.AddVertex(vertices[0], uvs[0]),
                builder.AddVertex(vertices[1], uvs[1]),
                // top front:
                builder.AddVertex(vertices[2], uvs[2]),
                builder.AddVertex(vertices[3], uvs[3]),
                // bottom back:
                builder.AddVertex(vertices[4], uvs[4]),
                builder.AddVertex(vertices[5], uvs[5]),
                // top back
                builder.AddVertex(vertices[6], uvs[6]),
                builder.AddVertex(vertices[7], uvs[7]),
                // top back 
                builder.AddVertex(vertices[8], uvs[8]),
                builder.AddVertex(vertices[9], uvs[9]),
                // bottom back:
                builder.AddVertex(vertices[10], uvs[10]),
                builder.AddVertex(vertices[11], uvs[11]),
                // top front:
                builder.AddVertex(vertices[12], uvs[12]),
                builder.AddVertex(vertices[13], uvs[13]),
                //bottom front
                builder.AddVertex(vertices[14], uvs[14]),
                builder.AddVertex(vertices[15], uvs[15]),
            };
            
            //front triangles
            builder.AddTriangle(v[0], v[1], v[2]);
            builder.AddTriangle(v[3], v[2], v[1]);

            //top triangles
            builder.AddTriangle(v[2], v[3], v[6]);
            builder.AddTriangle(v[7], v[6], v[3]);

            //side
            builder.AddTriangle(v[0], v[2], v[4]);
            builder.AddTriangle(v[4], v[2], v[6]);

            builder.AddTriangle(v[5], v[3], v[1]);
            builder.AddTriangle(v[7], v[3], v[5]);

            //bottom
            builder.AddTriangle(v[15], v[14], v[11]);
            builder.AddTriangle(v[10], v[11], v[14]);

            //back
            builder.AddTriangle(v[8], v[9], v[10]);
            builder.AddTriangle(v[11], v[10], v[9]);

            ////side
            //builder.AddTriangle(v[9], v[13], v[11]);
            //builder.AddTriangle(v[8], v[10], v[12]);


            GetComponent<MeshFilter>().mesh = builder.CreateMesh(true);
        }

        public IEnumerator Rebuild()
        {
            Debug.Log($"{delayStairsRebuilding} second before rebuilding");
            yield return new WaitForSeconds(delayStairsRebuilding);
            GetComponent<MeshFilter>().mesh = builder.CreateMesh(false);
            CreateShape();
            StartCoroutine(Rebuild());
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

       
    }
}