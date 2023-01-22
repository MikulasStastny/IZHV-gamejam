using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBuffer : MonoBehaviour
{
    public Transform playerPosition;
    public CharacterController controller;
    private Vector2[] array;
    private int arraySize;
    private int arrayLength = 100;

    IEnumerator enterTimeLoop(){
        print("Entering time loop...");
        enabled = false;
        int i = 0;
        Vector3 move = new Vector3(0, 0, 0);
        while(!Input.GetKey(KeyCode.P)){
            move.x = array[i].x;
            move.y = array[i].y;
            Vector3 diff = transform.TransformDirection(move - transform.position);
            controller.Move(diff);
            yield return new WaitForSeconds(0.025f);
            if(i == arrayLength -1){
                i = 0;
            }
            else{
                i++;
            }
        }
        enabled = true;
        print("Exitting time loop.");
    }

    void printArray(Vector2[] array){
        print("Array size: " + arraySize);
        for(int i = 0; i < arraySize; i++){
            print("index: " + i + '\n' + "values: " + array[i].x + " " + array[i].y);
            print("values: " + array[i].x + " " + array[i].y);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        array = new Vector2[arrayLength];
        arraySize = 0;
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.T)){
            printArray(array);
        }
        if(Input.GetKeyDown(KeyCode.Z)){
            StartCoroutine(enterTimeLoop());
        }

    }
    void FixedUpdate(){
    
        // Shifts values in array
        for(int i = arraySize; i > 0; i--){
            array[i] = array[i-1];
        }
        // Inserts current player position
        array[0].x = playerPosition.position.x;
        array[0].y = playerPosition.position.y;

        if(arraySize < arrayLength-1){
            arraySize++;
        }
    }
}
