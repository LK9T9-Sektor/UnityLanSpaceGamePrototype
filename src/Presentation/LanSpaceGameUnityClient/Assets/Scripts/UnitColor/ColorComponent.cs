using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.UnitColor
{
    public class ColorComponent : NetworkBehaviour
    {
        [SyncVar] public Color Color;
    }
}
