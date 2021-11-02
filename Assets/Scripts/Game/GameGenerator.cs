using Character;
using Road;
using UnityEngine;
using Utilities;

namespace Game
{
    public class GameGenerator : MonoBehaviour
    {
        public static GameGenerator Instance;

        [Header("Character")]
        [SerializeField] private Vector3 characterStartPosition;
        public Vector3 CharacterStartPosition
        {
            get {
                return characterStartPosition;
            }
        }
        [SerializeField] private GameObject CharacterPrefab;
        public GameObject Character { get; private set; }

        [Header("Camera")]
        [SerializeField] private Vector3 cameraPositionRelativeToCharacter;
        public Vector3 CameraPositionRelativeToCharacter
        {
            get {
                return cameraPositionRelativeToCharacter;
            }
        }
        private TargetFollower cameraTargetFollower;

        [Header("Road")]
        [SerializeField] private Vector3 roadStartPosition;
        public Vector3 RoadStartPosition
        {
            get {
                return roadStartPosition;
            }
        }
        [SerializeField] private GameObject RoadPrefab;
        public GameObject Road { get; private set; }

        [Header("Lanes")] 
        [SerializeField] private Vector3 lanesStartPosition;
        public Vector3 LanesStartPosition
        {
            get {
                return lanesStartPosition;
            }
        }
        [SerializeField] private GameObject LanesPrefab;
        public LanesData LanesData { get; private set; }

        void Awake()
        {
            Instance = this;

            Generate();
        }

        private void Generate()
        {
            var startQuaternion = new Quaternion();

            Character = Instantiate(CharacterPrefab, CharacterStartPosition, startQuaternion, transform);
            AddMainCameraScriptToFollowCharacter();

            Road = Instantiate(RoadPrefab, RoadStartPosition, startQuaternion, transform);

            LanesData = Instantiate(LanesPrefab, LanesStartPosition, startQuaternion, transform).GetComponent<LanesData>();
        }

        private void AddMainCameraScriptToFollowCharacter()
        {
            cameraTargetFollower = Camera.main.transform.gameObject.AddComponent<TargetFollower>();
            cameraTargetFollower.Target = Character.transform;
            cameraTargetFollower.PositionRelativeToTarget = CameraPositionRelativeToCharacter;
            cameraTargetFollower.OnTargetPositionChanged();
            Character.GetComponent<CharacterMovement>().PositionChanged += cameraTargetFollower.OnTargetPositionChanged;
        }

        void OnDestroy()
        {
            CharacterPrefab = null;
            Character.GetComponent<CharacterMovement>().PositionChanged -= cameraTargetFollower.OnTargetPositionChanged;
            Destroy(Character);
            cameraTargetFollower = null;

            RoadPrefab = null;
            Destroy(Road);
        }
    }
}
