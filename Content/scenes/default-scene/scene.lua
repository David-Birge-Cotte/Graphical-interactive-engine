i = 0

function _initialize ()
    local p = entity.AddChild("player"); -- add a new entity in the scene
    p.AddScriptComponent("player.lua"); -- add lua script onto the entity
end

function _update ()

end