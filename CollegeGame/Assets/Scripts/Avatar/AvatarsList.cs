using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AvatarsList : ScriptableObject
{
    public List<Avatar> Avatars;
    public static Avatar CurrentAvatar;

    public static AvatarsList Get() => Resources.Load("AvatarsList") as AvatarsList;

    public void SetAvatar(int id)
    {
        CurrentAvatar = Avatars[id];
    }

    public Avatar GetAvatar(int id) => Avatars[id];
}