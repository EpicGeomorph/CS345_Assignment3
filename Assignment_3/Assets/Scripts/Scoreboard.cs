using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    private RoundHandler round_handler;
    private TextMeshProUGUI textmp;
    void Start()
    {
        round_handler = GameObject.FindAnyObjectByType<RoundHandler>();
        textmp = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textmp.text = "Round: " + round_handler.round + "\nZombies Killed: " + round_handler.total_zombies_killed + "\nZombies Left: " + (round_handler.zombies_to_spawn-round_handler.zombies_killed);
    }
}
