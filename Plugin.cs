using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UKOneHit
{
    [BepInPlugin("cap.ultrakill.onehit", "UKOneHit", "1.0.0")]
    [BepInProcess("ULTRAKILL.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private NewMovement player = null;

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void Start()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void OnSceneChanged(Scene from, Scene to)
        {
            Debug.Log($"Scene changed to {to.name}");
            player = null;
            if (SceneManager.GetActiveScene().name.StartsWith("Level"))
            {
                player = FindObjectOfType<NewMovement>();
                Debug.Log($"Found player! name:{player.gameObject.name}");
                Invoke("ModifyHP", 0.01f);
            }
        }

        private void Update()
        {
            if (player != null)
            {
                if (player.dead && Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }

        private void ModifyHP()
        {
            if (player != null)
            {
                player.antiHp = 99;
                player.hp = 1;
                Invoke("ModifyHP", 0.01f);
            }
        }
    }
}
