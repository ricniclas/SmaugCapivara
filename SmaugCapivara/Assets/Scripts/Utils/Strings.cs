public static class Strings
{
    #region LAYERS
    public static string layerEnemy = "Enemy_Layer";
    public static string layerAttack = "Attack";
    public static string layerScreenCollider = "ScreenCollider";
    public static string layerScreenChecker = "ScreenChecker";
    public static string layerEnemyProjectile = "EnemyProjectile";
    #endregion

    #region TAGS
    public static string tagEnemy = "Enemy";
    public static string tagAttack = "Attack";
    public static string tagDeathZone = "DeathZone";
    public static string tagStageController = "StageController";
    public static string tagCheckpoint = "Checkpoint";
    public static string tagEnemyDamage = "EnemyDamage";
    public static string tagScreenCollider = "ScreenCollider";
    public static string tagEnemyProjectile = "EnemyProjectile";
    #endregion

    #region ANIMATION PARAMETERS
    public static string animParamPlayerAttack = "Attack";
    public static string animParamPlayerDeath = "Death";
    public static string animParamPlayerRunSpeed = "Run_Speed";
    public static string animParamPlayerInTheAir = "In_The_Air";
    public static string animParamPlayerAirSpeed = "Air_Speed";
    public static string animParamPlayerKnockback = "Knockback";

    #region Transition Screen
    public static string animParamTransitionScreenFadeIn = "Fade_In";
    public static string animParamTransitionScreenFadeOut = "Fade_Out";
    #endregion
    #endregion

    #region SCENE NAMES
    public static string sceneMenu = "Menu";
    public static string sceneTests = "Tests";
    public static string sceneLoadBanks = "LoadBanks";
    #endregion

    #region DATA PATHS
    public static string dataPathPlayerProgressionFolder = "/playerProgression.save";
    #endregion

    #region FMOD Paths
    private static string eventPathSFX = "event:/SFX/";
    private static string eventPathMUS = "event:/MUS/";
    public static string busPathMaster = "bus:/";
    public static string busPathMUS = "bus:/MUS";
    public static string busPathSFX = "bus:/SFX";
    public static string FMODEventPathMUSStage1 = eventPathMUS + "STAGE_1";
    public static string FMODEventPathMUSBoss = eventPathMUS + "BOSS";
    public static string FMODEventPathSFXClick = eventPathSFX + "CLICK";

    #endregion

    #region FMOD Banks

    public static string playerPrefsVolumeMaster = "VolumeMaster";
    public static string playerPrefsVolumeMUS = "VolumeMUS";
    public static string playerPrefsVolumeSFX = "VolumeSFX";
    public static string playerPrefsPostProcessBrightness = "PostProcessBrightness";

    #endregion
}
