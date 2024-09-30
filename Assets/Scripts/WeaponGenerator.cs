using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Firebase;
using Firebase.Database;

public class WeaponGenerator : MonoBehaviour
{
  [SerializeField]
  FirebaseManager firebaseManager;
  int weaponId;
  float rarity;
  float size;
  float attackSpeed;
  float blockChance;
  float abilityCDR;
  float speed;
  float jump;
  string weaponName;
  string[] weaponNames = {"sword", "guitar"};
  string[] opinions = {"superior", "murderous", "annoying", "stabby", "pointy", "sharp", "breathtaking", "bloodboiling", "special", "amazing", "epic", "tennymemes"};
  string[] ofs = {"destruction", "devastation", "destiny", "delirium", "dandelions", "decimation", "detriment", "desparation"};
  string[] thes = {"phantasm", "devil", "creator", "adi", "dabs"};
  string sizeDescriptor;
  string speedDescriptor;
  string rarityDescriptor;
  string opinionDescriptor;
  string ofDescriptor;
  string theDescriptor;
  Random rnd = new Random();

  public void Generate() {

    float n = (float) rnd.Next(0,10);
    if (n < 6) {
      rarity = 0;
    } else if (n > 5 && n < 9) {
      rarity = 1;
    } else {
      rarity = 2;
    }

    weaponId = rnd.Next(0, weaponNames.Length);

    n = (float) rnd.Next(50,151) / 100;
    size = n + rarity / 10;

    n = (float) rnd.Next(75,201) / 100;
    attackSpeed = n + rarity / 10;

    n = (float) rnd.Next(0,16) / 100;
    blockChance = n + rarity / 100;

    n = (float) rnd.Next(0,26) / 100;
    abilityCDR = n + rarity / 100;

    n = (float) rnd.Next(75,176) / 100;
    speed = n + rarity / 10;

    n = (float) rnd.Next(75,121) / 100;
    jump = n + rarity / 10;

    if (size < 1) {
      sizeDescriptor = "small";
    } else if (size > 1.25) {
      sizeDescriptor = "big";
    } else {
      sizeDescriptor = "averagly-endowed";
    }

    if (speed < 1) {
      speedDescriptor = "sluggish";
      } else if (speed > 1.75) {
        speedDescriptor = "lightning quick";
      } else {
        speedDescriptor = "fast";
      }

    if (rarity == 0) {
      rarityDescriptor = "common";
    } else if (rarity == 1) {
      rarityDescriptor = "rare";
    } else if (rarity == 2) {
      rarityDescriptor = "super rare";
    }

    opinionDescriptor = opinions[rnd.Next(0,opinions.Length)];
    ofDescriptor = ofs[rnd.Next(0,ofs.Length)];
    theDescriptor = thes[rnd.Next(0, thes.Length)];

    n = rnd.Next(0,2);
    if (n == 0) {
      weaponName = speedDescriptor + " " + sizeDescriptor + " " + rarityDescriptor + " " + weaponNames[weaponId] + " of " + opinionDescriptor + " " + ofDescriptor;
    } else {
      weaponName = speedDescriptor + " " + sizeDescriptor + " " + rarityDescriptor + " " + weaponNames[weaponId] + " of the " + opinionDescriptor + " " + theDescriptor;
    }
    Debug.Log(weaponName);

    DatabaseReference weaponPath = firebaseManager.db.Child("users").Child(firebaseManager.User.UserId).Child("weapons").Push();
    StartCoroutine(SetWeaponName(weaponPath, weaponName));
    StartCoroutine(SetWeaponId(weaponPath, weaponId));
    StartCoroutine(SetWeaponRarity(weaponPath, rarity));
    StartCoroutine(SetWeaponSize(weaponPath, size));
    StartCoroutine(SetWeaponAttackSpeed(weaponPath, attackSpeed));
    StartCoroutine(SetWeaponBlockChance(weaponPath, blockChance));
    StartCoroutine(SetWeaponAbilityCDR(weaponPath, abilityCDR));
    StartCoroutine(SetWeaponSpeed(weaponPath, speed));
    StartCoroutine(SetWeaponJump(weaponPath, jump));
  }

  IEnumerator SetWeaponName(DatabaseReference path, string weaponName) {
    var dbTask = path.Child("weaponName").SetValueAsync(weaponName);
    yield return new WaitUntil(predicate: () => dbTask.IsCompleted);
    if (dbTask.Exception != null) {
      Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
    } else {
    }
  }

  IEnumerator SetWeaponId(DatabaseReference path, int weaponId) {
    var dbTask = path.Child("weaponId").SetValueAsync(weaponId);
    yield return new WaitUntil(predicate: () => dbTask.IsCompleted);
    if (dbTask.Exception != null) {
      Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
    } else {
    }
  }

  IEnumerator SetWeaponRarity(DatabaseReference path, float rarity) {
    var dbTask = path.Child("rarity").SetValueAsync(rarity);
    yield return new WaitUntil(predicate: () => dbTask.IsCompleted);
    if (dbTask.Exception != null) {
      Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
    } else {
    }
  }

  IEnumerator SetWeaponSize(DatabaseReference path, float size) {
    var dbTask = path.Child("size").SetValueAsync(size);
    yield return new WaitUntil(predicate: () => dbTask.IsCompleted);
    if (dbTask.Exception != null) {
      Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
    } else {
    }
  }

  IEnumerator SetWeaponAttackSpeed(DatabaseReference path, float attackSpeed) {
    var dbTask = path.Child("attackSpeed").SetValueAsync(attackSpeed);
    yield return new WaitUntil(predicate: () => dbTask.IsCompleted);
    if (dbTask.Exception != null) {
      Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
    } else {
    }
  }

  IEnumerator SetWeaponBlockChance(DatabaseReference path, float blockChance) {
    var dbTask = path.Child("blockChance").SetValueAsync(blockChance);
    yield return new WaitUntil(predicate: () => dbTask.IsCompleted);
    if (dbTask.Exception != null) {
      Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
    } else {
    }
  }

  IEnumerator SetWeaponAbilityCDR(DatabaseReference path, float abilityCDR) {
    var dbTask = path.Child("abilityCDR").SetValueAsync(abilityCDR);
    yield return new WaitUntil(predicate: () => dbTask.IsCompleted);
    if (dbTask.Exception != null) {
      Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
    } else {
    }
  }

  IEnumerator SetWeaponSpeed(DatabaseReference path, float speed) {
    var dbTask = path.Child("speed").SetValueAsync(speed);
    yield return new WaitUntil(predicate: () => dbTask.IsCompleted);
    if (dbTask.Exception != null) {
      Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
    } else {
    }
  }

  IEnumerator SetWeaponJump(DatabaseReference path, float jump) {
    var dbTask = path.Child("jump").SetValueAsync(jump);
    yield return new WaitUntil(predicate: () => dbTask.IsCompleted);
    if (dbTask.Exception != null) {
      Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
    } else {
    }
  }


}
