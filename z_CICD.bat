set CURRENTDIR=%cd%

rem VS2017
cd "root_VS2017\programs\CS"
echo | call 0_ExecAllBat.bat > ..\..\..\Build_2017CS.log

cd %CURRENTDIR%
move /Y *.log .\BackupCICDLog

cd "root_VS2017\programs\VB"
echo | call 0_ExecAllBat.bat > ..\..\..\Build_2017VB.log

cd %CURRENTDIR%
move /Y *.log .\BackupCICDLog

rem VS2019
cd "root_VS2019\programs\CS"
echo | call 0_ExecAllBat.bat > ..\..\..\Build_2019CS.log

cd %CURRENTDIR%
move /Y *.log .\BackupCICDLog

cd "root_VS2019\programs\VB"
echo | call 0_ExecAllBat.bat > ..\..\..\Build_2019VB.log

cd %CURRENTDIR%
move /Y *.log .\BackupCICDLog

rem VS2022
cd "root_VS2022\programs\CS"
echo | call 0_ExecAllBat.bat > ..\..\..\Build_2022CS.log

cd %CURRENTDIR%
move /Y *.log .\BackupCICDLog

cd "root_VS2022\programs\VB"
echo | call 0_ExecAllBat.bat > ..\..\..\Build_2022VB.log

cd %CURRENTDIR%
move /Y *.log .\BackupCICDLog