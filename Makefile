all: ggdotnet

.PHONY: run clean

ggdotnet: Program.cs
	dotnet publish -c Debug --self-contained --runtime linux-x64 -o app

ggdotnet.zip: ggdotnet
	cd app && zip ../ggdotnet.zip *


clean:
	rm -rf app ggdotnet.zip bin obj
