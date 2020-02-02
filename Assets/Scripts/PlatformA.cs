﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformA : MonoBehaviour
{
    public enum Status
    {
        BROKEN,
        OK,
        FIXED
    }

    public enum Effect
    {
        NONE,
        BOUNCE,
        SPEED
    }

    public BoxCollider2D boxCollider2D;
    public Sprite brokenSprite;
    public Sprite okSprite;
    public Sprite fixedSprite;
    public Sprite bounceSprite;
    public Sprite dashSprite;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer upgradeRenderer;
    public SpriteRenderer shadowRenderer;
    public Status status = Status.BROKEN;
    public Effect initialEffect = Effect.NONE;
    Effect privEffect = Effect.NONE;
    public Effect effect
    {
        get { return privEffect; }

        set
        {
            switch (value)
            {
                case Effect.NONE:
                    upgradeRenderer.sprite = null;
                    break;
                case Effect.BOUNCE:
                    upgradeRenderer.sprite = bounceSprite;
                    break;
                case Effect.SPEED:
                    upgradeRenderer.sprite = dashSprite;
                    break;
                default:
                    break;
            }
            privEffect = value;
        }
    }
    public void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject shadow = Instantiate(spriteRenderer.gameObject);
        shadow.transform.parent = transform;
        shadow.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
        shadow.transform.localPosition = spriteRenderer.gameObject.transform.localPosition + new Vector3(0.02f, -0.02f);
        shadow.transform.localScale = spriteRenderer.gameObject.transform.localScale;
        shadow.transform.localRotation = spriteRenderer.gameObject.transform.localRotation;
        shadow.GetComponent<SpriteRenderer>().sortingOrder = -1;
        shadowRenderer = shadow.GetComponent<SpriteRenderer>();


        GameObject upg = Instantiate(spriteRenderer.gameObject);
        upg.transform.parent = transform;
        upg.transform.localPosition = spriteRenderer.gameObject.transform.localPosition + new Vector3(0.27f, 0.21f);
        upg.transform.localScale = spriteRenderer.gameObject.transform.localScale * 0.5f;
        upg.transform.localRotation = spriteRenderer.gameObject.transform.localRotation;
        upg.GetComponent<SpriteRenderer>().sortingOrder = 1;
        upgradeRenderer = upg.GetComponent<SpriteRenderer>();
        SetStatus(status, initialEffect);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetStatus(Status newStatus, Effect newEffect)
    {
        status = newStatus;

        switch (status)
        {
            case Status.BROKEN:
                boxCollider2D.isTrigger = true;
                spriteRenderer.sprite = brokenSprite;
                shadowRenderer.sprite = brokenSprite;
                effect = Effect.NONE;
                break;
            case Status.OK:
                spriteRenderer.sprite = okSprite;
                shadowRenderer.sprite = okSprite;
                effect = Effect.NONE;
                break;
            case Status.FIXED:
                boxCollider2D.isTrigger = false;
                spriteRenderer.sprite = fixedSprite;
                shadowRenderer.sprite = fixedSprite;
                effect = newEffect;
                break;
            default:
                break;
        }
    }

}
