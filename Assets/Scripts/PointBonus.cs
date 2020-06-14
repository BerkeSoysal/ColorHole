using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBonus : MonoBehaviour
{
    private GameObject cylinder;
    private GameObject plane;
    private GameLogic gameLogic;

    void Start()
    {
        cylinder = GameObject.Find("Cylinder");
        plane = GameObject.Find("success_way_"+this.gameObject.tag.Substring(this.gameObject.tag.Length-1));
        GameObject gameLogicObj = GameObject.Find("GameLogic");
        gameLogic = gameLogicObj.GetComponent<GameLogic>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (gameLogic.GetStage() > (Int16.Parse(this.gameObject.tag.Substring(this.gameObject.tag.Length - 1))))
        {
            Vector3 distance = this.transform.position - cylinder.transform.position;
            if (Math.Abs(distance.x) < 1 && Math.Abs(distance.z) < 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, cylinder.transform.position, Time.deltaTime * Math.Abs((distance.x - 0.3f) / 0.3f));
            }
            if (Math.Abs(distance.x) < 0.8 && Math.Abs(distance.z) < 0.8)
            {
                Physics.IgnoreCollision(plane.GetComponent<Collider>(), GetComponent<Collider>());
            }
            if (gameObject.transform.position.y < -0.5)
            {
                gameLogic.IncrementScore(50);
                Destroy(this.gameObject);
            }
        }
    }
}
