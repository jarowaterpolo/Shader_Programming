using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Handout {
	public class CreateStairs : MonoBehaviour {
		public int numberOfSteps = 10;
		// The dimensions of a single step of the staircase:
		public float width=3;
		public float height=1;
		public float depth=1;
        public bool RotateStairs;
        public float deg;
        private float rad;

		MeshBuilder builder;

		void Start () {
			builder = new MeshBuilder ();
            rad = deg * Mathf.Deg2Rad;
			CreateShape ();
			GetComponent<MeshFilter> ().mesh = builder.CreateMesh (true);
		}

        /// <summary>
        /// Creates a stairway shape in [builder].
        /// </summary>
        void CreateShape()
        {
            builder.Clear();

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
            for (int i = 0; i < numberOfSteps; i++)
            {
                Vector3 offsetX = new(1, 0, 0);
                Vector3 offsetY = new(0, 1, 0);
                Vector3 offsetZ = new(0, 0, 1);

                float v5depthChange = 0;
                float v6depthChange = 0;
                //float widthChange = 0;

                if (RotateStairs)
                {
                    //width = CosFunc(width, rad) - SinFunc(height, rad);
                    //height = SinFunc(width, rad) + CosFunc(height, rad);

                    v5depthChange = ((width * Mathf.Tan(rad)) / 2 * i);
                    v6depthChange = ((-width * Mathf.Tan(rad)) / 2 * i);

                    //widthChange = 1 * i;

                    Debug.Log("DcV5 = " +  v5depthChange);
                    Debug.Log("DcV6 = " + v6depthChange);
                }

                offsetX *= 0;
                //offsetX.x += widthChange;
                offsetY *= height * i;
                offsetZ *= depth * i;

                //i want stairs rotate
                //maybe by making the depth rely on the cross poduct of width and height
                //||a*b|| = ||a|| * ||b|| * sin(a)
                //width = Vector3.up * ||depth|| * sin(angle)
                //4 = 1 * ||depth|| * sin(90)
                //4 = 1 * depth * 1
                //depth = 4
                //4 = 1 * ||depth|| * sin(30)
                //4 = 1 * ||depth|| * .5f
                //.5 depth = 4
                //depth = 8
                //?????????

                //Vector3 offset = new Vector3(0, height * i, depth * i);
                Vector3 offset = offsetX + offsetY + offsetZ;

                int v1;
                // bottom:
                //if (i == 0)
                //{
                //    v1 = builder.AddVertex(offset + new Vector3(width, 0, 0), new Vector2(1, 0));
                //}
                //else
                //{
                //    v1 = builder.AddVertex(offset + new Vector3(width, 0, 0 - v5depthChange), new Vector2(1, 0));
                //}

                v1 = builder.AddVertex(offset + new Vector3(width, 0, 0 - v5depthChange), new Vector2(1, 0));

                int v2;
                //if (i == 0)
                //{
                //    v2 = builder.AddVertex(offset + new Vector3(-width, 0, 0), new Vector2(0, 0));
                //}
                //else
                //{
                //    v2 = builder.AddVertex(offset + new Vector3(-width, 0, 0 - v6depthChange), new Vector2(0, 0));
                //}

                v2 = builder.AddVertex(offset + new Vector3(-width, 0, 0 - v6depthChange), new Vector2(0, 0));

                // top front:
                int v3;
                //if (i == 0)
                //{
                //    v3 = builder.AddVertex(offset + new Vector3(width, height, 0), new Vector2(1, 0.5f));
                //}
                //else
                //{
                //    v3 = builder.AddVertex(offset + new Vector3(width, height, 0 - v5depthChange), new Vector2(1, 0.5f));
                //}

                v3 = builder.AddVertex(offset + new Vector3(width, height, 0 - v5depthChange), new Vector2(1, 0.5f));

                //Vector3 NormalV3 = Vector3.Cross(offset, new(width, height, 0));
                //Debug.Log(NormalV3);
                //Vector2 RotNormalV3 =  new(CosFunc(NormalV3.x, rad) - SinFunc(NormalV3.y, rad), SinFunc(NormalV3.x, rad) + CosFunc(NormalV3.y, rad));
                //Debug.DrawLine(new Vector3(width, height, 0), NormalV3, Color.blue);

                int v4;
                //if (i == 0)
                //{
                //    v4 = builder.AddVertex(offset + new Vector3(-width, height, 0), new Vector2(0, 0.5f));
                //}
                //else
                //{
                //    v4 = builder.AddVertex(offset + new Vector3(-width, height, 0 - v6depthChange), new Vector2(0, 0.5f));
                //}

                v4 = builder.AddVertex(offset + new Vector3(-width, height, 0 - v6depthChange), new Vector2(0, 0.5f));

                //Vector3 NormalV4 = Vector3.Cross(offset, new(-width, height, 0));
                //Debug.Log(NormalV4);
                //Vector2 RotNormalV4 = new(CosFunc(-width, rad) - SinFunc(height, rad), SinFunc(-width, rad) + CosFunc(height, rad));
                //Debug.DrawLine(new Vector3(-width, height, 0), NormalV4, Color.blue);

                // top back:
                int v5 = builder.AddVertex(offset + new Vector3(width, height, depth - v5depthChange - 1), new Vector2(1, 1));
                int v6 = builder.AddVertex(offset + new Vector3(-width, height, depth - v6depthChange + 1), new Vector2(0, 1));
                // R side
                int v7 = builder.AddVertex(offset + new Vector3(width, height, 0 - v5depthChange), new Vector2(0, 1));
                int v8 = builder.AddVertex(offset + new Vector3(width, height, depth - v5depthChange), new Vector2(1, 1));
                int v9 = builder.AddVertex(offset + new Vector3(width, 0, 0 - v5depthChange), new Vector2(0, 0));
                // L side
                int v10 = builder.AddVertex(offset + new Vector3(-width, height, 0 - v6depthChange), new Vector2(0, 1));
                int v11 = builder.AddVertex(offset + new Vector3(-width, height, depth - v6depthChange + 1), new Vector2(1, 1));
                int v12 = builder.AddVertex(offset + new Vector3(-width, 0, 0 - v6depthChange), new Vector2(0, 0));
                // back
                int v13 = builder.AddVertex(offset + new Vector3(width, 0, 0 - v5depthChange), new Vector2(1, 0));
                int v14 = builder.AddVertex(offset + new Vector3(-width, 0, 0 - v6depthChange), new Vector2(0, 0));
                int v15 = builder.AddVertex(offset + new Vector3(width, height, depth - v5depthChange - 1), new Vector2(1, 1));
                int v16 = builder.AddVertex(offset + new Vector3(-width, height, depth - v6depthChange + 1), new Vector2(0, 1));

                //front triangles
                builder.AddTriangle(v1, v2, v3);
                builder.AddTriangle(v4, v3, v2);

                //top triangles
                builder.AddTriangle(v3, v4, v5);

                var posvec3 = new Vector3(width, height, 0);
                var posvec4 = new Vector3(-width, height, 0);
                var posvec5 = new Vector3(width, height, depth - v5depthChange);

                string VsP1 = string.Format("v3 = {0}, v4 = {1}, v5 = {2}", posvec3, posvec4, posvec5);
                string VsP2 = string.Format(" Depth v5 == {0} - {1} = {2}", depth, v5depthChange, depth - v5depthChange);
                Debug.Log(VsP1 + VsP2);

                builder.AddTriangle(v6, v5, v4);

                //side
                //builder.AddTriangle(v7, v8, v9);
                builder.AddTriangle(v12, v11, v10);

                //back
                builder.AddTriangle(v15, v14, v13);
                builder.AddTriangle(v14, v15, v16);

            }
            /**/
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
    }
}