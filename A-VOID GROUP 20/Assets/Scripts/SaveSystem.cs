using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private static string savePath = Application.persistentDataPath + "/save.json";

    public static void SavePlayer()
    {
     PlayerData data = new PlayerData();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            data.position = player.transform.position;
            data.health = 100;
        }
        string json =JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved to" + savePath);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadPlayer()
    {
       if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = data.position;
            }
            Debug.Log("Game loaded");
        }
        else
        {
            Debug.LogWarning("No save file found!");
        }
    }

   
}
[System.Serializable]
public class PlayerData
{
    public Vector3 position;
    public int health;  
}
