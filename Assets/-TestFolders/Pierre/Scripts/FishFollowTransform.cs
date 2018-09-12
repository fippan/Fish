using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FishFollowTransform : VRTK_TransformFollow {


    private void Start()
    {
        transformToFollow = FishyManager.Instance.SpawnPos.transform;
        transformToChange = transform.transform;
        gameObjectToFollow = FishyManager.Instance.SpawnPos.gameObject;
        moment = FollowMoment.OnUpdate;
        //FollowPosition();
    }

    public override void Follow()
    {
        followsPosition = true;
        CacheTransforms();
        base.Follow();
    }

    //protected override Quaternion GetRotationToFollow()
    //{
    //    return transformToFollow.rotation;
    //}

    //protected override void SetPositionOnGameObject(Vector3 newPosition)
    //{
    //    newPosition = transformToFollow.position;

    //}

    //protected override void SetRotationOnGameObject(Quaternion newRotation)
    //{
    //    newRotation = transformToFollow.rotation;
    //}

}
