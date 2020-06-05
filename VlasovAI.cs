﻿using System.Collections.Generic;
using com.kbs.empire.ai.common.player;
using com.kbs.empire.ai.common.proc;
using com.kbs.empire.ai.vlasov;
using com.kbs.empire.common.game.data;
using com.kbs.empire.common.game.hints;
using com.kbs.empire.common.util.random;
using com.kbs.empire.common.util.xml;

public class VlasovAI : AIPlayerFactory
{
    private static readonly string[] leaders_ = {"Vlasov"};

    private readonly List<string> aiGroup_ = new List<string>();
    private readonly CMTRandom random_ = new CMTRandom();

    public override List<string> bestBuildSets()
    {
        var ret = new List<string>();
        ret.Add(EmpireCC.US_BS);
        ret.Add(EmpireCC.US_SS);
        ret.Add(EmpireCC.US_AS);
        ret.Add(EmpireCC.US_ES);
        return ret;
    }

    public override CDLLHints getHints()
    {
        return VlasovPlayer.getHints();
    }


    public override AIPlayer createAIPlayer(int position, string logpath, string logname,
                                            CDLLHints hints, AIEventInterfaceI aiEvent,
                                            AICommandInterfaceI command, AIQueryI query,
                                            AICheatI cheat, int logLevel) 
    {
        if (aiGroup_.Count == 0)
        {
            for (int i = 0; i < leaders_.Length; i++)
                aiGroup_.Add(leaders_[i]);
        }

        int r = random_.nextInt(aiGroup_.Count);
        string pname = aiGroup_[r];
        aiGroup_.RemoveAt(r);

        return new VlasovPlayer(position, pname, logpath, logname, hints, aiEvent, command, query,
                                cheat, logLevel);
    }

    public override AIPlayer reloadAIPlayer(int position, CEncodedObjectInputBufferI bin,
                                            string logpath, string logname,
                                            AIEventInterfaceI aiEvent, AICommandInterfaceI command,
                                            AIQueryI query, AICheatI cheat, int logLevel)
    {
        Dictionary<string, string> caMap = bin.getAttributes();
        return new VlasovPlayer(position, caMap, bin, logpath, logname, aiEvent, command, query,
                                cheat, logLevel);
    }
}