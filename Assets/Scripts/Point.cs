using System;
using UnityEngine;

public class Point : MonoBehaviour
{
    private GameObject cylinder;
    private GameObject plane;
    private GameLogic gameLogic;
    private const double DISTANCE_TO_ATTRACT = 0.7;

    void Start()
    {
        cylinder = GameObject.Find("Cylinder");
        plane = GameObject.Find("Plane_" + this.gameObject.tag.Substring(this.gameObject.tag.Length - 1));
        GameObject gameLogicObj = GameObject.Find("GameLogic");
        gameLogic = gameLogicObj.GetComponent<GameLogic>();
    }

    void Update()
    {
        Vector3 distance = this.transform.position - cylinder.transform.position;
        if (Math.Abs(distance.x) < DISTANCE_TO_ATTRACT && Math.Abs(distance.z) < DISTANCE_TO_ATTRACT)
        {
            transform.position = Vector3.MoveTowards(transform.position, cylinder.transform.position, Time.deltaTime * Math.Abs((distance.x - 0.3f) / 0.3f));
        }


        if (Math.Abs(distance.x) < 0.5 && Math.Abs(distance.z) < 0.5)
        {
            Physics.IgnoreCollision(plane.GetComponent<Collider>(), GetComponent<Collider>());
        }
        else
        {
            Physics.IgnoreCollision(plane.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }

        if (gameObject.transform.position.y < -0.5)
        {
            
            if (gameObject.tag.StartsWith("trap") && !gameLogic.GetIsChangeLevel())
            {
                gameLogic.EndGame();
            }
            gameLogic.IncrementScore(50);
            Destroy(this.gameObject);
            
        }
    }
}
