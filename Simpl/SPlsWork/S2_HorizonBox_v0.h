#ifndef __S2_HORIZONBOX_V0_H__
#define __S2_HORIZONBOX_V0_H__




/*
* Constructor and Destructor
*/

/*
* DIGITAL_INPUT
*/
#define __S2_HorizonBox_v0_DEBUG_DIG_INPUT 0
#define __S2_HorizonBox_v0_CONNECT_DIG_INPUT 1
#define __S2_HorizonBox_v0_POWER_DIG_INPUT 2
#define __S2_HorizonBox_v0_HELP_DIG_INPUT 3
#define __S2_HorizonBox_v0_GUIDE_DIG_INPUT 4
#define __S2_HorizonBox_v0_INFO_DIG_INPUT 5
#define __S2_HorizonBox_v0_TEXT_DIG_INPUT 6
#define __S2_HorizonBox_v0_MENU_DIG_INPUT 7
#define __S2_HorizonBox_v0_MENU1_DIG_INPUT 8
#define __S2_HorizonBox_v0_MENU2_DIG_INPUT 9
#define __S2_HorizonBox_v0_MENU3_DIG_INPUT 10
#define __S2_HorizonBox_v0_MENU_UP_DIG_INPUT 11
#define __S2_HorizonBox_v0_MENU_DOWN_DIG_INPUT 12
#define __S2_HorizonBox_v0_MENU_LEFT_DIG_INPUT 13
#define __S2_HorizonBox_v0_MENU_RIGHT_DIG_INPUT 14
#define __S2_HorizonBox_v0_MENU_OK_DIG_INPUT 15
#define __S2_HorizonBox_v0_BACK_DIG_INPUT 16
#define __S2_HorizonBox_v0_NUM_0_DIG_INPUT 17
#define __S2_HorizonBox_v0_NUM_1_DIG_INPUT 18
#define __S2_HorizonBox_v0_NUM_2_DIG_INPUT 19
#define __S2_HorizonBox_v0_NUM_3_DIG_INPUT 20
#define __S2_HorizonBox_v0_NUM_4_DIG_INPUT 21
#define __S2_HorizonBox_v0_NUM_5_DIG_INPUT 22
#define __S2_HorizonBox_v0_NUM_6_DIG_INPUT 23
#define __S2_HorizonBox_v0_NUM_7_DIG_INPUT 24
#define __S2_HorizonBox_v0_NUM_8_DIG_INPUT 25
#define __S2_HorizonBox_v0_NUM_9_DIG_INPUT 26
#define __S2_HorizonBox_v0_CHAN_UP_DIG_INPUT 27
#define __S2_HorizonBox_v0_CHAN_DWN_DIG_INPUT 28
#define __S2_HorizonBox_v0_PAUSE_DIG_INPUT 29
#define __S2_HorizonBox_v0_STOP_DIG_INPUT 30
#define __S2_HorizonBox_v0_RECORD_DIG_INPUT 31
#define __S2_HorizonBox_v0_FWD_DIG_INPUT 32
#define __S2_HorizonBox_v0_RWD_DIG_INPUT 33
#define __S2_HorizonBox_v0_ONDEMAND_DIG_INPUT 34
#define __S2_HorizonBox_v0_UNKNOWN_0_DIG_INPUT 35
#define __S2_HorizonBox_v0_UNKNOWN_1_DIG_INPUT 36
#define __S2_HorizonBox_v0_UNKNOWN_2_DIG_INPUT 37
#define __S2_HorizonBox_v0_UNKNOWN_3_DIG_INPUT 38
#define __S2_HorizonBox_v0_UNKNOWN_4_DIG_INPUT 39


/*
* ANALOG_INPUT
*/




/*
* DIGITAL_OUTPUT
*/


/*
* ANALOG_OUTPUT
*/



/*
* Direct Socket Variables
*/

#define __S2_HorizonBox_v0_RFB_TCP_CLIENT_SOCKET 0
#define __S2_HorizonBox_v0_RFB_TCP_CLIENT_STRING_MAX_LEN 512
START_SOCKET_DEFINITION( S2_HorizonBox_v0, __RFB_TCP_CLIENT )
{
   int SocketStatus;
   enum ESplusSocketType eSocketType;
   int SocketID;
   void *SocketPtr;
CREATE_SOCKET_STRING( S2_HorizonBox_v0, SocketRxBuf, __S2_HorizonBox_v0_RFB_TCP_CLIENT_STRING_MAX_LEN );
};
#define __S2_HorizonBox_v0_WEB_TCP_CLIENT_SOCKET 1
#define __S2_HorizonBox_v0_WEB_TCP_CLIENT_STRING_MAX_LEN 512
START_SOCKET_DEFINITION( S2_HorizonBox_v0, __WEB_TCP_CLIENT )
{
   int SocketStatus;
   enum ESplusSocketType eSocketType;
   int SocketID;
   void *SocketPtr;
CREATE_SOCKET_STRING( S2_HorizonBox_v0, SocketRxBuf, __S2_HorizonBox_v0_WEB_TCP_CLIENT_STRING_MAX_LEN );
};



/*
* INTEGER_PARAMETER
*/
#define __S2_HorizonBox_v0_PORT_INTEGER_PARAMETER 11
/*
* SIGNED_INTEGER_PARAMETER
*/
/*
* LONG_INTEGER_PARAMETER
*/
/*
* SIGNED_LONG_INTEGER_PARAMETER
*/
/*
* INTEGER_PARAMETER
*/
/*
* SIGNED_INTEGER_PARAMETER
*/
/*
* LONG_INTEGER_PARAMETER
*/
/*
* SIGNED_LONG_INTEGER_PARAMETER
*/
/*
* STRING_PARAMETER
*/
#define __S2_HorizonBox_v0_IPADDRESS_STRING_PARAMETER 10
#define __S2_HorizonBox_v0_IPADDRESS_PARAM_MAX_LEN 15
CREATE_STRING_STRUCT( S2_HorizonBox_v0, __IPADDRESS, __S2_HorizonBox_v0_IPADDRESS_PARAM_MAX_LEN );


/*
* INTEGER
*/


/*
* LONG_INTEGER
*/


/*
* SIGNED_INTEGER
*/


/*
* SIGNED_LONG_INTEGER
*/


/*
* STRING
*/
#define __S2_HorizonBox_v0_SRXBUFTMP_STRING_MAX_LEN 512
CREATE_STRING_STRUCT( S2_HorizonBox_v0, __SRXBUFTMP, __S2_HorizonBox_v0_SRXBUFTMP_STRING_MAX_LEN );
#define __S2_HorizonBox_v0_SRXBUF_STRING_MAX_LEN 512
CREATE_STRING_STRUCT( S2_HorizonBox_v0, __SRXBUF, __S2_HorizonBox_v0_SRXBUF_STRING_MAX_LEN );
#define __S2_HorizonBox_v0_TEMP_STRING_MAX_LEN 512
CREATE_STRING_STRUCT( S2_HorizonBox_v0, __TEMP, __S2_HorizonBox_v0_TEMP_STRING_MAX_LEN );
#define __S2_HorizonBox_v0_CMD_STRING_MAX_LEN 512
CREATE_STRING_STRUCT( S2_HorizonBox_v0, __CMD, __S2_HorizonBox_v0_CMD_STRING_MAX_LEN );
#define __S2_HorizonBox_v0_HEXSTRING_STRING_MAX_LEN 512
CREATE_STRING_STRUCT( S2_HorizonBox_v0, __HEXSTRING, __S2_HorizonBox_v0_HEXSTRING_STRING_MAX_LEN );

/*
* STRUCTURE
*/

START_GLOBAL_VAR_STRUCT( S2_HorizonBox_v0 )
{
   void* InstancePtr;
   struct GenericOutputString_s sGenericOutStr;
   unsigned short LastModifiedArrayIndex;

   unsigned short __NCONNECTED;
   unsigned short __NEXTCHAR;
   unsigned short __STATE;
   DECLARE_STRING_STRUCT( S2_HorizonBox_v0, __SRXBUFTMP );
   DECLARE_STRING_STRUCT( S2_HorizonBox_v0, __SRXBUF );
   DECLARE_STRING_STRUCT( S2_HorizonBox_v0, __TEMP );
   DECLARE_STRING_STRUCT( S2_HorizonBox_v0, __CMD );
   DECLARE_STRING_STRUCT( S2_HorizonBox_v0, __HEXSTRING );
   DECLARE_SOCKET( S2_HorizonBox_v0, __RFB_TCP_CLIENT );
   DECLARE_SOCKET( S2_HorizonBox_v0, __WEB_TCP_CLIENT );
   DECLARE_STRING_STRUCT( S2_HorizonBox_v0, __IPADDRESS );
};

START_NVRAM_VAR_STRUCT( S2_HorizonBox_v0 )
{
};



#endif //__S2_HORIZONBOX_V0_H__

