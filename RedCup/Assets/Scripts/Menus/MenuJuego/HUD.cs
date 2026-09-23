using TMPro;
using UnityEngine;

public class HUD : UIPanel
{
    [Header("Textos")]
    [SerializeField] private TMP_Text enemiesText;
    [SerializeField] private TMP_Text keysText;
    [SerializeField] private TMP_Text timerText;

    #region Events

    private void OnEnable()

    {

        GameEvents.OnPlayerDied += Ocultar;

        GameEvents.OnEnemiesUpdated += UpdateEnemies;

        GameEvents.OnKeysUpdated += UpdateKeys;

        LevelObjectiveManager manager = FindFirstObjectByType<LevelObjectiveManager>();



        if (manager != null)

        {

            UpdateEnemies(manager.EnemiesKilled, manager.EnemiesRequired);

            UpdateKeys(manager.KeysCollected, manager.TotalKeysRequired);

        }

    }



    private void OnDisable()

    {

        GameEvents.OnPlayerDied -= Ocultar;

        GameEvents.OnEnemiesUpdated -= UpdateEnemies;

        GameEvents.OnKeysUpdated -= UpdateKeys;

    }

    #endregion



    #region Unity Lifecycle

    private void Update()

    {

        if (TimeManager.Instance == null || timerText == null)

            return;



        timerText.text = TimeManager.Instance.GetFormattedTime();

    }

    #endregion



    #region Mostrar & Ocultar

    public override void Mostrar()

    {

        gameObject.SetActive(true);

    }



    public override void Ocultar()

    {

        gameObject.SetActive(false);

    }

    #endregion



    #region Updates

    private void UpdateEnemies(int current, int total)

    {

        if (enemiesText != null)

            enemiesText.text = $": {current}/{total}";

    }



    private void UpdateKeys(int current, int total)

    {

        if (enemiesText != null)

            keysText.text = $": {current}/{total}";

    }

    #endregion

}
