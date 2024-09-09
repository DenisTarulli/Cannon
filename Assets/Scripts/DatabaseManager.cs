using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using Models;
using TMPro;
using UnityEditor;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private LastShotInfo shotInfo;
    [SerializeField] private TMP_InputField index;
    [SerializeField] private string databaseURL;

    private ShotInfoToDisplay shotToRead = new();

    private void LogMessage(string title, string message)
    {
#if UNITY_EDITOR
        EditorUtility.DisplayDialog(title, message, "Ok");
#else
		Debug.Log(message);
#endif
    }

    public void SaveData()
    {
        RestClient.Put(databaseURL + "/Shot " + shotInfo.ShotIndex.ToString() + ".json", shotInfo);
        Debug.Log($"Shot {shotInfo.ShotIndex} values have been saved succesfully");
        shotInfo.ShotIndex++;
    }

    public void ReadData()
    {
        RestClient.Get<ShotInfoToDisplay>(databaseURL + "/Shot%20" + index.text + ".json").Then(response =>
        {
            shotToRead = response;
            this.LogMessage($"Shot values", $"Index: {index.text}\n" +
                $"Force: {shotToRead.Force}\n" +
                $"X Angle: {shotToRead.X_Angle}\n" +
                $"Y Angle: {shotToRead.Y_Angle}\n" +
                $"Impact force: {shotToRead.ImpactForce}");
        });
    }

    public void DeleteData()
    {
        RestClient.Delete(databaseURL + ".json", (err, res) =>
        {
            if (err != null)
            {
                this.LogMessage("Error", err.Message);
            }
            else
            {
                this.LogMessage("Success", "Status: " + res.StatusCode.ToString() + "\nData cleared");
                shotInfo.ResetIndex();
            }
        });
    }
}
