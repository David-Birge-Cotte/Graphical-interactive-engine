speed = 5;

function _initialize ()
    spr = _entity.addSpriteComponent();
    change_color();
end

function _update ()
    if (_input.IsKeyPressed(Keys.Space))
    then
        change_color();
    end
    if (_input.IsKeyPressed(Keys.D))
    then
        _entity.Rotate(45);
    end
    if (_input.IsKeyDown(Keys.Up))
    then
        _entity.Move(0, -dt * speed);
    end
    if (_input.IsKeyDown(Keys.Down))
    then
        _entity.Move(0, dt * speed);
    end
    if (_input.IsKeyDown(Keys.Right))
    then
        _entity.Move(dt * speed, 0);
    end
    if (_input.IsKeyDown(Keys.Left))
    then
        _entity.Move(-dt * speed, 0);
    end
end

function change_color()
    spr.ChangeColor(_color.RandomGaussianColor());
end