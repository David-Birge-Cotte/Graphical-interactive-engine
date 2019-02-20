function initialize ()
    local p = scene.Instantiate("player");
    p.AddScriptComponent("player.lua");

    local e = p.AddChild("other");
end

function update ()

end