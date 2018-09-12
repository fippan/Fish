using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FishFollowTransform : VRTK_ObjectFollow {

    protected override Vector3 GetPositionToFollow()
    {
        return FishyManager.Instance.SpawnPos.position;
    }

    protected override Quaternion GetRotationToFollow()
    {
        throw new System.NotImplementedException();
    }

    protected override void SetPositionOnGameObject(Vector3 newPosition)
    {
        newPosition = FishyManager.Instance.SpawnPos.position;

    }

    protected override void SetRotationOnGameObject(Quaternion newRotation)
    {
        throw new System.NotImplementedException();
    }
}
