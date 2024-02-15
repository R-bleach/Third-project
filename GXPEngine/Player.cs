using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class Player : AnimationSprite
{
    public Vec2 position
    {
        get
        {
            return _position;
        }
    }
    public Vec2 velocity;
    Vec2 _position;
    float _speed;
    float gravity = 0.5f;
    bool _autoRotateLeft = false;
    bool _autoRotateRight = false;
    bool _move = false;

    public Player(string fileName, int cols, int rows, TiledObject obj = null) : base("Assets/spaceship.png", 1, 1)
    {
        Initialize(obj);
    }

    void Initialize(TiledObject obj)
    {
        _position = new Vec2(x, y);
        _speed = 0.8f;
        SetOrigin(width / 2, height / 2);
        rotation = 270;
        scaleY = .3f;
        scaleX = .4f;
        _position.x = game.width / 2;
        _position.y = 3300;
    }
    void Update()
    {
        Movement();
        UpdateScreenPosition();
        //Console.WriteLine(rotation);
    }
    void UpdateScreenPosition()
    {
        x = _position.x;
        y = _position.y;

    }
    void Movement()
    {
        if (velocity.y < 50)
        {
            velocity.y += gravity;
        }
        collisions();

        //add gravity

        if (Input.GetKey(Key.A))
        {
            //boost left
            velocity.x -= _speed;

            if (velocity.y > -50)
            {
                velocity.y -= _speed;
            }
            _autoRotateLeft = true;
            if (rotation <= -50)
            {
                _autoRotateLeft = false;
            }
        }
        else
        {
            _autoRotateLeft = false;
            _move = false;
        }
        if (Input.GetKey(Key.D))
        {
            //boost right
            _autoRotateRight = true;
            velocity.x += _speed;
            if (velocity.y > -50)
            {
                velocity.y -= _speed;
            }

            if (rotation >= 50)
            {
                _autoRotateRight = false;
            }
        }
        else if (Input.GetKey(Key.A))
        {
            _autoRotateRight = false;
        }
        else
        {
            _autoRotateRight = false;
            _move = false;
        }

        if (_autoRotateLeft)
        {
            rotation -= 1;
        }
        if (_autoRotateRight)
        {
            rotation += 1;
        }

        _position += velocity * _speed;
    }
    void collisions()
    {
        Collision colx = MoveUntilCollision(velocity.x, 0);
        Collision coly = MoveUntilCollision(0, velocity.y);
        if (coly != null)
        {
            if (coly.normal.y > 0)
            {
                velocity.y = 0;
                _position.y += _speed + 1;
            }
            if (coly.normal.y < 0)
            {
                velocity.y = 0;
            }
        }
        if (colx != null)
        {
            if (colx.normal.x > 0)
            {
                ;
                velocity.x = 0;
                _position.x += 1;
            }
            if (colx.normal.x < 0)
            {
                velocity.x = 0;
                _position.x -= 1;
            }
        }
    }

}