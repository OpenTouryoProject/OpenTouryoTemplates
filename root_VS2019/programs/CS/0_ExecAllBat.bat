echo | call 1_DeleteDir.bat
echo | call 1_DeleteFile.bat

@echo on
timeout 5

echo | call 3_Build_Business_net48.bat
echo | call 3_Build_Business_netcore30.bat
echo | call 3_Build_BusinessRichClient_net48.bat
echo | call 3_Build_BusinessRichClient_netcore30.bat

@echo on
timeout 5

echo | call 4_Build_Framework_Tool.bat
echo | call 4_Build_Framework_ToolCore.bat

echo | call 5_Build_2CS_sample.bat
echo | call 5_Build_2CSCore_sample.bat

echo | call 5_Build_Bat_sample.bat
echo | call 5_Build_BatCore_sample.bat

echo | call 6_Build_WSSrv_sample.bat
echo | call 6_Build_WSSrvCore_sample.bat

echo | call 7_Build_Framework_WS.bat
echo | call 7_Build_Framework_WSCore.bat

echo | call 8_Build_WSClnt_sample.bat
echo | call 9_Build_WSClntCore_sample.bat

echo | call 10_Build_WebApp_sample.bat
echo | call 10_Build_WebAppCore_sample.bat

@echo on
timeout 5
