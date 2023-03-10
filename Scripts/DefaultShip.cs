using System.Collections.Generic;
using UnityEngine;
using Enum;

public class DefaultShip : MonoBehaviour
{
    protected int   maxLife { get; set; }
    protected int   currentLife { get; set; }
    protected float speed { get; set; }
    protected float curveRadius { get; set; }
    protected bool  stunned { get; set; }
    protected State state { get; set; }
    protected GameObject lifeBar { get; set; }
    protected List<GameObject> cannons {get; set;}
    protected float angleEffect;
    protected AudioSource sound;

    protected void Navigate()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    protected void Rudder(float _curveRadius)
    {
        transform.Rotate(0, 0, _curveRadius * Time.deltaTime);
    }

    protected void UpdateStatus()
    {
        float st = (float) currentLife / maxLife;

        if (st > 0.6f) state = State.fullLife;
        else if (st > 0.3f) state = State.halfLife;
        else if (st > 0) state = State.lowLife;
        else state = State.emptyLife;

        if (currentLife <= 0) stunned = true;

        try
        {
            GetComponent<Animator>().SetInteger("State", (int) state);
        }
        catch
        {}
    }

    protected void Damage(int _damage)
    {
        currentLife -= _damage;
        UpdateStatus();
        lifeBar.GetComponent<LifeBar>().UpdateScale((float)currentLife / maxLife);
        Explode();
    }

    protected void Explode()
    {
        if(currentLife <= 0)
        {
            GameObject explosion = Resources.Load<GameObject>("ExplosionShip");
            Instantiate(explosion, transform.position, transform.rotation );

            if(this.gameObject.tag == "Enemy")
                GameObject.Find("GameController").GetComponent<GameController>().ToScore();
            
            if(this.gameObject.tag == "Player")
            {
                GameObject.Find("GameController").GetComponent<GameController>().SaveData();
                GameObject.Find("GameController").GetComponent<GameController>().LoadNextScene("GameOver");
            }
            Destroy(lifeBar);
            Destroy(this.gameObject);
        }
    }
}
