
function _initialize ()
    -- _entity.AddSpriteComponent();

    local player = _entity.AddChild("player"); -- add a new entity in the scene as a child
    player.AddScriptComponent("player.lua"); -- add lua script onto the entity
    
    local panel = _entity.addChild("panel");
    panel.AddScriptComponent("fractal.lua");
end

function _update ()

end