using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem MoveParticle;

    private PlayerInputController mPlayerInputController;

    void Start()
    {
        mPlayerInputController = GameManager.Instance.player.GetComponent<PlayerInputController>();
        Debug.Assert(mPlayerInputController != null);
    }

    void Update()
    {
        //MoveParticle
        {
            if(mPlayerInputController.Direction.x == 0)
            {
                MoveParticle.Stop();
            }
            else
            {
                var particleMain = MoveParticle.main;
                const float SPEED = 7.0f;
                particleMain.startSpeed = SPEED * mPlayerInputController.Direction.x;
                if (MoveParticle.isStopped)
                {
                    MoveParticle.Play();
                }
            }

        }
    }
}
