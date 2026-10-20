using UnityEngine;

namespace DDREAMS.STUDIO.DOODLES
{
    /// <summary>
    /// This class provides functionality to spawn GameObjects in the scene on every frame update.
    /// It inherits from the SpawnObject class and can be used to continuously spawn objects during gameplay.
    /// </summary>
    [RequireComponent(typeof(GameObject))]
    public class SpawnObjectOnUpdate : SpawnObject
    {
        [SerializeField]
        [Tooltip("Add a GameObject or Prefab that will be spawned.")]
        private GameObject spawnObject;

        public int _spawnedObjects;

        private bool _isSpawnObjectNull;

        private void Awake()
        {
            _isSpawnObjectNull = spawnObject == null;
        }

        private void Update()
        {
            if (_isSpawnObjectNull) return;

            Spawn(spawnObject);

            _spawnedObjects++;
        }

        public int GetSpawnedObjectsCount()
        {
            return _spawnedObjects;
        }
    }
}