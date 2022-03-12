using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : GenericSingletonClass<Game>
{
    List<Hero> heroes = new List<Hero>();
    List<Enemy> enemies = new List<Enemy>();

    public void AddHero(Hero newHero) {
        heroes.Add(newHero);
    }

    public void AddEnermy(Enemy newEnemy) {
        enemies.Add(newEnemy);
    }

    public List<Enemy> getNearByEnemies(Hero heroToCheck) {
        List<Enemy> result = new List<Enemy>();
        foreach(Enemy enemy in enemies) {
            float distance = Vector3.SqrMagnitude(heroToCheck.transform.position - enemy.transform.position);
            if (distance <= heroToCheck.AwarenessDistanceSquared) {
                result.Add(enemy);
            }
        }

        return result;
    }
    
    public List<Hero> getNearByHeroes(Enemy enemy) {
        List<Hero> result = new List<Hero>();
        foreach(Hero hero in heroes) {
            float distance = Vector3.SqrMagnitude(enemy.transform.position - hero.transform.position);
            Debug.Log("Hero Distance" + distance);
            if (distance <= enemy.AwarenessDistanceSquared) {
                result.Add(hero);
            }
        }

        return result;
    }
}
