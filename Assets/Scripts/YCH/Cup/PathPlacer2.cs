using PathCreation;
using UnityEngine;
using PathCreation.Examples;

namespace PathCreation.Examples2 {

    [ExecuteInEditMode]
    public class PathPlacer2 : PathSceneTool {

        public GameObject prefab;
        public GameObject prefabGap;
        public GameObject holder;
        public float spacing = 3;
        public int gapIndex = 3;

        const float minSpacing = .1f;

        void Generate () {
            if (pathCreator != null && prefab != null && holder != null) {
                DestroyObjects ();

                VertexPath path = pathCreator.path;

                spacing = Mathf.Max(minSpacing, spacing);
                

                for (int i=0; spacing*i < path.length; i++) {
                    float dst = spacing * i;
                    int x;
                    

                    if (i % gapIndex == 0 & i != 0)
                    {
                        Vector3 point = path.GetPointAtDistance(dst);
                        Quaternion rot = path.GetRotationAtDistance(dst);
                        var clone = Instantiate(prefabGap, point, rot, holder.transform);
                        clone.name = i.ToString("D0");

                        x = i - 2;
                        GameObject.Find(x.ToString()).GetComponent<MeshRenderer>().enabled = false;
                        //DestroyImmediate(GameObject.Find(x.ToString()));
                    }

                    else
                    {
                        Vector3 point = path.GetPointAtDistance(dst);
                        Quaternion rot = path.GetRotationAtDistance(dst);
                        var clone = Instantiate(prefab, point, rot, holder.transform);
                        clone.name = i.ToString("D0");
                    }
                }
            }
            //X();
        }

        /*void X()
        {
            Vector3 temp = GameObject.Find("00").transform.localScale;
            temp.x += 5f;
            GameObject.Find("00").transform.localScale = temp;
        }*/

        void DestroyObjects () {
            int numChildren = holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--) {
                DestroyImmediate (holder.transform.GetChild (i).gameObject, false);
            }
        }

        protected override void PathUpdated () {
            if (pathCreator != null) {
                Generate ();
            }
        }
    }
}