using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_generator : MonoBehaviour
{
    // Start is called before the first frame update

    public Object enemy;
    public Object enemy_movement_node;

    void Start()
    {
        Rand_enemy_spawn();
    }

    void Rand_enemy_spawn()
    {
        int j = 0;
        while (j < 3)
        {
            int rand_x = Random.Range(0, 29);
            int rand_y = Random.Range(-15, 0);

            if(rand_x > 5 || rand_y > -7)
            {
                Vector2 Spawn_Pos = new Vector2(rand_x, rand_y);
                Instantiate(enemy, Spawn_Pos, Quaternion.identity);
                j++;
                for(int i = 0; i < 4; i++)
                {
                    int rand_node_x = Random.Range(0, 29);
                    int rand_node_y = Random.Range(-15, 0);
                    Vector2 Node_Pos = new Vector2(rand_node_x, rand_node_y);
                    Instantiate(enemy_movement_node, Node_Pos, Quaternion.identity);
                }
            }
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
