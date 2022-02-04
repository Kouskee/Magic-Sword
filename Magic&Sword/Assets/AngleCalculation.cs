using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCalculation : MonoBehaviour
{
    [SerializeField] private Transform _root, _enemy;
    
    
    void Update()
    {
        var vectorToEnemy = _enemy.position  - _root.position;
        Debug.DrawRay(_root.position, vectorToEnemy, Color.yellow);


        var rootVector = _root.transform.right;
        var enemyVector = _enemy.position - _root.position;

        // var Dot = Vector2.Dot(rootVector, enemyVector);
        // float Angle = Mathf.Acos(Dot/(rootVector.magnitude * enemyVector.magnitude)) * Mathf.Rad2Deg;
        float Angle = Vector3.Angle(rootVector, enemyVector);
        
        Debug.Log(Angle);
    }
}
