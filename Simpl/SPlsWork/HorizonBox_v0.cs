using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;

namespace UserModule_HORIZONBOX_V0
{
    public class UserModuleClass_HORIZONBOX_V0 : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        Crestron.Logos.SplusObjects.DigitalInput DEBUG;
        Crestron.Logos.SplusObjects.DigitalInput CONNECT;
        Crestron.Logos.SplusObjects.DigitalInput POWER;
        Crestron.Logos.SplusObjects.DigitalInput HELP;
        Crestron.Logos.SplusObjects.DigitalInput GUIDE;
        Crestron.Logos.SplusObjects.DigitalInput INFO;
        Crestron.Logos.SplusObjects.DigitalInput TEXT;
        Crestron.Logos.SplusObjects.DigitalInput MENU;
        Crestron.Logos.SplusObjects.DigitalInput MENU1;
        Crestron.Logos.SplusObjects.DigitalInput MENU2;
        Crestron.Logos.SplusObjects.DigitalInput MENU3;
        Crestron.Logos.SplusObjects.DigitalInput MENU_UP;
        Crestron.Logos.SplusObjects.DigitalInput MENU_DOWN;
        Crestron.Logos.SplusObjects.DigitalInput MENU_LEFT;
        Crestron.Logos.SplusObjects.DigitalInput MENU_RIGHT;
        Crestron.Logos.SplusObjects.DigitalInput MENU_OK;
        Crestron.Logos.SplusObjects.DigitalInput BACK;
        Crestron.Logos.SplusObjects.DigitalInput NUM_0;
        Crestron.Logos.SplusObjects.DigitalInput NUM_1;
        Crestron.Logos.SplusObjects.DigitalInput NUM_2;
        Crestron.Logos.SplusObjects.DigitalInput NUM_3;
        Crestron.Logos.SplusObjects.DigitalInput NUM_4;
        Crestron.Logos.SplusObjects.DigitalInput NUM_5;
        Crestron.Logos.SplusObjects.DigitalInput NUM_6;
        Crestron.Logos.SplusObjects.DigitalInput NUM_7;
        Crestron.Logos.SplusObjects.DigitalInput NUM_8;
        Crestron.Logos.SplusObjects.DigitalInput NUM_9;
        Crestron.Logos.SplusObjects.DigitalInput CHAN_UP;
        Crestron.Logos.SplusObjects.DigitalInput CHAN_DWN;
        Crestron.Logos.SplusObjects.DigitalInput PAUSE;
        Crestron.Logos.SplusObjects.DigitalInput STOP;
        Crestron.Logos.SplusObjects.DigitalInput RECORD;
        Crestron.Logos.SplusObjects.DigitalInput FWD;
        Crestron.Logos.SplusObjects.DigitalInput RWD;
        Crestron.Logos.SplusObjects.DigitalInput ONDEMAND;
        Crestron.Logos.SplusObjects.DigitalInput UNKNOWN_0;
        Crestron.Logos.SplusObjects.DigitalInput UNKNOWN_1;
        Crestron.Logos.SplusObjects.DigitalInput UNKNOWN_2;
        Crestron.Logos.SplusObjects.DigitalInput UNKNOWN_3;
        Crestron.Logos.SplusObjects.DigitalInput UNKNOWN_4;
        SplusTcpClient RFB_TCP_CLIENT;
        SplusTcpClient WEB_TCP_CLIENT;
        StringParameter IPADDRESS;
        UShortParameter PORT;
        ushort NCONNECTED = 0;
        ushort NEXTCHAR = 0;
        ushort STATE = 0;
        CrestronString SRXBUFTMP;
        CrestronString SRXBUF;
        CrestronString TEMP;
        CrestronString CMD;
        CrestronString HEXSTRING;
        private CrestronString PLOTBUFFERHEX (  SplusExecutionContext __context__, CrestronString DATA_IN ) 
            { 
            
            __context__.SourceCodeLine = 142;
            HEXSTRING  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 144;
            while ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Length( DATA_IN ) != 0))  ) ) 
                { 
                __context__.SourceCodeLine = 146;
                NEXTCHAR = (ushort) ( Functions.GetC( DATA_IN ) ) ; 
                __context__.SourceCodeLine = 148;
                MakeString ( TEMP , "{0:X2} ", NEXTCHAR) ; 
                __context__.SourceCodeLine = 149;
                HEXSTRING  .UpdateValue ( HEXSTRING + TEMP  ) ; 
                __context__.SourceCodeLine = 144;
                } 
            
            __context__.SourceCodeLine = 152;
            return ( HEXSTRING ) ; 
            
            }
            
        private ushort RFB_TCPCONNECT (  SplusExecutionContext __context__ ) 
            { 
            short SNSTATUS = 0;
            
            
            __context__.SourceCodeLine = 158;
            SNSTATUS = (short) ( Functions.SocketConnectClient( RFB_TCP_CLIENT , IPADDRESS  , (ushort)( PORT  .Value ) , (ushort)( 0 ) ) ) ; 
            __context__.SourceCodeLine = 159;
            NCONNECTED = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 161;
            return (ushort)( SNSTATUS) ; 
            
            }
            
        private ushort SENDKEY (  SplusExecutionContext __context__, ushort KEYSTATE , CrestronString KEY ) 
            { 
            
            __context__.SourceCodeLine = 166;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (NCONNECTED == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 168;
                RFB_TCPCONNECT (  __context__  ) ; 
                __context__.SourceCodeLine = 169;
                Functions.Delay (  (int) ( 100 ) ) ; 
                } 
            
            __context__.SourceCodeLine = 172;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (STATE == 2))  ) ) 
                { 
                __context__.SourceCodeLine = 174;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (KEYSTATE == 1))  ) ) 
                    { 
                    __context__.SourceCodeLine = 176;
                    MakeString ( CMD , "\u0004\u0001\u0000\u0000\u0000\u0000{0}", KEY ) ; 
                    __context__.SourceCodeLine = 177;
                    Functions.SocketSend ( RFB_TCP_CLIENT , CMD ) ; 
                    __context__.SourceCodeLine = 179;
                    if ( Functions.TestForTrue  ( ( DEBUG  .Value)  ) ) 
                        {
                        __context__.SourceCodeLine = 180;
                        Trace( "Module HorizonBox: {0} Send -> Press Key: {1} : Hex: {2}", GetSymbolInstanceName ( ) , KEY , PLOTBUFFERHEX (  __context__ , CMD) ) ; 
                        }
                    
                    } 
                
                else 
                    {
                    __context__.SourceCodeLine = 182;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (KEYSTATE == 0))  ) ) 
                        { 
                        __context__.SourceCodeLine = 184;
                        MakeString ( CMD , "\u0004\u0000\u0000\u0000\u0000\u0000{0}", KEY ) ; 
                        __context__.SourceCodeLine = 185;
                        Functions.SocketSend ( RFB_TCP_CLIENT , CMD ) ; 
                        __context__.SourceCodeLine = 187;
                        if ( Functions.TestForTrue  ( ( DEBUG  .Value)  ) ) 
                            {
                            __context__.SourceCodeLine = 188;
                            Trace( "Module  HorizonBox: {0} Send -> Release Key: {1} : Hex: {2}", GetSymbolInstanceName ( ) , KEY , PLOTBUFFERHEX (  __context__ , CMD) ) ; 
                            }
                        
                        } 
                    
                    }
                
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 193;
                return (ushort)( 0) ; 
                } 
            
            
            return 0; // default return value (none specified in module)
            }
            
        object RFB_TCP_CLIENT_OnSocketConnect_0 ( Object __Info__ )
        
            { 
            SocketEventInfo __SocketInfo__ = (SocketEventInfo)__Info__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SocketInfo__);
                
                __context__.SourceCodeLine = 201;
                if ( Functions.TestForTrue  ( ( DEBUG  .Value)  ) ) 
                    {
                    __context__.SourceCodeLine = 202;
                    Trace( "Module HorizonBox: {0} TCP_Client Connected!!", GetSymbolInstanceName ( ) ) ; 
                    }
                
                __context__.SourceCodeLine = 204;
                NCONNECTED = (ushort) ( 1 ) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SocketInfo__ ); }
            return this;
            
        }
        
    object RFB_TCP_CLIENT_OnSocketDisconnect_1 ( Object __Info__ )
    
        { 
        SocketEventInfo __SocketInfo__ = (SocketEventInfo)__Info__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SocketInfo__);
            
            __context__.SourceCodeLine = 209;
            if ( Functions.TestForTrue  ( ( DEBUG  .Value)  ) ) 
                {
                __context__.SourceCodeLine = 210;
                Trace( "Module HorizonBox: {0} TCP_Client disconnected!!", GetSymbolInstanceName ( ) ) ; 
                }
            
            __context__.SourceCodeLine = 211;
            NCONNECTED = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 212;
            STATE = (ushort) ( 0 ) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SocketInfo__ ); }
        return this;
        
    }
    
object RFB_TCP_CLIENT_OnSocketReceive_2 ( Object __Info__ )

    { 
    SocketEventInfo __SocketInfo__ = (SocketEventInfo)__Info__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SocketInfo__);
        
        __context__.SourceCodeLine = 217;
        SRXBUFTMP  .UpdateValue ( RFB_TCP_CLIENT .  SocketRxBuf  ) ; 
        __context__.SourceCodeLine = 219;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (STATE == 0))  ) ) 
            { 
            __context__.SourceCodeLine = 222;
            if ( Functions.TestForTrue  ( ( Functions.Find( "\u000A" , RFB_TCP_CLIENT.SocketRxBuf ))  ) ) 
                { 
                __context__.SourceCodeLine = 225;
                SRXBUF  .UpdateValue ( Functions.Remove ( "\u000A" , RFB_TCP_CLIENT .  SocketRxBuf )  ) ; 
                __context__.SourceCodeLine = 227;
                if ( Functions.TestForTrue  ( ( Functions.Find( "RFB" , SRXBUF ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 229;
                    SRXBUFTMP  .UpdateValue ( SRXBUF  ) ; 
                    __context__.SourceCodeLine = 231;
                    if ( Functions.TestForTrue  ( ( DEBUG  .Value)  ) ) 
                        {
                        __context__.SourceCodeLine = 233;
                        Trace( "Module HorizonBox: {0} Send -> Send RFB version Back {1} hex: {2}", GetSymbolInstanceName ( ) , SRXBUF , PLOTBUFFERHEX (  __context__ , SRXBUFTMP) ) ; 
                        }
                    
                    __context__.SourceCodeLine = 235;
                    Functions.SocketSend ( RFB_TCP_CLIENT , SRXBUF ) ; 
                    __context__.SourceCodeLine = 236;
                    STATE = (ushort) ( 1 ) ; 
                    } 
                
                } 
            
            } 
        
        else 
            {
            __context__.SourceCodeLine = 240;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (STATE == 1))  ) ) 
                { 
                __context__.SourceCodeLine = 243;
                if ( Functions.TestForTrue  ( ( DEBUG  .Value)  ) ) 
                    {
                    __context__.SourceCodeLine = 244;
                    Trace( "Module HorizonBox: {0} Receive <- : Get number of security types and security type (2x Bytes) hex: {1}", GetSymbolInstanceName ( ) , PLOTBUFFERHEX (  __context__ , RFB_TCP_CLIENT.SocketRxBuf) ) ; 
                    }
                
                __context__.SourceCodeLine = 245;
                Functions.SocketSend ( RFB_TCP_CLIENT , "\u0001\u0001" ) ; 
                __context__.SourceCodeLine = 247;
                STATE = (ushort) ( 2 ) ; 
                __context__.SourceCodeLine = 249;
                if ( Functions.TestForTrue  ( ( DEBUG  .Value)  ) ) 
                    {
                    __context__.SourceCodeLine = 250;
                    Trace( "Module HorizonBox: {0} Send-> security type 01", GetSymbolInstanceName ( ) ) ; 
                    }
                
                } 
            
            else 
                {
                __context__.SourceCodeLine = 252;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (STATE == 2))  ) ) 
                    { 
                    __context__.SourceCodeLine = 254;
                    if ( Functions.TestForTrue  ( ( DEBUG  .Value)  ) ) 
                        {
                        __context__.SourceCodeLine = 255;
                        Trace( "Module HorizonBox: {0} Receive <- Hn ex: {1}", GetSymbolInstanceName ( ) , PLOTBUFFERHEX (  __context__ , RFB_TCP_CLIENT.SocketRxBuf) ) ; 
                        }
                    
                    } 
                
                }
            
            }
        
        __context__.SourceCodeLine = 258;
        Functions.ClearBuffer ( RFB_TCP_CLIENT .  SocketRxBuf ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SocketInfo__ ); }
    return this;
    
}

object RFB_TCP_CLIENT_OnSocketStatus_3 ( Object __Info__ )

    { 
    SocketEventInfo __SocketInfo__ = (SocketEventInfo)__Info__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SocketInfo__);
        short STATUS = 0;
        
        
        __context__.SourceCodeLine = 267;
        STATUS = (short) ( __SocketInfo__.SocketStatus ) ; 
        __context__.SourceCodeLine = 269;
        Trace( "The SocketGetStatus returns:       {0:d}\r\n", (short)STATUS) ; 
        __context__.SourceCodeLine = 271;
        Trace( "The RFB_TCP_Client.SocketStatus returns: {0:d}\r\n", (short)RFB_TCP_CLIENT.SocketStatus) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SocketInfo__ ); }
    return this;
    
}

object CONNECT_OnPush_4 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 279;
        RFB_TCPCONNECT (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object CONNECT_OnRelease_5 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 286;
        Functions.SocketDisconnectClient ( RFB_TCP_CLIENT ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object POWER_OnPush_6 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 292;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E0\u0000") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object POWER_OnRelease_7 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 297;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E0\u0000") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object HELP_OnPush_8 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 302;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E0\u0009") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object HELP_OnRelease_9 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 307;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E0\u0009") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object GUIDE_OnPush_10 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 312;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E0\u000b") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object GUIDE_OnRelease_11 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 317;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E0\u000b") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object INFO_OnPush_12 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 322;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E0\u000e") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object INFO_OnRelease_13 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 327;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E0\u000e") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object TEXT_OnPush_14 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 332;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E0\u000f") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object TEXT_OnRelease_15 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 337;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E0\u000f") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU_OnPush_16 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 342;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E0\u000a") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU_OnRelease_17 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 347;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E0\u000a") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU1_OnPush_18 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 352;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E0\u0011") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU1_OnRelease_19 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 357;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E0\u0011") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU2_OnPush_20 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 362;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E0\u0015") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU2_OnRelease_21 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 367;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E0\u0015") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU3_OnPush_22 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 372;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00EF\u0000") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU3_OnRelease_23 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 377;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00EF\u0000") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU_UP_OnPush_24 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 383;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E1\u0000") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU_UP_OnRelease_25 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 388;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E1\u0000") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU_DOWN_OnPush_26 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 393;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E1\u0001") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU_DOWN_OnRelease_27 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 398;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E1\u0001") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU_LEFT_OnPush_28 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 403;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E1\u0002") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU_LEFT_OnRelease_29 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 408;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E1\u0002") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU_RIGHT_OnPush_30 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 413;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E1\u0003") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU_RIGHT_OnRelease_31 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 418;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E1\u0003") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU_OK_OnPush_32 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 423;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E0\u0001") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MENU_OK_OnRelease_33 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 428;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E0\u0001") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object BACK_OnPush_34 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 434;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E0\u0002") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object BACK_OnRelease_35 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 439;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E0\u0002") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_0_OnPush_36 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 444;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E3\u0000") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_0_OnRelease_37 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 449;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E3\u0001") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_1_OnPush_38 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 454;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E3\u0001") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_1_OnRelease_39 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 459;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E3\u0001") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_2_OnPush_40 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 464;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E3\u0002") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_2_OnRelease_41 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 469;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E3\u0002") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_3_OnPush_42 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 474;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E3\u0003") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_3_OnRelease_43 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 479;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E3\u0003") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_4_OnPush_44 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 484;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E3\u0004") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_4_OnRelease_45 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 489;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E3\u0004") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_5_OnPush_46 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 494;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E3\u0005") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_5_OnRelease_47 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 499;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E3\u0005") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_6_OnPush_48 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 504;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E3\u0006") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_6_OnRelease_49 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 509;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E3\u0006") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_7_OnPush_50 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 514;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E3\u0007") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_7_OnRelease_51 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 519;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E3\u0007") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_8_OnPush_52 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 524;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E3\u0008") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_8_OnRelease_53 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 529;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E3\u0008") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_9_OnPush_54 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 534;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E3\u0009") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object NUM_9_OnRelease_55 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 539;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E3\u0009") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object CHAN_UP_OnPush_56 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 544;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E0\u0006") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object CHAN_UP_OnRelease_57 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 549;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E0\u0006") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object CHAN_DWN_OnPush_58 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 554;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E0\u0007") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object CHAN_DWN_OnRelease_59 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 559;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E0\u0007") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object PAUSE_OnPush_60 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 564;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E4\u0000") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object PAUSE_OnRelease_61 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 569;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E4\u0000") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object STOP_OnPush_62 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 574;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E4\u0002") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object STOP_OnRelease_63 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 579;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E4\u0002") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object RECORD_OnPush_64 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 584;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E4\u0003") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object RECORD_OnRelease_65 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 589;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E4\u0003") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object FWD_OnPush_66 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 594;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E4\u0005") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object FWD_OnRelease_67 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 599;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E4\u0005") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object RWD_OnPush_68 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 604;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00E4\u0007") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object RWD_OnRelease_69 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 609;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00E4\u0007") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object ONDEMAND_OnPush_70 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 614;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00EF\u0028") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object ONDEMAND_OnRelease_71 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 619;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00EF\u0028") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object UNKNOWN_0_OnPush_72 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 624;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00EF\u0006") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object UNKNOWN_0_OnRelease_73 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 629;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00EF\u0006") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object UNKNOWN_1_OnPush_74 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 634;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00EF\u0015") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object UNKNOWN_1_OnRelease_75 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 639;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00EF\u0015") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object UNKNOWN_2_OnPush_76 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 644;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00EF\u0016") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object UNKNOWN_2_OnRelease_77 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 649;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00EF\u0016") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object UNKNOWN_3_OnPush_78 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 654;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00EF\u0017") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object UNKNOWN_3_OnRelease_79 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 659;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00EF\u0017") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object UNKNOWN_4_OnPush_80 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 664;
        SENDKEY (  __context__ , (ushort)( 1 ), "\u00EF\u0019") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object UNKNOWN_4_OnRelease_81 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 669;
        SENDKEY (  __context__ , (ushort)( 0 ), "\u00EF\u0019") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

public override object FunctionMain (  object __obj__ ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusFunctionMainStartCode();
        
        __context__.SourceCodeLine = 677;
        STATE = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 678;
        WaitForInitializationComplete ( ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    SocketInfo __socketinfo__ = new SocketInfo( 1, this );
    InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
    _SplusNVRAM = new SplusNVRAM( this );
    SRXBUFTMP  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
    SRXBUF  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
    TEMP  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
    CMD  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
    HEXSTRING  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
    RFB_TCP_CLIENT  = new SplusTcpClient ( 512, this );
    WEB_TCP_CLIENT  = new SplusTcpClient ( 512, this );
    
    DEBUG = new Crestron.Logos.SplusObjects.DigitalInput( DEBUG__DigitalInput__, this );
    m_DigitalInputList.Add( DEBUG__DigitalInput__, DEBUG );
    
    CONNECT = new Crestron.Logos.SplusObjects.DigitalInput( CONNECT__DigitalInput__, this );
    m_DigitalInputList.Add( CONNECT__DigitalInput__, CONNECT );
    
    POWER = new Crestron.Logos.SplusObjects.DigitalInput( POWER__DigitalInput__, this );
    m_DigitalInputList.Add( POWER__DigitalInput__, POWER );
    
    HELP = new Crestron.Logos.SplusObjects.DigitalInput( HELP__DigitalInput__, this );
    m_DigitalInputList.Add( HELP__DigitalInput__, HELP );
    
    GUIDE = new Crestron.Logos.SplusObjects.DigitalInput( GUIDE__DigitalInput__, this );
    m_DigitalInputList.Add( GUIDE__DigitalInput__, GUIDE );
    
    INFO = new Crestron.Logos.SplusObjects.DigitalInput( INFO__DigitalInput__, this );
    m_DigitalInputList.Add( INFO__DigitalInput__, INFO );
    
    TEXT = new Crestron.Logos.SplusObjects.DigitalInput( TEXT__DigitalInput__, this );
    m_DigitalInputList.Add( TEXT__DigitalInput__, TEXT );
    
    MENU = new Crestron.Logos.SplusObjects.DigitalInput( MENU__DigitalInput__, this );
    m_DigitalInputList.Add( MENU__DigitalInput__, MENU );
    
    MENU1 = new Crestron.Logos.SplusObjects.DigitalInput( MENU1__DigitalInput__, this );
    m_DigitalInputList.Add( MENU1__DigitalInput__, MENU1 );
    
    MENU2 = new Crestron.Logos.SplusObjects.DigitalInput( MENU2__DigitalInput__, this );
    m_DigitalInputList.Add( MENU2__DigitalInput__, MENU2 );
    
    MENU3 = new Crestron.Logos.SplusObjects.DigitalInput( MENU3__DigitalInput__, this );
    m_DigitalInputList.Add( MENU3__DigitalInput__, MENU3 );
    
    MENU_UP = new Crestron.Logos.SplusObjects.DigitalInput( MENU_UP__DigitalInput__, this );
    m_DigitalInputList.Add( MENU_UP__DigitalInput__, MENU_UP );
    
    MENU_DOWN = new Crestron.Logos.SplusObjects.DigitalInput( MENU_DOWN__DigitalInput__, this );
    m_DigitalInputList.Add( MENU_DOWN__DigitalInput__, MENU_DOWN );
    
    MENU_LEFT = new Crestron.Logos.SplusObjects.DigitalInput( MENU_LEFT__DigitalInput__, this );
    m_DigitalInputList.Add( MENU_LEFT__DigitalInput__, MENU_LEFT );
    
    MENU_RIGHT = new Crestron.Logos.SplusObjects.DigitalInput( MENU_RIGHT__DigitalInput__, this );
    m_DigitalInputList.Add( MENU_RIGHT__DigitalInput__, MENU_RIGHT );
    
    MENU_OK = new Crestron.Logos.SplusObjects.DigitalInput( MENU_OK__DigitalInput__, this );
    m_DigitalInputList.Add( MENU_OK__DigitalInput__, MENU_OK );
    
    BACK = new Crestron.Logos.SplusObjects.DigitalInput( BACK__DigitalInput__, this );
    m_DigitalInputList.Add( BACK__DigitalInput__, BACK );
    
    NUM_0 = new Crestron.Logos.SplusObjects.DigitalInput( NUM_0__DigitalInput__, this );
    m_DigitalInputList.Add( NUM_0__DigitalInput__, NUM_0 );
    
    NUM_1 = new Crestron.Logos.SplusObjects.DigitalInput( NUM_1__DigitalInput__, this );
    m_DigitalInputList.Add( NUM_1__DigitalInput__, NUM_1 );
    
    NUM_2 = new Crestron.Logos.SplusObjects.DigitalInput( NUM_2__DigitalInput__, this );
    m_DigitalInputList.Add( NUM_2__DigitalInput__, NUM_2 );
    
    NUM_3 = new Crestron.Logos.SplusObjects.DigitalInput( NUM_3__DigitalInput__, this );
    m_DigitalInputList.Add( NUM_3__DigitalInput__, NUM_3 );
    
    NUM_4 = new Crestron.Logos.SplusObjects.DigitalInput( NUM_4__DigitalInput__, this );
    m_DigitalInputList.Add( NUM_4__DigitalInput__, NUM_4 );
    
    NUM_5 = new Crestron.Logos.SplusObjects.DigitalInput( NUM_5__DigitalInput__, this );
    m_DigitalInputList.Add( NUM_5__DigitalInput__, NUM_5 );
    
    NUM_6 = new Crestron.Logos.SplusObjects.DigitalInput( NUM_6__DigitalInput__, this );
    m_DigitalInputList.Add( NUM_6__DigitalInput__, NUM_6 );
    
    NUM_7 = new Crestron.Logos.SplusObjects.DigitalInput( NUM_7__DigitalInput__, this );
    m_DigitalInputList.Add( NUM_7__DigitalInput__, NUM_7 );
    
    NUM_8 = new Crestron.Logos.SplusObjects.DigitalInput( NUM_8__DigitalInput__, this );
    m_DigitalInputList.Add( NUM_8__DigitalInput__, NUM_8 );
    
    NUM_9 = new Crestron.Logos.SplusObjects.DigitalInput( NUM_9__DigitalInput__, this );
    m_DigitalInputList.Add( NUM_9__DigitalInput__, NUM_9 );
    
    CHAN_UP = new Crestron.Logos.SplusObjects.DigitalInput( CHAN_UP__DigitalInput__, this );
    m_DigitalInputList.Add( CHAN_UP__DigitalInput__, CHAN_UP );
    
    CHAN_DWN = new Crestron.Logos.SplusObjects.DigitalInput( CHAN_DWN__DigitalInput__, this );
    m_DigitalInputList.Add( CHAN_DWN__DigitalInput__, CHAN_DWN );
    
    PAUSE = new Crestron.Logos.SplusObjects.DigitalInput( PAUSE__DigitalInput__, this );
    m_DigitalInputList.Add( PAUSE__DigitalInput__, PAUSE );
    
    STOP = new Crestron.Logos.SplusObjects.DigitalInput( STOP__DigitalInput__, this );
    m_DigitalInputList.Add( STOP__DigitalInput__, STOP );
    
    RECORD = new Crestron.Logos.SplusObjects.DigitalInput( RECORD__DigitalInput__, this );
    m_DigitalInputList.Add( RECORD__DigitalInput__, RECORD );
    
    FWD = new Crestron.Logos.SplusObjects.DigitalInput( FWD__DigitalInput__, this );
    m_DigitalInputList.Add( FWD__DigitalInput__, FWD );
    
    RWD = new Crestron.Logos.SplusObjects.DigitalInput( RWD__DigitalInput__, this );
    m_DigitalInputList.Add( RWD__DigitalInput__, RWD );
    
    ONDEMAND = new Crestron.Logos.SplusObjects.DigitalInput( ONDEMAND__DigitalInput__, this );
    m_DigitalInputList.Add( ONDEMAND__DigitalInput__, ONDEMAND );
    
    UNKNOWN_0 = new Crestron.Logos.SplusObjects.DigitalInput( UNKNOWN_0__DigitalInput__, this );
    m_DigitalInputList.Add( UNKNOWN_0__DigitalInput__, UNKNOWN_0 );
    
    UNKNOWN_1 = new Crestron.Logos.SplusObjects.DigitalInput( UNKNOWN_1__DigitalInput__, this );
    m_DigitalInputList.Add( UNKNOWN_1__DigitalInput__, UNKNOWN_1 );
    
    UNKNOWN_2 = new Crestron.Logos.SplusObjects.DigitalInput( UNKNOWN_2__DigitalInput__, this );
    m_DigitalInputList.Add( UNKNOWN_2__DigitalInput__, UNKNOWN_2 );
    
    UNKNOWN_3 = new Crestron.Logos.SplusObjects.DigitalInput( UNKNOWN_3__DigitalInput__, this );
    m_DigitalInputList.Add( UNKNOWN_3__DigitalInput__, UNKNOWN_3 );
    
    UNKNOWN_4 = new Crestron.Logos.SplusObjects.DigitalInput( UNKNOWN_4__DigitalInput__, this );
    m_DigitalInputList.Add( UNKNOWN_4__DigitalInput__, UNKNOWN_4 );
    
    PORT = new UShortParameter( PORT__Parameter__, this );
    m_ParameterList.Add( PORT__Parameter__, PORT );
    
    IPADDRESS = new StringParameter( IPADDRESS__Parameter__, this );
    m_ParameterList.Add( IPADDRESS__Parameter__, IPADDRESS );
    
    
    RFB_TCP_CLIENT.OnSocketConnect.Add( new SocketHandlerWrapper( RFB_TCP_CLIENT_OnSocketConnect_0, false ) );
    RFB_TCP_CLIENT.OnSocketDisconnect.Add( new SocketHandlerWrapper( RFB_TCP_CLIENT_OnSocketDisconnect_1, false ) );
    RFB_TCP_CLIENT.OnSocketReceive.Add( new SocketHandlerWrapper( RFB_TCP_CLIENT_OnSocketReceive_2, false ) );
    RFB_TCP_CLIENT.OnSocketStatus.Add( new SocketHandlerWrapper( RFB_TCP_CLIENT_OnSocketStatus_3, false ) );
    CONNECT.OnDigitalPush.Add( new InputChangeHandlerWrapper( CONNECT_OnPush_4, false ) );
    CONNECT.OnDigitalRelease.Add( new InputChangeHandlerWrapper( CONNECT_OnRelease_5, false ) );
    POWER.OnDigitalPush.Add( new InputChangeHandlerWrapper( POWER_OnPush_6, false ) );
    POWER.OnDigitalRelease.Add( new InputChangeHandlerWrapper( POWER_OnRelease_7, false ) );
    HELP.OnDigitalPush.Add( new InputChangeHandlerWrapper( HELP_OnPush_8, false ) );
    HELP.OnDigitalRelease.Add( new InputChangeHandlerWrapper( HELP_OnRelease_9, false ) );
    GUIDE.OnDigitalPush.Add( new InputChangeHandlerWrapper( GUIDE_OnPush_10, false ) );
    GUIDE.OnDigitalRelease.Add( new InputChangeHandlerWrapper( GUIDE_OnRelease_11, false ) );
    INFO.OnDigitalPush.Add( new InputChangeHandlerWrapper( INFO_OnPush_12, false ) );
    INFO.OnDigitalRelease.Add( new InputChangeHandlerWrapper( INFO_OnRelease_13, false ) );
    TEXT.OnDigitalPush.Add( new InputChangeHandlerWrapper( TEXT_OnPush_14, false ) );
    TEXT.OnDigitalRelease.Add( new InputChangeHandlerWrapper( TEXT_OnRelease_15, false ) );
    MENU.OnDigitalPush.Add( new InputChangeHandlerWrapper( MENU_OnPush_16, false ) );
    MENU.OnDigitalRelease.Add( new InputChangeHandlerWrapper( MENU_OnRelease_17, false ) );
    MENU1.OnDigitalPush.Add( new InputChangeHandlerWrapper( MENU1_OnPush_18, false ) );
    MENU1.OnDigitalRelease.Add( new InputChangeHandlerWrapper( MENU1_OnRelease_19, false ) );
    MENU2.OnDigitalPush.Add( new InputChangeHandlerWrapper( MENU2_OnPush_20, false ) );
    MENU2.OnDigitalRelease.Add( new InputChangeHandlerWrapper( MENU2_OnRelease_21, false ) );
    MENU3.OnDigitalPush.Add( new InputChangeHandlerWrapper( MENU3_OnPush_22, false ) );
    MENU3.OnDigitalRelease.Add( new InputChangeHandlerWrapper( MENU3_OnRelease_23, false ) );
    MENU_UP.OnDigitalPush.Add( new InputChangeHandlerWrapper( MENU_UP_OnPush_24, false ) );
    MENU_UP.OnDigitalRelease.Add( new InputChangeHandlerWrapper( MENU_UP_OnRelease_25, false ) );
    MENU_DOWN.OnDigitalPush.Add( new InputChangeHandlerWrapper( MENU_DOWN_OnPush_26, false ) );
    MENU_DOWN.OnDigitalRelease.Add( new InputChangeHandlerWrapper( MENU_DOWN_OnRelease_27, false ) );
    MENU_LEFT.OnDigitalPush.Add( new InputChangeHandlerWrapper( MENU_LEFT_OnPush_28, false ) );
    MENU_LEFT.OnDigitalRelease.Add( new InputChangeHandlerWrapper( MENU_LEFT_OnRelease_29, false ) );
    MENU_RIGHT.OnDigitalPush.Add( new InputChangeHandlerWrapper( MENU_RIGHT_OnPush_30, false ) );
    MENU_RIGHT.OnDigitalRelease.Add( new InputChangeHandlerWrapper( MENU_RIGHT_OnRelease_31, false ) );
    MENU_OK.OnDigitalPush.Add( new InputChangeHandlerWrapper( MENU_OK_OnPush_32, false ) );
    MENU_OK.OnDigitalRelease.Add( new InputChangeHandlerWrapper( MENU_OK_OnRelease_33, false ) );
    BACK.OnDigitalPush.Add( new InputChangeHandlerWrapper( BACK_OnPush_34, false ) );
    BACK.OnDigitalRelease.Add( new InputChangeHandlerWrapper( BACK_OnRelease_35, false ) );
    NUM_0.OnDigitalPush.Add( new InputChangeHandlerWrapper( NUM_0_OnPush_36, false ) );
    NUM_0.OnDigitalRelease.Add( new InputChangeHandlerWrapper( NUM_0_OnRelease_37, false ) );
    NUM_1.OnDigitalPush.Add( new InputChangeHandlerWrapper( NUM_1_OnPush_38, false ) );
    NUM_1.OnDigitalRelease.Add( new InputChangeHandlerWrapper( NUM_1_OnRelease_39, false ) );
    NUM_2.OnDigitalPush.Add( new InputChangeHandlerWrapper( NUM_2_OnPush_40, false ) );
    NUM_2.OnDigitalRelease.Add( new InputChangeHandlerWrapper( NUM_2_OnRelease_41, false ) );
    NUM_3.OnDigitalPush.Add( new InputChangeHandlerWrapper( NUM_3_OnPush_42, false ) );
    NUM_3.OnDigitalRelease.Add( new InputChangeHandlerWrapper( NUM_3_OnRelease_43, false ) );
    NUM_4.OnDigitalPush.Add( new InputChangeHandlerWrapper( NUM_4_OnPush_44, false ) );
    NUM_4.OnDigitalRelease.Add( new InputChangeHandlerWrapper( NUM_4_OnRelease_45, false ) );
    NUM_5.OnDigitalPush.Add( new InputChangeHandlerWrapper( NUM_5_OnPush_46, false ) );
    NUM_5.OnDigitalRelease.Add( new InputChangeHandlerWrapper( NUM_5_OnRelease_47, false ) );
    NUM_6.OnDigitalPush.Add( new InputChangeHandlerWrapper( NUM_6_OnPush_48, false ) );
    NUM_6.OnDigitalRelease.Add( new InputChangeHandlerWrapper( NUM_6_OnRelease_49, false ) );
    NUM_7.OnDigitalPush.Add( new InputChangeHandlerWrapper( NUM_7_OnPush_50, false ) );
    NUM_7.OnDigitalRelease.Add( new InputChangeHandlerWrapper( NUM_7_OnRelease_51, false ) );
    NUM_8.OnDigitalPush.Add( new InputChangeHandlerWrapper( NUM_8_OnPush_52, false ) );
    NUM_8.OnDigitalRelease.Add( new InputChangeHandlerWrapper( NUM_8_OnRelease_53, false ) );
    NUM_9.OnDigitalPush.Add( new InputChangeHandlerWrapper( NUM_9_OnPush_54, false ) );
    NUM_9.OnDigitalRelease.Add( new InputChangeHandlerWrapper( NUM_9_OnRelease_55, false ) );
    CHAN_UP.OnDigitalPush.Add( new InputChangeHandlerWrapper( CHAN_UP_OnPush_56, false ) );
    CHAN_UP.OnDigitalRelease.Add( new InputChangeHandlerWrapper( CHAN_UP_OnRelease_57, false ) );
    CHAN_DWN.OnDigitalPush.Add( new InputChangeHandlerWrapper( CHAN_DWN_OnPush_58, false ) );
    CHAN_DWN.OnDigitalRelease.Add( new InputChangeHandlerWrapper( CHAN_DWN_OnRelease_59, false ) );
    PAUSE.OnDigitalPush.Add( new InputChangeHandlerWrapper( PAUSE_OnPush_60, false ) );
    PAUSE.OnDigitalRelease.Add( new InputChangeHandlerWrapper( PAUSE_OnRelease_61, false ) );
    STOP.OnDigitalPush.Add( new InputChangeHandlerWrapper( STOP_OnPush_62, false ) );
    STOP.OnDigitalRelease.Add( new InputChangeHandlerWrapper( STOP_OnRelease_63, false ) );
    RECORD.OnDigitalPush.Add( new InputChangeHandlerWrapper( RECORD_OnPush_64, false ) );
    RECORD.OnDigitalRelease.Add( new InputChangeHandlerWrapper( RECORD_OnRelease_65, false ) );
    FWD.OnDigitalPush.Add( new InputChangeHandlerWrapper( FWD_OnPush_66, false ) );
    FWD.OnDigitalRelease.Add( new InputChangeHandlerWrapper( FWD_OnRelease_67, false ) );
    RWD.OnDigitalPush.Add( new InputChangeHandlerWrapper( RWD_OnPush_68, false ) );
    RWD.OnDigitalRelease.Add( new InputChangeHandlerWrapper( RWD_OnRelease_69, false ) );
    ONDEMAND.OnDigitalPush.Add( new InputChangeHandlerWrapper( ONDEMAND_OnPush_70, false ) );
    ONDEMAND.OnDigitalRelease.Add( new InputChangeHandlerWrapper( ONDEMAND_OnRelease_71, false ) );
    UNKNOWN_0.OnDigitalPush.Add( new InputChangeHandlerWrapper( UNKNOWN_0_OnPush_72, false ) );
    UNKNOWN_0.OnDigitalRelease.Add( new InputChangeHandlerWrapper( UNKNOWN_0_OnRelease_73, false ) );
    UNKNOWN_1.OnDigitalPush.Add( new InputChangeHandlerWrapper( UNKNOWN_1_OnPush_74, false ) );
    UNKNOWN_1.OnDigitalRelease.Add( new InputChangeHandlerWrapper( UNKNOWN_1_OnRelease_75, false ) );
    UNKNOWN_2.OnDigitalPush.Add( new InputChangeHandlerWrapper( UNKNOWN_2_OnPush_76, false ) );
    UNKNOWN_2.OnDigitalRelease.Add( new InputChangeHandlerWrapper( UNKNOWN_2_OnRelease_77, false ) );
    UNKNOWN_3.OnDigitalPush.Add( new InputChangeHandlerWrapper( UNKNOWN_3_OnPush_78, false ) );
    UNKNOWN_3.OnDigitalRelease.Add( new InputChangeHandlerWrapper( UNKNOWN_3_OnRelease_79, false ) );
    UNKNOWN_4.OnDigitalPush.Add( new InputChangeHandlerWrapper( UNKNOWN_4_OnPush_80, false ) );
    UNKNOWN_4.OnDigitalRelease.Add( new InputChangeHandlerWrapper( UNKNOWN_4_OnRelease_81, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_HORIZONBOX_V0 ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint DEBUG__DigitalInput__ = 0;
const uint CONNECT__DigitalInput__ = 1;
const uint POWER__DigitalInput__ = 2;
const uint HELP__DigitalInput__ = 3;
const uint GUIDE__DigitalInput__ = 4;
const uint INFO__DigitalInput__ = 5;
const uint TEXT__DigitalInput__ = 6;
const uint MENU__DigitalInput__ = 7;
const uint MENU1__DigitalInput__ = 8;
const uint MENU2__DigitalInput__ = 9;
const uint MENU3__DigitalInput__ = 10;
const uint MENU_UP__DigitalInput__ = 11;
const uint MENU_DOWN__DigitalInput__ = 12;
const uint MENU_LEFT__DigitalInput__ = 13;
const uint MENU_RIGHT__DigitalInput__ = 14;
const uint MENU_OK__DigitalInput__ = 15;
const uint BACK__DigitalInput__ = 16;
const uint NUM_0__DigitalInput__ = 17;
const uint NUM_1__DigitalInput__ = 18;
const uint NUM_2__DigitalInput__ = 19;
const uint NUM_3__DigitalInput__ = 20;
const uint NUM_4__DigitalInput__ = 21;
const uint NUM_5__DigitalInput__ = 22;
const uint NUM_6__DigitalInput__ = 23;
const uint NUM_7__DigitalInput__ = 24;
const uint NUM_8__DigitalInput__ = 25;
const uint NUM_9__DigitalInput__ = 26;
const uint CHAN_UP__DigitalInput__ = 27;
const uint CHAN_DWN__DigitalInput__ = 28;
const uint PAUSE__DigitalInput__ = 29;
const uint STOP__DigitalInput__ = 30;
const uint RECORD__DigitalInput__ = 31;
const uint FWD__DigitalInput__ = 32;
const uint RWD__DigitalInput__ = 33;
const uint ONDEMAND__DigitalInput__ = 34;
const uint UNKNOWN_0__DigitalInput__ = 35;
const uint UNKNOWN_1__DigitalInput__ = 36;
const uint UNKNOWN_2__DigitalInput__ = 37;
const uint UNKNOWN_3__DigitalInput__ = 38;
const uint UNKNOWN_4__DigitalInput__ = 39;
const uint IPADDRESS__Parameter__ = 10;
const uint PORT__Parameter__ = 11;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    
}

SplusNVRAM _SplusNVRAM = null;

public class __CEvent__ : CEvent
{
    public __CEvent__() {}
    public void Close() { base.Close(); }
    public int Reset() { return base.Reset() ? 1 : 0; }
    public int Set() { return base.Set() ? 1 : 0; }
    public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
}
public class __CMutex__ : CMutex
{
    public __CMutex__() {}
    public void Close() { base.Close(); }
    public void ReleaseMutex() { base.ReleaseMutex(); }
    public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
}
 public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
}


}
