using UnityEngine;
using UnityEngine.SceneManagement;

namespace beats
{
    public class ShaderEdit : MonoBehaviour
    {
        [SerializeField] private Material targetMat;
        [SerializeField] private float maxOffset = 0.1f;
        private static readonly int Offset = Shader.PropertyToID("_Offset");
        private AudioBinder _volumeProvider = null;
        private float _value = 0f;


        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            targetMat.SetFloat(Offset, _value);
            SceneManager.sceneLoaded += OnSceneLoaded;
            NewAudioListener();
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            NewAudioListener();
        }

        void NewAudioListener()
        {
            // find an AudioListener
            var audioListener = FindFirstObjectByType<AudioListener>();
            if (audioListener == null)
            {
                Debug.LogWarning("No AudioListener found in the scene");
                return;
            }

            // add AudioBinder component to the AudioListener
            var audioBinder = audioListener.gameObject.AddComponent<AudioBinder>();
            _volumeProvider = audioBinder;
            Debug.Log("AudioBinder added to " + audioListener.gameObject.name);
        }

        // Update is called once per frame
        void Update()
        {
            if (_volumeProvider == null)
            {
                _value = 0.0f;
            }
            else
            {
                _value = _volumeProvider.Value;
            }

            targetMat.SetFloat(Offset, _value * maxOffset);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void PreInit()
        {
            var prefa = Resources.Load<GameObject>("beats");
            Instantiate(prefa);
        }
    }
}