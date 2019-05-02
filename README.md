# Karl-Oskar
*A migration tool for the new world*

![A very old torp](https://dms-cf-06.dimu.org/image/022yjygyvT7C?dimension=max)

As Karl-Oskar in [Utvandrarna](https://en.wikipedia.org/wiki/The_Emigrants_(novel_series)) took his family over the vast Ocean, `Karl-Oskar` will help you over the sea of database changes. With [DbUp](https://github.com/DbUp/DbUp) as the Transantlantic crossing vessel, `Karl-Oskar` will set sails and help you to the other shore.

## Usage
```
Usage:  [options]

Options:
  -?|-h|--help                               Show help information
  -dir|--script-directory <DIRECTORY>        Path to migration script directory
  -cs|--connectionstring <CONNECTIONSTRING>  Connection string
  -db|--dbtype <name>                        Choose between sql (SQL Server) or postgresql (PostgreSQL - default)
```

## Build
Run `dotnet publish -c Release -r osx.10.11-x64` in the `/src` directory to build the project. 

## Run 
If you did build according to the previous description, the executable should be found as `/src/bin/Release/netcoreapp2.1/osx.10.11-x64/publish/karl-oskar`. Pro tip is to add `/src/bin/Release/netcoreapp2.1/osx.10.11-x64/publish` to the path to be able to run `karl-oskar` from any location.
