using UnityEngine;

public class Player : MonoBehaviour
{
    Camera cam;
    Collider planeCollider;
    Ray ray;
    RaycastHit hit;
    private bool onLevelTransition;
    private bool onLevelTransition2;
    private bool nextStage;
    private bool transitionDone;
    private GameLogic gameLogic;

    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        planeCollider = GameObject.Find("Plane_1").GetComponent<Collider>();
        onLevelTransition = false;
        GameObject gameLogicObj = GameObject.Find("GameLogic");
        gameLogic = gameLogicObj.GetComponent<GameLogic>();
     
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !onLevelTransition && !onLevelTransition2 && !gameLogic.GetGameOver() && !gameLogic.GetLevelDone())
        {
            cam = gameLogic.GetEnabledCamera();
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == planeCollider)
                {
                    transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime * 20);
                }
            }
        }
        if(gameLogic.GetIsChangeLevel() && !onLevelTransition2)
        {
            onLevelTransition = true;
        }
        if(transitionDone)
        {
            gameLogic.SetIsChangeLevel(false);
            transitionDone = false;
        }
        //transition on sideway
        if(onLevelTransition)
        {
            Vector3 vector = new Vector3();
            vector.x = 0;
            vector.y = this.transform.position.y;
            vector.z = this.transform.position.z;

            float step = 2 * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, vector, step);

            if (Vector3.Distance(transform.position, vector) < 0.001f)
            {
                // Swap the position of the cylinder.
                this.transform.position = new Vector3(0, this.transform.position.y, this.transform.position.z);
            }
            if(this.transform.position.x == 0 && !nextStage)
            {
                onLevelTransition = false;
                onLevelTransition2 = true;
            
            }
        }
        //transition on forward
        if(onLevelTransition2)
        {
            GameObject nextPlane = GameObject.Find("Plane_" + gameLogic.GetStage());
            Vector3 vector = new Vector3();

            vector.x = this.transform.position.x;
            vector.y = this.transform.position.y;
            vector.z = nextPlane.transform.position.z - 4;


            float step = 5 * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, vector, step);
            if (Vector3.Distance(transform.position, vector) < 0.001f)
            {
                onLevelTransition2 = false;
                nextStage = true;
                transitionDone = true;
            }
        }
        if(nextStage)
        {
            planeCollider = GameObject.Find("Plane_" + gameLogic.GetStage()).GetComponent<Collider>();
            gameLogic.SetEnabledCamera(0);
            nextStage = false;
        }
    }


}
