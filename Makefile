all: ggdotnet

ggdotnet: Program.cs
	dotnet publish -c Debug --self-contained --runtime linux-x64
	cp bin/Debug/netcoreapp2.2/linux-x64/ggdotnet .
