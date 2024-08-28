using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVisible : MonoBehaviour
{
    public float DetectingAngle;

    [SerializeField] private Weapon m_Weapon;

    private List<Enemy> mEnemies;
    private List<SpriteRenderer> mEnemyRenderers;

    // Start is called before the first frame update
    void Start()
    {
        mEnemies = new List<Enemy>();
        mEnemyRenderers = new List<SpriteRenderer>();

        Enemy[] enemyObjects = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemyObjects)
        {
            mEnemies.Add(enemy);
            mEnemyRenderers.Add(enemy.gameObject.GetComponent<SpriteRenderer>());
        }

        print($"Enemies Count: {mEnemies.Count}, {mEnemyRenderers.Count}");
    }

    private void Update()
    {
        for (int i = 0; i < mEnemies.Count; ++i)
        {
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

            bool isVisible = viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
                             viewportPosition.y >= 0 && viewportPosition.y <= 1 &&
                             viewportPosition.z > 0;

            if (isVisible)
            {
                mEnemyRenderers[i].enabled = CanVisible(mEnemies[i]);
            }
        }
    }

    private bool CanVisible(Enemy enemy)
    {
        Vector2 direction = new Vector2(transform.position.x - enemy.transform.position.x, transform.position.y - enemy.transform.position.y);
        float angle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;


        if (Mathf.Abs(m_Weapon.pAngle - angle) < DetectingAngle)
            return true;
        else
            return false;
    }
}
