speed = 10;

function initialize ()
    print("Hello from player.lua");
    spr = entity.addSpriteComponent();
    spr.RandomizeColor();
end

function update ()
    if (Keyboard.IsKeyDown(Keys.Up))
    then
        entity.Move(0, -dt * speed);
    end
    if (Keyboard.IsKeyDown(Keys.Down))
    then
        entity.Move(0, dt * speed);
    end
    if (Keyboard.IsKeyDown(Keys.Right))
    then
        entity.Move(dt * speed, 0);
    end
    if (Keyboard.IsKeyDown(Keys.Left))
    then
        entity.Move(-dt * speed, 0);
    end
    if (Keyboard.IsKeyDown(Keys.Space))
    then
        spr.RandomizeColor();
        local m = import('module_test');
    end
end