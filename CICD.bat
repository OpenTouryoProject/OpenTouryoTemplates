set CURRENTDIR=%cd%

rem VS2015
cd %CURRENTDIR%
cd "root_VS2015\programs\CS"
echo | call 0_ExecAllBat.bat > ..\..\..\BuildLog_2015CS.log

cd %CURRENTDIR%
cd "root_VS2015\programs\VB"
echo | call 0_ExecAllBat.bat > ..\..\..\BuildLog_2015VB.log


rem VS2017
cd %CURRENTDIR%
cd "root_VS2017\programs\CS"
echo | call 0_ExecAllBat.bat > ..\..\..\BuildLog_2017CS.log

cd %CURRENTDIR%
cd "root_VS2017\programs\VB"
echo | call 0_ExecAllBat.bat > ..\..\..\BuildLog_2017VB.log


rem VS2019
cd %CURRENTDIR%
cd "root_VS2019\programs\CS"
echo | call 0_ExecAllBat.bat > ..\..\..\BuildLog_2019CS.log

cd %CURRENTDIR%
cd "root_VS2019\programs\VB"
echo | call 0_ExecAllBat.bat > ..\..\..\BuildLog_2019VB.log