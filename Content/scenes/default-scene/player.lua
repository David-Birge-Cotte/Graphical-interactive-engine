speed = 5;

function _initialize ()
    print("Hello from player.lua");
    spr = entity.addSpriteComponent();
    spr.RandomizeColor();
end

function _update ()
    if (input.IsKeyPressed(Keys.Space))
    then
        spr.RandomizeColor();
    end
    if (input.IsKeyPressed(Keys.D))
    then
        entity.Rotate(45);
    end
    if (input.IsKeyDown(Keys.Up))
    then
        entity.Move(0, -dt * speed);
    end
    if (input.IsKeyDown(Keys.Down))
    then
        entity.Move(0, dt * speed);
    end
    if (input.IsKeyDown(Keys.Right))
    then
        entity.Move(dt * speed, 0);
    end
    if (input.IsKeyDown(Keys.Left))
    then
        entity.Move(-dt * speed, 0);
    end
end
