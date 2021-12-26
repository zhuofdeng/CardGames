using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CharacterTypes {
    SPIDER,
}
/*
    This is a class for spawning enemies.
*/
public class SpawnManager : MonoBehaviour
{
    public GameObject m_spider;
    private Collider m_collider;

    void Start()
    {
        m_collider = GetComponent<Collider>();

        // Temporary:
        // spawn two spiders
        SpawnCharacterType(CharacterTypes.SPIDER, 2);
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
        switch(character)
        {
            case CharacterTypes.SPIDER:
                StartCoroutine(Spawn(m_spider, count));
            break;
        }
    }

}
