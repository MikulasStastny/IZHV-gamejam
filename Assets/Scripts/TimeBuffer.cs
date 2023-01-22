using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBuffer : MonoBehaviour
{
    public Transform playerPosition;
    // it is 60 frames per second
    private const int TOTAL_FRAMES = 300;
    public float moveSpeed = 0.25f;

    private Queue<Vector2> _positions;
    private List<Vector2> _traceBack;

    IEnumerator enterTimeLoop(){
        print("Entering time loop...");
        enabled = false;
        GetComponent<PlayerControl>().enabled = false;
        
        int i = 0;
        Vector3 move = new Vector3(0, 0, 0);

        while(!Input.GetKey(KeyCode.Space))
        {
            move.x = _traceBack[i].x;
            move.y = _traceBack[i].y;

            transform.position = move;
            if (i == _traceBack.Count - 1)
            {
                // restart the loop
                i = 0;
            }
            else
            {
                i++;
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        enabled = true;
        GetComponent<PlayerControl>().enabled = true;
        print("Exitting time loop.");
    }

    void fillInTraceBack()
    {
        _traceBack.Clear();
        while (_positions.Count > 0)
        {
            _traceBack.Add(_positions.Dequeue());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _positions = new Queue<Vector2>();
        _traceBack = new List<Vector2>();
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Z)){
            fillInTraceBack();
            StartCoroutine(enterTimeLoop());
        }

        if (_positions.Count > TOTAL_FRAMES)
        {
            // We have filled our queue, drop the oldest record
            _positions.Dequeue();
        }
        _positions.Enqueue(new Vector2(
            playerPosition.position.x,
            playerPosition.position.y
        ));
    }
}
