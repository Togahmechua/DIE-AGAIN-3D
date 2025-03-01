using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache 
{
    private static Dictionary<Collider, PlayerController> player = new Dictionary<Collider, PlayerController>();

    public static PlayerController GetPlayerController(Collider collider)
    {
        if (!player.ContainsKey(collider))
        {
            player.Add(collider, collider.GetComponent<PlayerController>());
        }

        return player[collider];
    }

    private static Dictionary<Collider, DieBox> dieBox = new Dictionary<Collider, DieBox>();

    public static DieBox GetDieBox(Collider collider)
    {
        if (!dieBox.ContainsKey(collider))
        {
            dieBox.Add(collider, collider.GetComponent<DieBox>());
        }

        return dieBox[collider];
    }

    private static Dictionary<Collider, Chest> chest = new Dictionary<Collider, Chest>();

    public static Chest GetChest(Collider collider)
    {
        if (!chest.ContainsKey(collider))
        {
            chest.Add(collider, collider.GetComponent<Chest>());
        }

        return chest[collider];
    }
}
