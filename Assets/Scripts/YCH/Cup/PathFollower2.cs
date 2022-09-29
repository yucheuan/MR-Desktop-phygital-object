using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PathCreation.Examples2
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower2 : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;
        float distanceTravelledx;
        public float spacing = 3;
        public float dis = 1;


        //[SerializeField] private PathPlacer2 pathPlacer2 = GameObject.FindWithTag("Finish").GetComponent<PathPlacer2>();

        void Start() {

            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;                
            }
        }

        void Update()
        {
            VertexPath path = pathCreator.path;

            if (pathCreator != null)
            {
                //iteration
                for (float t = 0; spacing * t < path.length; t++) 
                {                    
                    distanceTravelled += speed * Time.deltaTime * dis;
                    //objectID
                    for (float i = 0; spacing * i < path.length; i++)
                    {
                        //distance
                        distanceTravelledx = distanceTravelled + speed * (Time.deltaTime + i*dis);
                        GameObject temp = GameObject.Find(i.ToString());

                        temp.transform.position = pathCreator.path.GetPointAtDistance(distanceTravelledx, endOfPathInstruction);
                        temp.transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelledx, endOfPathInstruction);
                    }
                }
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}