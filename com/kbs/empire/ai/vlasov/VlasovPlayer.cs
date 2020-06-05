using System;
using System.Collections.Generic;
using com.kbs.empire.ai.common.cevent;
using com.kbs.empire.ai.common.player;
using com.kbs.empire.ai.common.proc;
using com.kbs.empire.common.game.hints;
using com.kbs.empire.common.util.log;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.ai.vlasov
{
    public class VlasovPlayer : AIPlayerData
    {
        public readonly CSubLog elogger_ = null;

        private readonly CDLLHints hints_;

        public VlasovPlayer(int position, string pname, string logpath, string logname,
                            CDLLHints hints, AIEventInterfaceI aiEvent,
                            AICommandInterfaceI command, AIQueryI query, AICheatI cheat,
                            int logLevel) : base(position, logpath, logname, aiEvent, command,
                                                 query, cheat, logLevel)
        {
            elogger_ = new CSubLog("Vlasov Player:" + Convert.ToString(position), realLog_);
            elogger_.info("D Logger Log Open: " + logpath + " " + logname);
            elogger_.info(pname + " waking up");

            hints_ = hints.copy();

            pname_ = pname;
        }

        protected override void runTurn()
        {
            try
            {
                elogger_.info("Running Turn");

                if (pollAllEvents()) { ackHold(); return; }

                elogger_.info("Ending Turn");
            }
            catch (Exception T)
            {
                elogger_.info(T);
            }
        }

        public override void processEvent(CGameEvent ge, CSubLog logger)
        {
            base.processEvent(ge, logger);

            elogger_.info("Got Event:" + ge.type_);
        }

        private const string TEST_ATTR = "TA";

        private int testAttribute_ = 12;
        public override void encodeInternal(CEncodedObjectOutputBufferI output)
        {
            encodeAttr(output);

            output.addAttr(TEST_ATTR, Convert.ToString(testAttribute_));

            encodeChildren(output);

            hints_.encode(output);

        }

        public VlasovPlayer(
            int position, 
            Dictionary<string, string> caMap, 
            CEncodedObjectInputBufferI bin, 
            string logpath, 
            string logname,
            AIEventInterfaceI aiEvent, 
            AICommandInterfaceI command, 
            AIQueryI query, 
            AICheatI cheat,
            int logLevel) 
            : base(
                position, 
                logpath, 
                logname, 
                caMap,
                bin, 
                aiEvent, 
                command, 
                query, 
                cheat,
                logLevel)
        {
            elogger_ = new CSubLog("ExamplePlayer:" + Convert.ToString(position), realLog_);
            elogger_.info("D Logger Log Open: " + logpath + " " + logname);
            elogger_.info("Position " + Convert.ToSingle(position) + " waking up.");

            testAttribute_ = EncodeUtil.parseInt(caMap[TEST_ATTR]);

            hints_ = new CDLLHints(bin);
        }

        public override void aiRestored(CStateEvent cse)
        {
            elogger_.info("Restored started");
            base.aiRestored(cse);
            elogger_.info("Restored done");
            elogger_.info(pname_ + " is now self aware.");
        }

        private const string DO_NOTHING_ATTR = "DN";
        public static CDLLHints getHints()
        {
            var ret = new CDLLHints(new CDLLInfo(
                "Vlasov",
                "Vlasov AI",
                "Vlasov Stub For AI...not too challenging",
                "1.0"
            ));
            ret.addInfo(new CDLLBoolHintInfo(
                DO_NOTHING_ATTR,
                "Do nothing",
                "Do Not Expand",
                false
            ));
            return ret;
        }

    }
}
