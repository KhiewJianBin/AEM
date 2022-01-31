using UnityEngine;

namespace AEM.Managers.Tilemanager
{
    public class Tile : MonoBehaviour
    {
        public enum TileCode
        {
            InPassable,
            Passable
        }

        public TileCode tilecode = TileCode.InPassable;
    }
}