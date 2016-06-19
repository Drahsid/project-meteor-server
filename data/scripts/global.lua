--[[

Globals referenced in all of the lua scripts

--]]

--ACTOR STATES

ACTORSTATE_PASSIVE = 0;
ACTORSTATE_DEAD1 = 1;
ACTORSTATE_ACTIVE = 2;
ACTORSTATE_DEAD2 = 3;
ACTORSTATE_SITTING_ONOBJ = 11;
ACTORSTATE_SITTING_ONFLOOR = 13;
ACTORSTATE_MOUNTED = 15;

--UTILS

function callClientFunction(player, functionName, ...)
	player:RunEventFunction(functionName, ...);
	result = coroutine.yield();
	return result;
end