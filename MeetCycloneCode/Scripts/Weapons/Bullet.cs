using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bullet : Projectile {

	public Color trailColor;

    protected override void Start()
    {
        base.Start();
        GetComponent<TrailRenderer>().material.SetColor("_TintColor", trailColor);
    }
}
