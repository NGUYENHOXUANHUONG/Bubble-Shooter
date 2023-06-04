using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.DOMove(new Vector3(1, 2, 0), 1).OnComplete(() => {
        //    //Debug.Log("da den");
        //    transform.DOMove(new Vector3(2, 5, 0), 1).OnComplete(() => {
        //        //Debug.Log("da den");
        //        transform.DOMove(new Vector3(-2, -7, 0), 1).OnComplete(() => {
        //            Debug.Log("da den");

        //        });
        //    });
        //});

        Vector3 vector1 = new Vector3(1, 2, 0);
        Vector3 vector2 = new Vector3(-2, -5, 0);
        List<Vector3> vector3s = new List<Vector3>();
        vector3s.Add(vector1);
        vector3s.Add(vector2);
        transform.DOPath(vector3s.ToArray(), 2).OnComplete(() =>
        {
            Debug.Log("da den"); ;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
