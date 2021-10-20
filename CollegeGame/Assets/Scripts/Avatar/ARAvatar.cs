using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARAvatar : MonoBehaviour
{
    private void Awake()
    {
        var avatar = AvatarsList.Get().Avatars[Auth.User.CharacterId];

        var avatarInst = Instantiate(avatar.Model, transform);
        avatarInst.transform.localScale = new Vector3(10, 10, 10);
        avatarInst.transform.Rotate(Vector3.up, 180);
        avatarInst.AddComponent<Animator>().runtimeAnimatorController = avatar.AnimatorController;
        avatarInst.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
    }
}
