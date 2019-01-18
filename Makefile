
ROLEARN ?= arn:aws:iam::097591811552:role/GreenGrassAccess
RUNTIME = arn:aws:greengrass:::runtime/function/executable

all: ggdotnet

.PHONY: run clean lambda update delete

ggdotnet: Program.cs
	dotnet publish -c Debug --self-contained --runtime linux-x64 -o app

ggdotnet.zip: ggdotnet
	rm -f ggdotnet.zip
	cd app && zip ../ggdotnet.zip *

clean:
	rm -rf app ggdotnet.zip bin obj

lambda: ggdotnet.zip
	aws lambda create-function --function-name ggdotnet \
		--runtime $(RUNTIME) \
		--role $(ROLEARN) \
		--handler ggdotnet \
		--publish \
		--zip-file fileb://ggdotnet.zip

update: ggdotnet.zip
	aws lambda update-function-code --function-name ggdotnet \
		--zip-file fileb://ggdotnet.zip \
		--publish

delete:
	aws lambda delete-function --function-name ggdotnet
