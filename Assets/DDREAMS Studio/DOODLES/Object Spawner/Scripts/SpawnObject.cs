using UnityEngine;

namespace DDREAMS.STUDIO.DOODLES
{
    /// <summary>
    /// This class provides functionality to spawn GameObjects in the scene. It can be used as a base class for other scripts that require object spawning capabilities.
    /// </summary>
    public class SpawnObject : MonoBehaviour
    {
        public static void Spawn(GameObject objectToSpawn)
        {
            if (objectToSpawn == null) return;

            Instantiate(objectToSpawn);
        }

        public void Spawn(GameObject objectToSpawn, Transform parentTransform)
        {
            if (objectToSpawn == null) return;

            Instantiate(objectToSpawn, parentTransform);
        }
    }
}
