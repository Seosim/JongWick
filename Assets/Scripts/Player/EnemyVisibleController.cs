using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVisible : MonoBehaviour
{
    public float DetectingAngle;
    public float ShowSpeed;

    [SerializeField] private Weapon m_Weapon;

    private List<Enemy> mEnemies;
    private List<SpriteRenderer> mEnemyRenderers;
    private List<SpriteRenderer> mEnemyWeaponRenderers;

    // Start is called before the first frame update
    void Start()
    {
        mEnemies = new List<Enemy>();
        mEnemyRenderers = new List<SpriteRenderer>();
        mEnemyWeaponRenderers = new List<SpriteRenderer>();

        Enemy[] enemyObjects = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemyObjects)
        {
            mEnemies.Add(enemy);
            mEnemyRenderers.Add(enemy.gameObject.GetComponent<SpriteRenderer>());
            mEnemyWeaponRenderers.Add(enemy.transform.GetChild(0).GetComponent<SpriteRenderer>());
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
                float value = CanVisible(mEnemies[i]);
                float lerpValue = Mathf.Lerp(mEnemyRenderers[i].color.a, value, Time.deltaTime * ShowSpeed);

                mEnemyRenderers[i].color = new Color(1.0f, 1.0f, 1.0f, lerpValue);
                mEnemyWeaponRenderers[i].color = new Color(1.0f, 1.0f, 1.0f, lerpValue);
            }
        }
    }

    private float CanVisible(Enemy enemy)
    {
        Vector2 direction = new Vector2(transform.position.x - enemy.transform.position.x, transform.position.y - enemy.transform.position.y);
        float angle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;


        if (Mathf.Abs(m_Weapon.pAngle - angle) < DetectingAngle)
            return 1.0f;
        else
            return 0.0f;
    }
}
