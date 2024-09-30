using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class ListItem : MonoBehaviour
{
  [SerializeField]
  UnityEngine.UI.Text labelText;
  string primaryKey;
  FirebaseManager firebaseManager;

  void Awake() {
    firebaseManager = GameObject.FindObjectOfType<FirebaseManager>();
  }
  public string Label {
    get {
      return labelText.text;
    } set {
      labelText.text = value;
    }
  }

  public string WeaponPrimaryKey {
    get {
      return primaryKey;
    } set {
      primaryKey = value;
    }
  }

  public void OnListItemClick() {
    StartCoroutine(SaveStats());
  }

  IEnumerator SaveStats() {
    var dbTask = firebaseManager.db.Child("users").Child(firebaseManager.User.UserId).Child("weapons").Child(this.WeaponPrimaryKey).GetValueAsync();

    yield return new WaitUntil(predicate: () => dbTask.IsCompleted);

    if (dbTask.Exception != null) {
      Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
    } else if (dbTask.Result.Value == null) {
    } else {
      DataSnapshot snapshot = dbTask.Result;
      int weaponId = int.Parse(snapshot.Child("weaponId").Value.ToString());
      float size = float.Parse(snapshot.Child("size").Value.ToString());
      float attackSpeed = float.Parse(snapshot.Child("attackSpeed").Value.ToString());
      float blockChance = float.Parse(snapshot.Child("blockChance").Value.ToString());
      float speed = float.Parse(snapshot.Child("speed").Value.ToString());
      float jump = float.Parse(snapshot.Child("jump").Value.ToString());
      float abilityCDR = float.Parse(snapshot.Child("abilityCDR").Value.ToString());
      PlayerPrefs.SetInt("weaponId", weaponId);
      PlayerPrefs.SetFloat("size", size);
      PlayerPrefs.SetFloat("attackSpeed", attackSpeed);
      PlayerPrefs.SetFloat("blockChance", blockChance);
      PlayerPrefs.SetFloat("speed", speed);
      PlayerPrefs.SetFloat("jump", jump);
      PlayerPrefs.SetFloat("abilityCDR", abilityCDR);
  }
}}
