using UnityEngine;

public static class SaveSystem
{
    // Guarda valores del progreso
    public static void SaveGame()
    {
        PlayerPrefs.SetInt("llaves", Data.llave);   // ejemplo
        PlayerPrefs.Save();

        Debug.Log("Juego guardado.");
    }

    // Carga valores del progreso
    public static void LoadGame()
    {
        if (PlayerPrefs.HasKey("llaves"))
            Data.llave = PlayerPrefs.GetInt("llaves");

        Debug.Log("Juego cargado.");
    }

    // Borra TODO para nueva partida
    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Datos borrados para nueva partida.");
    }
}
