using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBehaviour : MonoBehaviour {


    private float RotateSpeed = 1f;
    private float Radius = 1f;
    public float MaxDistance = 2;
    private float DangerZone;

    private Vector2 _centre;
    private float _angle;
    private GameObject shark;
    private GameObject player;

    // Use this for initialization
    void Start () {
        
        shark = GameObject.FindGameObjectWithTag("Shark");
        player = GameObject.FindGameObjectWithTag("player");

        DangerZone = 3 * Radius;

        _centre = shark.transform.position;
        print(_centre.x);
        print(_centre.y);
    }
	
	// Update is called once per frame
	void Update () {



        _angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        shark.transform.position = _centre + offset;
    }

    public bool SharkOnPath()
    {
        float start_X = _centre.x;
        float start_Y = _centre.y;

        float end_X = shark.transform.position.x;
        float end_Y = shark.transform.position.y;

        float distance = Mathf.Sqrt(Mathf.Pow(end_X - start_X, 2) + Mathf.Pow(end_Y - start_Y, 2));

        return distance == Radius;
    }

    public void GoBackOnPath()
    {
        float start_X = _centre.x;
        float start_Y = _centre.y;

        float first_X = start_X + 1f;
        float first_Y = start_Y + 1f;

        float second_X = start_X - 1f;
        float second_Y = start_Y - 1f;

        float end_X = shark.transform.position.x;
        float end_Y = shark.transform.position.y;

        List<List<float>> position_list = new List<List<float>>();




        position_list.Add(new List<float>(new float[] { first_X, start_Y }));
        position_list.Add(new List<float>(new float[] { start_X, first_Y }));
        position_list.Add(new List<float>(new float[] { second_X, start_Y }));
        position_list.Add(new List<float>(new float[] { start_X, second_Y }));

        float leastDist = 100;
        List<float> goal_pos = new List<float>();

        foreach (List<float> list in position_list)
        {
            if(FindDistance(list[0], list[1], end_X, end_Y) < leastDist)
            {
                leastDist = FindDistance(list[0], list[1], end_X, end_Y);
                goal_pos = list;
            }
        }

    }

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }

    private float FindDistance(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt(Mathf.Pow(x2 - x1, 2) + Mathf.Pow(y2 - y1, 2));

    }

    public bool InDangerZone()
    {
        float start_X = _centre.x;
        float start_Y = _centre.y;

        float end_X = player.transform.position.x;
        float end_Y = player.transform.position.y;

        float distance = FindDistance(start_X, start_Y, end_X, end_Y);
        
        if(distance > DangerZone)
        {
            return false;
        }
        return true;

    }

    public float FindPlayerDistance()
    {
        float start_X = player.transform.position.x;
        float start_Y = player.transform.position.y;

        float end_X = shark.transform.position.x;
        float end_Y = shark.transform.position.y;

        float distance = FindDistance(start_X, start_Y, end_X, end_Y);

        return distance;
    }
}
