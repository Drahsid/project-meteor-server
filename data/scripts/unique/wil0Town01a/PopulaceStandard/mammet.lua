
function onEventStarted(player, npc)
    defaultWil = GetStaticActor("DftWil");
    player:RunEventFunction("delegateEvent", player, defaultWil, "defaultTalkWithMammet_001", nil, nil, nil);
end
