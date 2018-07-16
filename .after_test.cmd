nuget install NUnit.Runners -Version 3.8.0 -OutputDirectory tools
nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
 
nuget install coveralls.net -Version 0.7.0 -OutputDirectory tools

./tools/OpenCover.4.6.519/tools/OpenCover.Console.exe -target:.tools/NUnit.ConsoleRunner.3.8.0/tools/nunit3-console.exe -targetargs:"/nologo /noshadow ./Algorithm.Logic.Tests/bin/Debug/Algorithm.Logic.CommandProcessor.dll" -filter:"+[*]* -[*.Tests]*" -register:user
 
./tools/coveralls.net.0.7.0/tools/csmacnz.Coveralls.exe --opencover -i ./results.xml