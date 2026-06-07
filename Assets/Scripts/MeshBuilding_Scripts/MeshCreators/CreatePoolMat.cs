using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Handout {
	public class CreatePoolMat : MonoBehaviour {
		public float w=3;
		public float h=1;
		public float d=1;
        public float deg;
        private float rad;

        [Space(20)]
        public float delayStairsRebuilding = 1;
        enum Shapes {NormalCube, CubeWithSideUVChange}
        [SerializeField] private Shapes shape;
        [SerializeField] private float UV_Mult = 1;

		MeshBuilder builder;

        private int VCounter = 0;


		void Start () {
			builder = new MeshBuilder ();
            //CreateShape ();
            if (shape == Shapes.CubeWithSideUVChange)
            {
                UV_Mult = UV_Mult;
            }
            else
            {
                UV_Mult = 1;
            }

            StartCoroutine(Rebuild());
		}

        void CreateShape()
        {
            Debug.Log("create the pool mat");
            rad = deg * Mathf.Deg2Rad;

            builder.Clear();

            Vector3[] FrontVertices = {
                /*v1*/    new(-w, 0, 0),
                /*v2*/    new (w, 0, 0),
                /*v3*/    new(-w, h, 0),
                /*v4*/    new(w, h, 0),
                };

            Vector2[] FrontUVs =
            {
                /*uv1*/  new (0,0),
                /*uv2*/  new (1*UV_Mult,0),
                /*uv3*/  new (0,1*UV_Mult),
                /*uv4*/  new (1*UV_Mult,1*UV_Mult),
            };

            Vector3[] RightVertices = {
                /*v1*/    new(w, 0, 0),
                /*v3*/    new(w, h, 0),
                /*v5*/    new(w, 0, d),
                /*v7*/    new(w, h, d),
                };

            Vector2[] RightUVs =
            {
                /*uv1*/  new (0,0),
                /*uv3*/  new (0,1*UV_Mult),
                /*uv5*/  new (1*UV_Mult,0),
                /*uv7*/  new (1*UV_Mult,1*UV_Mult),
            };

            Vector3[] LeftVertices = {
                /*v2*/    new (-w, 0, 0),
                /*v4*/    new(-w, h, 0),
                /*v6*/    new(-w, 0, d),
                /*v8*/    new(-w, h, d),
                };

            Vector2[] LeftUVs =
            {
                /*uv2*/  new (0,0),
                /*uv4*/  new (0,1*UV_Mult),
                /*uv6*/  new (1*UV_Mult,0),
                /*uv8*/  new (1*UV_Mult,1*UV_Mult),
            };

            Vector3[] BackVertices = {
                /*v5*/    new(-w, 0, d),
                /*v6*/    new(w, 0, d),
                /*v7*/    new(-w, h, d),
                /*v8*/    new(w, h, d),
                };

            Vector2[] BackUVs =
            {
                /*uv5*/  new (0,0),
                /*uv6*/  new (1*UV_Mult,0),
                /*uv7*/  new (0,1*UV_Mult),
                /*uv8*/  new (1*UV_Mult,1*UV_Mult),
            };

            Vector3[] TopVertices = {
                /*v3*/    new(-w, h, 0),
                /*v4*/    new(w, h, 0),
                /*v7*/    new(-w, h, d),
                /*v8*/    new(w, h, d),
                };

            Vector2[] TopUVs =
            {
                /*uv3*/  new (0,0),
                /*uv4*/  new (1,0),
                /*uv7*/  new (0,1),
                /*uv8*/  new (1,1),
            };

            Vector3[] BottomVertices = {
                /*v1*/    new(-w, 0, 0),
                /*v2*/    new (w, 0, 0),
                /*v5*/    new(-w, 0, d),
                /*v6*/    new(w, 0, d),
                };

            Vector2[] BottomUVs =
            {
                /*uv1*/  new (0,0),
                /*uv2*/  new (1,0),
                /*uv5*/  new (0,1),
                /*uv6*/  new (1,1),
            };

            int[] v = new int[24];

            //front
            for (int i = 0; i < 4; i++)
            {
                Debug.Log($"vertex was added to v[{i}] = {FrontVertices[i]}, {FrontUVs[i]}");
                v[i] = builder.AddVertex(FrontVertices[i], FrontUVs[i]);
            }
            VCounter += 4;

            //right
            for (int i = 0; i < 4; i++)
            {
                Debug.Log($"{i + VCounter}");
                Debug.Log($"i + Vcounter = {i + VCounter} and v has {v.Length} spaces so v[{i + VCounter}] == {v[i + VCounter]}");
                v[i + VCounter] = builder.AddVertex(RightVertices[i], RightUVs[i]);
            }
            VCounter += 4;

            //left
            for (int i = 0; i < 4; i++)
            {
                v[i + VCounter] = builder.AddVertex(LeftVertices[i], LeftUVs[i]);
            }
            VCounter += 4;

            //back
            for (int i = 0; i < 4; i++)
            {
                v[i + VCounter] = builder.AddVertex(BackVertices[i], BackUVs[i]);
            }
            VCounter += 4;

            //top
            for (int i = 0; i < 4; i++)
            {
                v[i + VCounter] = builder.AddVertex(TopVertices[i], TopUVs[i]);
            }
            VCounter += 4;

            //bottom
            for (int i = 0; i < 4; i++)
            {
                v[i + VCounter] = builder.AddVertex(BottomVertices[i], BottomUVs[i]);
            }
            VCounter += 4;


            //front
            builder.AddTriangle(v[2], v[1], v[0]);
            builder.AddTriangle(v[1], v[2], v[3]);

            //right
            builder.AddTriangle(v[4], v[5], v[6]);
            builder.AddTriangle(v[7], v[6], v[5]);

            //left
            builder.AddTriangle(v[10], v[9], v[8]);
            builder.AddTriangle(v[9], v[10], v[11]);

            //back
            builder.AddTriangle(v[12], v[13], v[14]);
            builder.AddTriangle(v[15], v[14], v[13]);

            //top
            builder.AddTriangle(v[18], v[17], v[16]);
            builder.AddTriangle(v[17], v[18], v[19]);

            //bottom
            builder.AddTriangle(v[20], v[21], v[22]);
            builder.AddTriangle(v[23], v[22], v[21]);

            GetComponent<MeshFilter>().mesh = builder.CreateMesh(true);
            VCounter = 0;
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