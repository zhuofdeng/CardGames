using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterTypes {
    BAT,
    BOAR,
    BEAR,
    RAT,
    WOLF,
}

[System.Serializable]
public struct SpawnTypes {
    public CharacterTypes t_type;
    public GameObject t_GameObjectPrefab; 
}


/*
    This is a class for spawning enemies.
*/
public class SpawnManager : MonoBehaviour
{
    public List<SpawnTypes> m_spawnableCharacters = new List<SpawnTypes>();

    private Collider m_collider;

    void Start()
    {
        m_collider = GetComponent<Collider>();

        // Temporary:
        // spawn two spiders
        SpawnCharacterType(CharacterTypes.WOLF, 2);
    }

    GameObject GetSpawnableGameObjectByType(CharacterTypes type) {
        for(int i = 0; i < m_spawnableCharacters.Count; i++) {
            if (m_spawnableCharacters[i].t_type == type) {
                return m_spawnableCharacters[i].t_GameObjectPrefab;
            }
        }

        return null;
    }
    /*
        Randomly spawn game objects inside a rectangle
        GameObject: game object to spawn
        unit: How many do we want
    */
    IEnumerator Spawn(GameObject gameObject, uint count = 1) 
    {
        while(count > 0) {
            float xPos = Random.Range(m_collider.bounds.min.x, m_collider.bounds.max.x);
            float zPos = Random.Range(m_collider.bounds.min.z, m_collider.bounds.max.z);
            GameObject go = Instantiate(gameObject, new Vector3(xPos, this.transform.position.y, zPos), Quaternion.identity);
            go.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
            count -= 1;
            yield return new WaitForSeconds(0.1f);
        }
    }
    void SpawnCharacterType(CharacterTypes character, uint count = 1) 
    {
        GameObject go = GetSpawnableGameObjectByType(character);
        if (go) {
            StartCoroutine(Spawn(go, count));
        }
    }
}
