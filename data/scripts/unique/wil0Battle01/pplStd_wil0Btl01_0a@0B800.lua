require("/quests/man/man0u0")

function init(npc)
	return "/Chara/Npc/Populace/PopulaceStandard", false, false, false, false, false, npc:GetActorClassId(), false, false, 0, 1, "TEST";	
end

function onSpawn(player, npc)
	npc:SetQuestGraphic(player, 0x2);	
end

function onEventStarted(player, npc, triggerName)

	man0u0Quest = player:GetQuest("Man0u0");
	if (man0u0Quest ~= nil) then	
	
		if (triggerName == "pushDefault") then
			player:RunEventFunction("delegateEvent", player, man0u0Quest, "processTtrNomal002", nil, nil, nil);			
		elseif (triggerName == "talkDefault") then		
			if (man0u0Quest:GetQuestFlag(MAN0U0_FLAG_TUTORIAL1_DONE) == false) then
				player:RunEventFunction("delegateEvent", player, man0u0Quest, "processTtrNomal003", nil, nil, nil);			
				player:SetEventStatus(npc, "pushDefault", false, 0x2);
				player:GetDirector():OnTalked(npc);			
				man0u0Quest:SetQuestFlag(MAN0U0_FLAG_TUTORIAL1_DONE, true);				
				man0u0Quest:SaveData();
			else
				player:RunEventFunction("delegateEvent", player, man0u0Quest, "processTtrMini001", nil, nil, nil);
				
				if (man0u0Quest:GetQuestFlag(MAN0U0_FLAG_MINITUT_DONE1) == false) then
					npc:SetQuestGraphic(player, 0x0);
					man0u0Quest:SetQuestFlag(MAN0U0_FLAG_MINITUT_DONE1, true);
					man0u0Quest:SaveData();					
				end
				
			end
		else
			player:EndEvent();
		end
	else
		player:EndEvent(); --Should not be here w.o this quest
	end	
	
end

function onEventUpdate(player, npc)	
	
	player:EndEvent();	
	
end