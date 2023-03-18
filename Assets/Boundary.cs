using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boundary : MonoBehaviour
{
    private Vector2 boundary;
    private float _userWt;
    private float _userHt;

    // Start is called before the first frame update
    void Start()
    {
        boundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _userWt = transform.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        _userHt = transform.GetComponent<BoxCollider2D>().bounds.size.y / 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 CurrentPos = transform.position;

        if ((CurrentPos.x
            - _userWt) < (boundary.x * -1))
            CurrentPos.x = (boundary.x * -1) + _userWt;

        if ((CurrentPos.x + _userWt) > boundary.x)
            CurrentPos.x = boundary.x - _userWt;

        if ((CurrentPos.y + _userHt) > boundary.y)
            CurrentPos.y = boundary.y - _userHt;

        transform.position = CurrentPos;

        if ((CurrentPos.y - _userHt) < (boundary.y * -1))
        {
            UnityEngine.Debug.Log("You have Died. T^T");
            SceneManager.LoadScene(0);
        }
    }

}
