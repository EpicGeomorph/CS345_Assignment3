using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System;

public class RoundHandler : MonoBehaviour
{
    private float last_spawned = 0;
    public int zombie_spawn_wait = 1;
    public int zombie_round_incr_by = 2;
    public int zombies_to_spawn = 10;
    private List<GameObject> spawners;
    public int zombies_spawned = 0;
    public int zombies_killed = 0;
    public int total_zombies_killed = 0;
    public GameObject zombie;
    private Boolean round_started = false;
    public int round = 1;

    void Start()
    {
        Debug.Log("STARTING GAME.");
        spawners = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawner"));
    }

    void Update()
    {
        if (!round_started)
        {
            Debug.Log("Starting round " + round);
            round_started = true;

            zombies_spawned = 0;
            zombies_killed = 0;
            zombies_to_spawn = 10 + (round*zombie_round_incr_by);
        }

        if (Time.time - last_spawned > zombie_spawn_wait && zombies_spawned < zombies_to_spawn)
        {
            Debug.Log("Spawning Zombie");
            last_spawned = Time.time;

            zombies_spawned++;
            GameObject new_spawn = spawners[new System.Random().Next(spawners.Count)];
            Instantiate(zombie, new_spawn.transform.position, Quaternion.identity);
        }

        if (round_started && zombies_killed >= zombies_to_spawn)
        {
            round++;
            round_started = false;
            Debug.Log("Killed all zombies, starting next round");
        }
    }
}
