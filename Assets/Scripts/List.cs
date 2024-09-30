using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class List : MonoBehaviour
{
[SerializeField]
ListItem itemPrefab;
[SerializeField]
RectTransform itemContainer;
[SerializeField]
FirebaseManager firebaseManager;

void Start() {
  StartCoroutine(LoadListItems());
}

public void CreateNewListItem(string label, string weaponPrimaryKey)
{
        var newItem = Instantiate(itemPrefab);
        // Place it in the container; tell it to not keep its current
        // position or scale, so it will be laid out correctly by the
        // UI system
        newItem.transform.SetParent(itemContainer, worldPositionStays: false);
        // Give it a label
        newItem.Label = label;
        newItem.WeaponPrimaryKey = weaponPrimaryKey;
}

IEnumerator LoadListItems() {
  var dbTask = firebaseManager.db.Child("users").Child(firebaseManager.User.UserId).Child("weapons").GetValueAsync();

  yield return new WaitUntil(predicate: () => dbTask.IsCompleted);

  if (dbTask.Exception != null) {
    Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
  } else if (dbTask.Result.Value == null) {
  } else {
    DataSnapshot snapshot = dbTask.Result;
    foreach (DataSnapshot child in snapshot.Children) {
      string key = child.Key.ToString();
      string weaponName = child.Child("weaponName").Value.ToString();
      int weaponId = int.Parse(child.Child("weaponId").Value.ToString());
      int rarity = int.Parse(child.Child("rarity").Value.ToString());
      float size = float.Parse(child.Child("size").Value.ToString());
      float attackSpeed = float.Parse(child.Child("attackSpeed").Value.ToString());
      float blockChance = float.Parse(child.Child("blockChance").Value.ToString());
      float speed = float.Parse(child.Child("speed").Value.ToString());
      float jump = float.Parse(child.Child("jump").Value.ToString());
      float abilityCDR = float.Parse(child.Child("abilityCDR").Value.ToString());
      string text = weaponName + ": size: " + size + " attack speed: " + attackSpeed + " block chance: " + blockChance + " speed: " + speed + " jump: " + jump;

      CreateNewListItem(text, key);

    }
  }

}

}
