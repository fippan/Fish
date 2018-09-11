using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FishFollowTransform : VRTK_TransformFollow {


    private void Start()
    {
        gameObjectToFollow = gameObject;
        gameObjectToChange = FishyManager.Instance.fish;
        //FollowPosition();
    }

    private void Update()
    {
        transform.position = FishyManager.Instance.SpawnPos.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, FishyManager.Instance.transform.rotation, 90f);
    }










    //protected override Vector3 GetPositionToFollow()
    //{
    //    return transform.position;
    //}

    //protected override Quaternion GetRotationToFollow()
    //{
    //    throw new System.NotImplementedException();
    //}

    //protected override void SetPositionOnGameObject(Vector3 newPosition)
    //{
    //    newPosition = transform.position;

    //}

    //protected override void SetRotationOnGameObject(Quaternion newRotation)
    //{
    //    throw new System.NotImplementedException();
    //}
}
